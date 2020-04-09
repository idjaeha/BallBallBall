using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <Summary>
/// 게임의 진행을 도와주는 매니저
/// 각종 메세지를 받아 다른 매니저에게 명령을 내려 일을 수행한다.
/// 게임은 Setting -> Loading -> Playing -> Ending 으로 진행한다.
/// Setting은 게임이 최초 시작되었을 때 진행되고 GameManager가 생성된다.
/// Loading -> Playing -> Ending 은 Play단계에 해당한다.
/// 만약 Ending단계에서 특정 조건을 만족하면 Loading 단계로 돌아가서 Play단계를 진행할 수 있다.
/// </Summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private Player rawPlayer;
    [HideInInspector] public Player player;
    public StageManager rawStageManager;
    [HideInInspector] public StageManager stageManager;
    public GameObject UIPause;
    private GameObject tmpUIPause;
    public static GameManager instance = null;
    private List<Coroutine> processes = new List<Coroutine>();

    void Awake()
    {
        InitgameManager();
    }

    void Start()
    {
        StartCoroutine("PrepareGame");
    }


    private void InitPlay()
    {
        GameData.playtime = 0.0f;
        GameData.mapSizeX = 14;
        GameData.mapSizeY = 7;
        GameData.isPlaying = true;
        GameData.isPause = false;
        Time.timeScale = 1;
        player = Instantiate(rawPlayer, Vector3.zero, Quaternion.identity) as Player;
        tmpUIPause = Instantiate(UIPause, Vector3.zero, Quaternion.identity);
        tmpUIPause.SetActive(false);

        stageManager = Instantiate(rawStageManager, Vector3.zero, Quaternion.identity) as StageManager;
    }

    void Update()
    {
        if (GameData.isPlaying)
        {
            GameData.playtime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameData.isPlaying)
            {
                togglePause();
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void togglePause()
    {
        Time.timeScale = (Time.timeScale + 1) % 2;
        GameData.isPause = !GameData.isPause;
        if (GameManager.instance.tmpUIPause != null) GameManager.instance.tmpUIPause.SetActive(GameData.isPause);
    }

    IEnumerator PrepareGame()
    {
        while (instance == null)
        {
            yield return null;
        }
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Loading");
    }

    public void StartPlay()
    {
        SceneManager.LoadScene("InGame");
        StartCoroutine("PreparePlay");
    }

    IEnumerator PreparePlay()
    {
        while (!SceneManager.GetSceneByName("InGame").isLoaded)
        {
            yield return null;
        }
        InitPlay();
        stageManager.NextStage();
    }

    private void KillProcess()
    {
        foreach (var process in processes)
        {
            StopCoroutine(process);
        }
    }

    public void EndPlay()
    {
        KillProcess();
        GameData.isPlaying = false;
        SceneManager.LoadScene("Ending");
    }

    private void InitgameManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    /// <Summary>
    /// 메세지를 받아 처리하는 함수
    /// </Summary>
    public void HandleMailbox(string msg)
    {
        if (msg.Equals("CrashBall"))
        {
            stageManager.HandleCrash();
        }
    }
}
