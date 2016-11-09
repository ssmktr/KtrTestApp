using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Login : MonoBehaviour {

    public GameObject LoginBtn, SignupBtn;
    public UIInput InputId, InputPw, InputNickName;

    void Awake()
    {
        UIEventListener.Get(LoginBtn).onClick = (sender) =>
        {
            if (!string.IsNullOrEmpty(InputId.value) && !string.IsNullOrEmpty(InputPw.value))
            {

            }
            else
                Debug.Log("아이디와 비밀번호를 올바르게 입력해주세요.");
        };

        UIEventListener.Get(SignupBtn).onClick = (sender) =>
        {
            if (!string.IsNullOrEmpty(InputId.value) && !string.IsNullOrEmpty(InputPw.value) && !string.IsNullOrEmpty(InputNickName.value))
            {

            }
            else
                Debug.Log("아이디와 비밀번호, 닉네임을 올바르게 입력해주세요.");
        };
    }

    void Start () {
        InputId.value = "";
        InputPw.value = "";
        InputNickName.value = "";    
	}
}
