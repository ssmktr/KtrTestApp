using UnityEngine;
using System.Collections;

public class NativeManager : Singleton<NativeManager> {

    AndroidJavaObject MyObj;

    public void Init()
    {
        AndroidJavaClass MyClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        MyObj = MyClass.GetStatic<AndroidJavaObject>("currentActivity");
    }

    // 폰번호 얻기
    public string ReceivePhoneNumber()
    {
        return MyObj.Call<string>("GetPhoneNumber");
    }

    // 폰 모델 아이디 얻기
    public string ReceivePhoneModelID()
    {
        return MyObj.Call<string>("GetPhoneModelID");
    }

    // 토스트 메시지 출력
    public void PlayToastMsg(string msg)
    {
        MyObj.Call("PlayToast", msg);
    }

    // 진동
    public void OnVibrator(long time)
    {
        MyObj.Call("OnVibrator", time);
    }
}
