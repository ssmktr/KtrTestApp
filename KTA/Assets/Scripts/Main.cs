using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public GameObject GetPhoneNumber;
    public UILabel Value1;
    AndroidJavaObject MyObj;

    void Awake()
    {
        UIEventListener.Get(GetPhoneNumber).onClick = (sender) =>
        {
            if (MyObj != null)
            {
                MyObj.Call("GetPhoneNumber");
            }
        };
    }

	void Start () {
        Value1.text = "Init";

        AndroidJavaClass MyClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        MyObj = MyClass.GetStatic<AndroidJavaObject>("currentActivity");

    }
	
    // 볼륨, 백키, 등 기기에 달린 버튼누렀을때 해당 번호 얻기
	public void ReceiveKey(string keycode) {
        Value1.text = keycode;
	}

    // 폰번호 얻기
    public void ReceivePhoneNumber(string number)
    {
        Value1.text = number;
    }
}
