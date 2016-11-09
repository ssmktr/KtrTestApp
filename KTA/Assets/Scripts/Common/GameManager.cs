using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public static void GoScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
