using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Login : MonoBehaviour {

    public const string DB_URL = "http://ssmktr.ivyro.net/MMOProject/";

    public GameObject LoginBtn, SignupBtn;
    public UIInput InputId, InputPw, InputNickName;

    void Awake()
    {
        UIEventListener.Get(LoginBtn).onClick = (sender) =>
        {
            if (!string.IsNullOrEmpty(InputId.value) && !string.IsNullOrEmpty(InputPw.value))
            {
                InputNickName.value = "";
                RequestLogin();
            }
            else
                Debug.Log("아이디와 비밀번호를 올바르게 입력해주세요.");
        };

        UIEventListener.Get(SignupBtn).onClick = (sender) =>
        {
            if (!string.IsNullOrEmpty(InputId.value) && !string.IsNullOrEmpty(InputPw.value) && !string.IsNullOrEmpty(InputNickName.value))
            {
                RequestSignup();
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

    void RequestLogin()
    {
        StartCoroutine(_RequestLogin());
    }

    IEnumerator _RequestLogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("platform_id", InputId.value);
        form.AddField("platform_pass", InputPw.value);

        WWW www = new WWW(DB_URL + "Login/Login.php", form);

        while (!www.isDone)
            yield return null;

        yield return www;

        if (www.error == null)
        {
            string Content = www.text.Trim();
            Debug.Log(Content);

            Dictionary<string, object> DicData = Json.Deserialize(Content) as Dictionary<string, object>;
            if (DicData != null)
            {
                if (DicData.ContainsKey("ecode"))
                {
                    if (JsonUtil.GetStringValue(DicData, "ecode") == "success")
                    {
                        if (DicData.ContainsKey("AccountInfo"))
                        {
                            Dictionary<string, object> DicAccountInfo = DicData["AccountInfo"] as Dictionary<string, object>;
                            if (DicAccountInfo.ContainsKey("account_gsn"))
                                GameManager.Instance.AccountInfo.account_gsn = uint.Parse(JsonUtil.GetStringValue(DicAccountInfo, "account_gsn"));

                            if (DicAccountInfo.ContainsKey("platform_id"))
                                GameManager.Instance.AccountInfo.platform_id = JsonUtil.GetStringValue(DicAccountInfo, "platform_id");

                            if (DicAccountInfo.ContainsKey("platform_pass"))
                                GameManager.Instance.AccountInfo.platform_pass = JsonUtil.GetStringValue(DicAccountInfo, "platform_pass");

                            if (DicAccountInfo.ContainsKey("nickname"))
                            {
                                InputNickName.value = JsonUtil.GetStringValue(DicAccountInfo, "nickname");
                                GameManager.Instance.AccountInfo.nickname = InputNickName.value;
                            }

                            if (DicAccountInfo.ContainsKey("join_dt"))
                                GameManager.Instance.AccountInfo.join_dt = long.Parse(JsonUtil.GetStringValue(DicAccountInfo, "join_dt"));

                            if (DicAccountInfo.ContainsKey("block_type"))
                                GameManager.Instance.AccountInfo.block_type = int.Parse(JsonUtil.GetStringValue(DicAccountInfo, "block_type")) != 0;

                            if (DicAccountInfo.ContainsKey("block_expire_dt"))
                                GameManager.Instance.AccountInfo.block_expire_dt = long.Parse(JsonUtil.GetStringValue(DicAccountInfo, "block_expire_dt"));

                            GameManager.GoScene("Lobby");
                        }
                    }
                }
            }
        }
        else
            Debug.Log(www.error);
    }

    void RequestSignup()
    {
        StartCoroutine(_RequestSignup());
    }

    IEnumerator _RequestSignup()
    {
        WWWForm form = new WWWForm();
        form.AddField("platform_id", InputId.value);
        form.AddField("platform_pass", InputPw.value);
        form.AddField("nickname", InputNickName.value);

        WWW www = new WWW(DB_URL + "Login/Signup.php", form);

        while (!www.isDone)
            yield return null;

        yield return www;

        if (www.error == null)
        {
            string Content = www.text.Trim();
            Debug.Log(Content);

            Dictionary<string, object> DicData = Json.Deserialize(Content) as Dictionary<string, object>;
            if (DicData != null)
            {
                
            }
        }
        else
            Debug.Log(www.error);
    }
}
