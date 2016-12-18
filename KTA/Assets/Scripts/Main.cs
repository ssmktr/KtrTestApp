using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GooglePlayGames;

public class Main : MonoBehaviour {

    public GameObject GetPhoneNumber, PlayToastMsg, GetPhoneID;
    public GameObject OnVibrator, VibratorTime1Btn, VibratorTime2Btn, VibratorTime3Btn;
    public UILabel Value1;

    long VibratorTime = 1000;

    // 구글
    public GameObject GoogleLogin, LeaderBoardShowBtn, AchieveShowBtn, Achieve1Btn, Achieve2Btn, Achieve3Btn, Achieve4Btn, Achieve5Btn, Achieve6Btn;
    public bool bGoogleLogon { get; set; }

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

        UIEventListener.Get(OnVibrator).onClick = (sender) =>
        {
            NativeManager.Instance.OnVibrator(VibratorTime);
        };

        UIEventListener.Get(VibratorTime1Btn).onClick = (sender) =>
        {
            VibratorTime = 500;
            OnVibrator.transform.FindChild("name").GetComponent<UILabel>().text = "0.5초 진동";
        };

        UIEventListener.Get(VibratorTime2Btn).onClick = (sender) =>
        {
            VibratorTime = 1000;
            OnVibrator.transform.FindChild("name").GetComponent<UILabel>().text = "1.0초 진동";
        };

        UIEventListener.Get(VibratorTime3Btn).onClick = (sender) =>
        {
            VibratorTime = 3000;
            OnVibrator.transform.FindChild("name").GetComponent<UILabel>().text = "3.0초 진동";
        };

        UIEventListener.Get(GoogleLogin).onClick = (sender) =>
        {
            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate(LoginCallBackGPGS);
            }
            else
                if (Social.localUser.authenticated)
            {
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
                LoginCallBackGPGS(false);
            }
        };

        UIEventListener.Get(LeaderBoardShowBtn).onClick = (sender) =>
        {
            Social.ShowLeaderboardUI();
        };

        UIEventListener.Get(AchieveShowBtn).onClick = (sender) =>
        {
            Social.ShowAchievementsUI();
        };

        UIEventListener.Get(Achieve1Btn).onClick = (sender) =>
        {
            IncreaseAchievement(sender, GoogleManager.GoogleData.achievement_1, 500);
        };

        UIEventListener.Get(Achieve2Btn).onClick = (sender) =>
        {
            IncreaseAchievement(sender, GoogleManager.GoogleData.achievement_2, 100);
        };

        UIEventListener.Get(Achieve3Btn).onClick = (sender) =>
        {
            IncreaseAchievement(sender, GoogleManager.GoogleData.achievement_3, 100);
        };

        UIEventListener.Get(Achieve4Btn).onClick = (sender) =>
        {
            IncreaseAchievement(sender, GoogleManager.GoogleData.achievement_4, 100);
        };

        UIEventListener.Get(Achieve5Btn).onClick = (sender) =>
        {
            IncreaseAchievement(sender, GoogleManager.GoogleData.achievement_5, 100);
        };

        UIEventListener.Get(Achieve6Btn).onClick = (sender) =>
        {
            IncreaseAchievement(sender, GoogleManager.GoogleData.achievement_6, 100);
        };
    }

    void IncreaseAchievement(GameObject sender, string key, double value)
    {
        Social.ReportProgress(key, value, (success) =>
        {
            if (success)
            {
                sender.transform.FindChild("name").GetComponent<UILabel>().text = "Success";
            }
            else
                sender.transform.FindChild("name").GetComponent<UILabel>().text = "Fail";
        });
    }

    public void LoginCallBackGPGS(bool result)
    {
        bGoogleLogon = result;
        if (bGoogleLogon)
        {
            GoogleLogin.transform.FindChild("name").GetComponent<UILabel>().text = "구글로그아웃";
        }
        else
        {
            GoogleLogin.transform.FindChild("name").GetComponent<UILabel>().text = "구글로그인";
        }
    }

    void Start () {
        NativeManager.Instance.Init();
        Value1.text = "Init";

        // 진동 초기화
        VibratorTime = 1000;
        OnVibrator.transform.FindChild("name").GetComponent<UILabel>().text = "1.0초 진동";

        PlayGamesPlatform.Activate();

        NativeManager.Instance.OnLocalAlarm(5);
    }

    // 볼륨, 백키, 등 기기에 달린 버튼누렀을때 해당 번호 얻기
    public void ReceiveKey(string keycodeint)
    {
        Value1.text = keycodeint;
    }
}
