using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeAndroid : MonoBehaviour {

    public AndroidJavaClass _Player;
    public AndroidJavaObject _CurrentActivity;

    public void RequestOpenWebView(string url)
    {
        OpenWebView(url);
    }

    public void OpenWebView(string _param)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if (_Player == null && _CurrentActivity == null)
        {
            _Player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _CurrentActivity = _Player.GetStatic<AndroidJavaClass>("currentActivity");
        }
        _CurrentActivity.Call("openNativeWebView", new string[] { _param });
#endif
    }
}
