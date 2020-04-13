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
    [HideInInspector] public float playtime = 0.0f;
    [HideInInspector] public bool isPlaying = false;
    [HideInInspector] public bool isPause = false;
    [HideInInspector] public int playerLife = 0;
    private int ballCount = 200;
    public NormalBall normalBall;
    public GameObject balls;
    private List<NormalBall> objectPool;
    [HideInInspector]
    public float mapSizeX
    {
        get; set;
    }
    [HideInInspector]
    public float mapSizeY
    {
        get; set;
    }
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
        CreateNormalBallInObjectPool();
    }

    private void CreateNormalBallInObjectPool()
    {
        objectPool = new List<NormalBall>();
        balls = Instantiate(balls, Vector3.zero, Quaternion.identity);

        for (int idx = 0; idx < ballCount; idx++)
        {
            NormalBall tmp = Instantiate(normalBall, balls.transform) as NormalBall;
            tmp.name = $"ball_{idx}";
            objectPool.Add(tmp);
            tmp.gameObject.SetActive(false);
        }
        DontDestroyOnLoad(balls);
    }

    public NormalBall GetBall()
    {
        for (int idx = 0; idx < ballCount; idx++)
        {
            if (objectPool[idx].gameObject.activeSelf == false)
            {
                return objectPool[idx];
            }
        }
        return null;
    }

    public void ClearBalls()
    {
        for (int idx = 0; idx < ballCount; idx++)
        {
            objectPool[idx].gameObject.SetActive(false);
        }
    }

    public void ClearBall(NormalBall ball)
    {
        ball.gameObject.SetActive(false);
    }

    void Start()
    {
        StartCoroutine("PrepareGame");
    }


    private void InitPlay()
    {
        playtime = 0.0f;
        mapSizeX = 16.25f;
        mapSizeY = 8.25f;
        isPlaying = true;
        isPause = false;
        playerLife = 0;
        Time.timeScale = 1;
        player = Instantiate(rawPlayer, Vector3.zero, Quaternion.identity) as Player;
        tmpUIPause = Instantiate(UIPause, Vector3.zero, Quaternion.identity);
        tmpUIPause.SetActive(false);

        stageManager = Instantiate(rawStageManager, Vector3.zero, Quaternion.identity) as StageManager;
    }

    void Update()
    {
        if (isPlaying)
        {
            playtime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPlaying)
            {
                togglePause();
            }
            else
            {
                Application.Quit();
            }
        }
        if (stageManager != null)
        {
            if (stageManager.stage == 0 && playtime > 10.0f)
            {
                stageManager.NextStage();
            }

            if (stageManager.stage == 1 && playtime > 25.0f)
            {
                stageManager.NextStage();
            }

            if (stageManager.stage == 2 && playtime > 60.0f)
            {
                stageManager.NextStage();
            }
        }
    }

    public void togglePause()
    {
        Time.timeScale = (Time.timeScale + 1) % 2;
        isPause = !isPause;
        if (tmpUIPause != null) tmpUIPause.SetActive(isPause);
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
        ClearBalls();
        stageManager.Clean();
        isPlaying = false;
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
