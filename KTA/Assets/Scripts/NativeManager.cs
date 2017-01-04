using UnityEngine;
using System.Collections;

public class NativeManager : Singleton<NativeManager> {

    AndroidJavaObject MyObj;

    public void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass MyClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        MyObj = MyClass.GetStatic<AndroidJavaObject>("currentActivity");
#endif
    }

    // 폰번호 얻기
    public string ReceivePhoneNumber()
    {
        if (MyObj == null) return "";

        return MyObj.Call<string>("GetPhoneNumber");
    }

    // 폰 모델 아이디 얻기
    public string ReceivePhoneModelID()
    {
        if (MyObj == null) return "";

        return MyObj.Call<string>("GetPhoneModelID");
    }

    // 토스트 메시지 출력
    public void PlayToastMsg(string msg)
    {
        if (MyObj == null) return;

        MyObj.Call("PlayToast", msg);
    }

    // 진동
    public void OnVibrator(long time)
    {
        if (MyObj == null) return;

        MyObj.Call("OnVibrator", time);
    }

    // 로컬 푸시
    public void OnLocalAlarm(int time)
    {
        if (MyObj == null) return;

        MyObj.Call("LocalAlarm", time);
    }
}
