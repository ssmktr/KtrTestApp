using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

//using Facebook.Unity;

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
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
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

    #endregion // GOOGLE

    #region FACEBOOK

    #endregion // FACEBOOK
}
