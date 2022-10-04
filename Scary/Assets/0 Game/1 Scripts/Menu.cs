using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    readonly string s_nextScene = "2 ªüÀ¨®a";

    AsyncOperation async = null;

    void Awake()
    {
        StartCoroutine(nameof(LoadScene));
    }

    public void StartBtn()
    {
        if (async.progress < 0.9f)
            return;

        // async.allowSceneActivation = true;
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(s_nextScene);

        async.allowSceneActivation = false;

        if (async.progress >= 0.9f)
            Debug.Log($"PreLoad scene {s_nextScene} complete");

        yield return null;
    }
}
