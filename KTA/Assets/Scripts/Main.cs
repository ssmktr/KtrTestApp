using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    public GameObject GetPhoneNumber, PlayToastMsg, GetPhoneID;
    public GameObject OnVibrator, VibratorTime1Btn, VibratorTime2Btn, VibratorTime3Btn;
    public UILabel Value1;

    long VibratorTime = 1000;

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
