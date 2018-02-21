using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class MainPanel : MonoBehaviour {

    public UILabel MessageLbl;

    private void Start()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    void InitCallBack()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            MessageLbl.text = "Failed to Initialize the FaceBook SDK";
        }
    }

    void OnHideUnity(bool isGameShow)
    {
        if (!isGameShow)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    void FaceBookLogin()
    {
        List<string> perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallBack);
    }

    void AuthCallBack(ILoginResult result)
    {
        if (result.Error != null)
        {
            MessageLbl.text = string.Format("Auth Error : {0}", result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                MessageLbl.text = "FaceBook Login!!";
                AccessToken aToken = AccessToken.CurrentAccessToken;
                MessageLbl.text += string.Format("\naToken string : {0}", aToken.TokenString);
                MessageLbl.text += string.Format("\nUser ID : {0}", aToken.UserId);

                foreach (string perm in aToken.Permissions)
                {
                    MessageLbl.text += string.Format("\npermissions : {0}", perm);
                }
            }
            else
            {
                MessageLbl.text = "User cancelled login";
            }
        }
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields-first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
    }

    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            MessageLbl.text += string.Format("\n반가워요. {0}", result.ResultDictionary["first_name"]);
        }
        else
        {
            MessageLbl.text = string.Format("DisplayUsername Error {0}", result.Error);
        }
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Error == null)
        {
            MessageLbl.text += "\n나의 프로필 이미지 성공";
        }
        else
        {
            MessageLbl.text = string.Format("DisplayProfilePic Error {0}", result.Error);
        }
    }

    void FaceBookLogout()
    {
        FB.LogOut();
        MessageLbl.text = "Logout";
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 200, 100), "Login"))
        {
            FaceBookLogin();
        }

        if (GUI.Button(new Rect(50, 250, 200, 100), "Logout"))
        {
            FaceBookLogout();
        }
    }
}
