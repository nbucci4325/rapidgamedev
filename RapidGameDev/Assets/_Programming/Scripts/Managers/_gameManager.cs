using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _gameManager : MonoBehaviour
{
    //[SerializeField] GameObject playerGameObject;
    //public GameObject PlayerGameObject => playerGameObject;
    //private playerMove playerMove;
    //public playerMove PlayerMove => playerMove;

    #region Singleton
    public static _gameManager instance;

    /// <summary>
    /// Defines the construction of the singleton
    /// </summary>
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

    /// <summary>
    /// Defines the behaviour of the singleton on start
    /// </summary>
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
    [Tooltip("Input level names here (MUST be exactly as written in the scene file) that are to be loaded")]
    [SerializeField] private string[] levelNames;
    private bool isLoadingScene = false;
    private string currentLevelName = "";
    private int currentLevelIndex = 0;
    [SerializeField] GameObject playerGO;
    [SerializeField] GameObject mainCamera;

    /// <summary>
    /// Defines the start behaviour of the game
    /// </summary>
    private void Start()
    {
        StartCoroutine(loadLevel(levelNames[0]));
    }

    /// <summary>
    /// Defines what is to be done upon level completion
    /// </summary>
    public void levelComplete()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levelNames.Length)
        {
            StartCoroutine(loadLevel(levelNames[currentLevelIndex]));
        }
    }

    /// <summary>
    /// Coroutine describe actions to be taken in order to load a new level into the game
    /// </summary>
    /// <param name="LevelName">The level name, as a string, to be loaded</param>
    /// <returns>IEnumerator interface as per coroutine convention</returns>
    IEnumerator loadLevel(string levelName)
    {
        isLoadingScene = true;
        mainCamera.SetActive(false);
        playerGO.SetActive(false);
       if (!string.IsNullOrEmpty(currentLevelName))
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentLevelName);
            while (!asyncUnload.isDone)
                yield return null;
        }

       AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
            yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));
        currentLevelName = levelName;

        if (currentLevelName == "Level_01")
        {
            playerGO.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        }
        if (currentLevelName == "Level_02")
        {
            playerGO.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        }
        if (currentLevelName == "Level_03")
        {
            playerGO.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        }
        mainCamera.SetActive(true);
        playerGO.SetActive(true);
        isLoadingScene = false;
    }
    #endregion
}
