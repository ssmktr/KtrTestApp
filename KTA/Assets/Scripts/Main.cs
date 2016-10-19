using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public GameObject Btn1;
    public UILabel Value1;

    void Awake()
    {
        UIEventListener.Get(Btn1).onClick = (sender) =>
        {
            Value1.text = "Click";
        };
    }

	void Start () {
        Value1.text = "Init";

    }
	
	public void ReceiveKey(string keycode) {
        Value1.text = keycode;
	}
}
