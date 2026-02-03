using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _gameManager : MonoBehaviour
{
    #region Singleton
    public static _gameManager instance;

    public static _gameManager Instance
    {
        get
        {
            if (!instance)
                instance = FindFirstObjectByType<_gameManager>();

            if (!instance)
                throw new System.Exception("No _gameManager instance found in the scene.");

            return instance;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Scene Management
    [SerializeField] private string[] levelNames;
    private bool isLoadingScene = false;
    private string currentLevelName = "";
    private int currentLevelIndex = 0;

    private void Start()
    {
        StartCoroutine(loadLevel(levelNames[0]));
    }

    IEnumerator loadLevel(string LevelName)
    {
        isLoadingScene = true;
        if (!string.IsNullOrEmpty(currentLevelName))
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentLevelName);
            while (!asyncUnload.isDone)
                yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelName);
        while (!asyncLoad.isDone)
            yield return null;
        isLoadingScene = false;
    }
    #endregion
}
