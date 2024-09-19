using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string[] m_requiredSceneNames;

    [SerializeField]
    private string m_gameplaySceneName;

    public event System.Action GameplaySceneReloaded = delegate { };

    public void LoadRequiredScenes()
    {
        for (int iScene = 0; iScene < m_requiredSceneNames.Length; ++iScene)
        {
            SceneManager.LoadScene(m_requiredSceneNames[iScene], LoadSceneMode.Additive);
        }
    }

    public void ReloadGameplayScene()
    {
        StartCoroutine(WaitForReload());

    }

    private IEnumerator WaitForReload()
    {
        AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(m_gameplaySceneName);
        SceneManager.LoadScene(m_gameplaySceneName, LoadSceneMode.Additive);
        yield return asyncOp;
        yield return new WaitForEndOfFrame();
        GameplaySceneReloaded.Invoke();
    }

}

