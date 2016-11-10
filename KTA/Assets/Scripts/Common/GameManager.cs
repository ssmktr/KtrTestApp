using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public AccountInfo AccountInfo = new AccountInfo();
    public UserInfo UserInfo = new UserInfo();

    public List<UnitData> MyUnitList = new List<UnitData>();

    public static void GoScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
