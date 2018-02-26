using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

using TinyJSON;

public class MainPanel : MonoBehaviour {

    public GameObject GoogleLoginBtn, GoogleLogoutBtn, GoogleImageBtn, GoogleShowAchievementBtn, GoogleShowLeaderBoardBtn, GoogleUseLeaderBoardBtn, GoogleGetLeaderBoardScoreBtn;
    public GameObject FaceBookLoginBtn, FaceBookLogoutBtn, FaceBookTestBtn, FaceBookShowTestBtn;
    public UILabel MessageLbl;
    public UI2DSprite Profile;

    private void Awake()
    {
        UIEventListener.Get(GoogleLoginBtn).onClick = (sender) =>
        {
            if (!NativeManager.Instance.IsGoogleLogin)
            {
                NativeManager.Instance.GoogleLogin();
            }
        };

        UIEventListener.Get(GoogleLogoutBtn).onClick = (sender) =>
        {
            if (NativeManager.Instance.IsGoogleLogin)
            {
                NativeManager.Instance.GoogleLogout();
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

        UIEventListener.Get(GoogleShowAchievementBtn).onClick = (sender) =>
        {
            if (NativeManager.Instance.IsGoogleLogin)
            {
                NativeManager.Instance.GoogleShowAchievement();
            }
        };

        UIEventListener.Get(GoogleShowLeaderBoardBtn).onClick = (sender) =>
        {
            if (NativeManager.Instance.IsGoogleLogin)
            {
                NativeManager.Instance.GoogleShowLeaderBoard();
            }
        };

        UIEventListener.Get(GoogleUseLeaderBoardBtn).onClick = (sender) =>
        {
            if (NativeManager.Instance.IsGoogleLogin)
            {
                NativeManager.Instance.GoogleUseLeaderBoard(Random.Range(1, 100000), (result)=> 
                {
                    MessageLbl.text = result;
                });
            }
        };

        UIEventListener.Get(GoogleGetLeaderBoardScoreBtn).onClick = (sender) =>
        {
            if (NativeManager.Instance.IsGoogleLogin)
            {
                MessageLbl.text = "";
                NativeManager.Instance.GoogleGetLeaderBoardScore(result =>
                {
                    UnityEngine.SocialPlatforms.IScore[] scoreArray = result;
                    if (scoreArray.Length > 0)
                    {
                        for (int i = 0; i < scoreArray.Length; ++i)
                        {
                            MessageLbl.text += string.Format("User Id : {0}, Value : {1}, Rank : {2}\n\n", scoreArray[i].userID, scoreArray[i].value, scoreArray[i].rank);
                        }
                    }
                    else
                        MessageLbl.text = "EMPTY...";
                });
            }
        };

        UIEventListener.Get(FaceBookLoginBtn).onClick = (sender) =>
        {
            Debug.Log("FACEBOOK LOGIN...");

            FaceBookLogin();
        };

        UIEventListener.Get(FaceBookLogoutBtn).onClick = (sender) =>
        {
            Debug.Log("FACEBOOK LOGOUT...");

            FaceBookLogout();
        };

        UIEventListener.Get(FaceBookTestBtn).onClick = (sender) =>
        {
            MessageLbl.text = "FACEBOOK TEST BTN";

            SetScores();
        };

        UIEventListener.Get(FaceBookShowTestBtn).onClick = (sender) =>
        {
            MessageLbl.text = "FACEBOOK SHOW TEST BTN";

            QueryScores();
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

    //점수 불러오기
    public void QueryScores()
    {
        FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, ScoresCallback);
    }

    //점수 Scores Console에 표시하기
    private void ScoresCallback(IResult result)
    {
        Debug.Log("Scores Callback : " + result.ResultDictionary.ToJson());
        MessageLbl.text = result.ResultDictionary.ToJson();
    }

    //점수 설정하기
    public void SetScores()
    {
        Dictionary<string, string> scoreData = new Dictionary<string, string>();
        scoreData["score"] = Random.Range(10, 200).ToString();
        FB.API("/me/scores", HttpMethod.POST, ScoreCallBack, scoreData);
    }

    void ScoreCallBack(IGraphResult result)
    {
        MessageLbl.text = MiniJSON.Json.Serialize(result.ResultDictionary);

        QueryScores();
    }

    void FaceBookLogout()
    {
        FB.LogOut();
        MessageLbl.text = "Logout";
    }
    #endregion // FACEBOOK
}
