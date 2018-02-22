using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class MainPanel : MonoBehaviour {

    public GameObject GoogleLoginBtn, GoogleLogoutBtn, GoogleImageBtn, FaceBookLoginBtn, FaceBookLogoutBtn;
    public UILabel MessageLbl;
    public UI2DSprite Profile;

    private void Awake()
    {
        UIEventListener.Get(GoogleLoginBtn).onClick = (sender) =>
        {
            MessageLbl.text = "Login...";
            if (!NativeManager.Instance.IsGoogleLogin)
            {
                NativeManager.Instance.GoogleLogin();
                MessageLbl.text = "Login Success";
            }
        };

        UIEventListener.Get(GoogleLogoutBtn).onClick = (sender) =>
        {
            MessageLbl.text = "Logout...";
            if (NativeManager.Instance.IsGoogleLogin)
            {
                NativeManager.Instance.GoogleLogout();
                MessageLbl.text = "Logout Success";
            }
        };

        UIEventListener.Get(GoogleImageBtn).onClick = (sender) =>
        {
            if (NativeManager.Instance.IsGoogleLogin)
            {
                MessageLbl.text = NativeManager.Instance.GetGoogleId();

                Profile.sprite2D = NativeManager.Instance.GetGoogleImage();
                Profile.MakePixelPerfect();
            }
        };

        UIEventListener.Get(FaceBookLoginBtn).onClick = (sender) =>
        {
            FaceBookLogin();
        };

        UIEventListener.Get(FaceBookLogoutBtn).onClick = (sender) =>
        {
            FaceBookLogout();
        };
    }

    private void Start()
    {
        NativeManager.Instance.Init();

        FaceBookInit();
    }

    #region FACEBOOK
    void FaceBookInit()
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

        DealWithFBMenus(FB.IsLoggedIn);
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
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
            Profile.sprite2D = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
            Profile.MakePixelPerfect();
        }
        else
        {
            MessageLbl.text = string.Format("DisplayProfilePic Error {0}", result.Error);
        }
    }

    //친구초대
    public void InviteFriends()
    {
        // 순서 변경시 컴파일 에러 발생
        // message : 보낼 메시지
        // title : 메시지 보낼 친구목록 창의 타이틀
        FB.AppRequest(
            message: "This gmae is awesome, join me. now!",
            title: "Invite your firends to join you"
        );
    }

    void FaceBookLogout()
    {
        FB.LogOut();
        MessageLbl.text = "Logout";
    }
    #endregion // FACEBOOK
}
