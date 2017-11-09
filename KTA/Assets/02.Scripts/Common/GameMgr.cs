using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMgr : Singleton<GameMgr> {

    public void GoScene(string SceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
        StartCoroutine(_GoScene(async));
    }

    IEnumerator _GoScene(AsyncOperation async)
    {
        while (!async.isDone)
        {
            Debug.Log(async.progress);
            yield return null;
        }
    }
}
