using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class NativeManager : Singleton<NativeManager>
{

    public void Init()
    {
        GoogleInit();
    }

    #region GOOGLE
    public bool IsGoogleLogin = false;
    void GoogleInit()
    {
        IsGoogleLogin = false;
        PlayGamesPlatform.Activate();
    }

    public void GoogleLogin()
    {
        if (!Social.localUser.authenticated)
            Social.localUser.Authenticate(GoogleLoginCallBack);
    }

    void GoogleLoginCallBack(bool result)
    {
        IsGoogleLogin = result;
    }

    public void GoogleLogout()
    {
        if (Social.localUser.authenticated)
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
            IsGoogleLogin = false;
        }
    }

    public Sprite GetGoogleImage()
    {
        if (Social.localUser.authenticated)
            return Sprite.Create(Social.localUser.image, new Rect(0, 0, 128, 128), new Vector2());

        return null;
    }

    public string GetGoogleId()
    {
        if (Social.localUser.authenticated)
            return Social.localUser.id;

        return null;
    }

    public string GetGoogleName()
    {
        if (Social.localUser.authenticated)
            return Social.localUser.userName;

        return null;
    }

    // 리더보드 보여주기
    public void GoogleShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }

    // 리더보드 사용
    public void GoogleUseLeaderBoard(long score, System.Action<string> callback)
    {
        Social.ReportScore(score, GoogleManager.GoogleData.leaderboard_ktrtestappleaderboard, (result) =>
        {
            if (result)
                callback(string.Format("SCORE : {0} SUCCESS", score));
            else
                callback(string.Format("SCORE : {0} FAIL", score));
        });
    }

    // 리더보드 스코어 가져오기
    public void GoogleGetLeaderBoardScore(System.Action<UnityEngine.SocialPlatforms.IScore[]> callback)
    {
        Social.LoadScores(GoogleManager.GoogleData.leaderboard_ktrtestappleaderboard, callback);
    }

    // 업적 보기
    public void GoogleShowAchievement()
    {
        Social.ShowAchievementsUI();
    }

    #endregion // GOOGLE

    #region FACEBOOK

    #endregion // FACEBOOK
}
