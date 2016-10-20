using UnityEngine;
using System.Collections;

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
    }

    // 볼륨, 백키, 등 기기에 달린 버튼누렀을때 해당 번호 얻기
    public void ReceiveKey(string keycodeint)
    {
        Value1.text = keycodeint;
    }
}
