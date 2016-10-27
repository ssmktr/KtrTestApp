using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class Main : MonoBehaviour {

    public GameObject GetPhoneNumber, PlayToastMsg, GetPhoneID;
    public UILabel Value1;

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

    }

	void Start () {
        NativeManager.Instance.Init();
        Value1.text = "Init";

        FB.Init(SetInit, OnHideUnity);
    }

    // 볼륨, 백키, 등 기기에 달린 버튼누렀을때 해당 번호 얻기
    public void ReceiveKey(string keycodeint)
    {
        Value1.text = keycodeint;
    }

    void SetInit()
    {
        Debug.Log("FaceBook Init");

        if (FB.IsLoggedIn)
        {
            Debug.Log("FaceBook Login");

            FB.ActivateApp();
        }
        else
        {
            Debug.Log("FaceBook Fail");

            FB.ShareLink(new System.Uri("http://developers.facebook.com/"), callback: ShareCallBack);
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0f;
            Debug.Log(0);
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log(1);
        }
    }

    void ShareCallBack(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink Error: " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("ShareLink success");
        }
    }

    public void LoginWithPublishPermission()
    {
        var perms = new List<string>() { "publish_actions" };
        FB.LogInWithPublishPermissions(perms, AuthCallBack);
    }

    void AuthCallBack(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);

            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
}
