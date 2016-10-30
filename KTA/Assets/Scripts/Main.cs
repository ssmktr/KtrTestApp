using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;

public class Main : MonoBehaviour {

    public GameObject GetPhoneNumber, PlayToastMsg, GetPhoneID, GoogleLoginBtn, GoogleLogoutBtn, LeaderboardBtn;
    public GameObject[] AchievementBtn;
    public UILabel Value1;

    const string Leaderboard = "CgkIwbTUkrcMEAIQBA";

    const string Achievement1 = "CgkIwbTUkrcMEAIQAQ";
    const string Achievement2 = "CgkIwbTUkrcMEAIQAg";
    const string Achievement3 = "CgkIwbTUkrcMEAIQAw";
    const string Achievement4 = "CgkIwbTUkrcMEAIQBQ";
    const string Achievement5 = "CgkIwbTUkrcMEAIQBg";

    void Awake()
    {
        UIEventListener.Get(GetPhoneNumber).onClick = (sender) =>
        {
            Value1.text = NativeManager.Instance.ReceivePhoneNumber();
        };

        UIEventListener.Get(PlayToastMsg).onClick = (sender) =>
        {
            NativeManager.Instance.PlayToastMsg(Value1.text);
        };

        UIEventListener.Get(GetPhoneID).onClick = (sender) =>
        {
            Value1.text = NativeManager.Instance.ReceivePhoneModelID();
        };

        UIEventListener.Get(GoogleLoginBtn).onClick = (sender) =>
        {
            Social.localUser.Authenticate((success) =>
            {
                if (success)
                {
                    Debug.Log("login success");
                }
                else
                {
                    Debug.Log("login fail");
                }
            });
        };

        UIEventListener.Get(GoogleLogoutBtn).onClick = (sender) =>
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
        };

        for (int i = 0; i < AchievementBtn.Length; ++i)
            UIEventListener.Get(AchievementBtn[i]).onClick = OnClickAchievement;

        UIEventListener.Get(LeaderboardBtn).onClick = (sender) =>
        {
            Social.ReportScore(500, Leaderboard, (success) => {

            });
        };

    }

    void OnClickAchievement(GameObject sender)
    {
        switch (sender.name)
        {
            case "Achievement1Btn":
                Social.ReportProgress(Achievement1, 100.0f, (success) => {
                    ((PlayGamesPlatform)Social.Active).IncrementAchievement(Achievement1, 5, (success2) => {

                    });
                });
                break;

            case "Achievement2Btn":
                Social.ReportProgress(Achievement2, 100.0f, (success) => {
                    ((PlayGamesPlatform)Social.Active).IncrementAchievement(Achievement2, 5, (success2) => {

                    });
                });
                break;

            case "Achievement3Btn":
                Social.ReportProgress(Achievement3, 100.0f, (success) => {
                    ((PlayGamesPlatform)Social.Active).IncrementAchievement(Achievement3, 5, (success2) => {

                    });
                });
                break;

            case "Achievement4Btn":
                Social.ReportProgress(Achievement4, 100.0f, (success) => {
                    ((PlayGamesPlatform)Social.Active).IncrementAchievement(Achievement4, 5, (success2) => {

                    });
                });
                break;

            case "Achievement5Btn":
                Social.ReportProgress(Achievement5, 100.0f, (success) => {
                    ((PlayGamesPlatform)Social.Active).IncrementAchievement(Achievement5, 5, (success2) => {

                    });
                });
                break;
        }
    }


    void Start () {
        NativeManager.Instance.Init();
        Value1.text = "Init";

        PlayGamesPlatform.Activate();


    }

    // 볼륨, 백키, 등 기기에 달린 버튼누렀을때 해당 번호 얻기
    public void ReceiveKey(string keycodeint)
    {
        Value1.text = keycodeint;
    }
}
