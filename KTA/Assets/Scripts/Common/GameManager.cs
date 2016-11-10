using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public AccountInfo AccountInfo = new AccountInfo();
    public UserInfo UserInfo = new UserInfo();

    public static void GoScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
