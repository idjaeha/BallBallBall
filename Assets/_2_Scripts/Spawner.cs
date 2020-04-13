using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public NormalBall normalBall;
    public string state = "Circle";
    public bool isTrigger = false; // true일 경우 AutoShoot을 이용하지 않고 사용자가 정의내린 함수를 이용하게 된다.
    public float shootSpeed = 300.0f;
    public int shootCount = 0; // count가 0이면 무한 반복
    public float shootWait = 10.0f;
    private Coroutine runningShoot;
    private Dictionary<string, DelSpawn> convertorState;
    public delegate void DelSpawn(float speed);
    float rotateAngle = 0;

    public AudioClip spawnSound;
    private AudioSource audioPlayer;

    [SerializeField] private GameObject warp;
    public void Spawn(float speed = 0f)
    {
        shootSpeed = speed;
        PlaySound(spawnSound);
        StartCoroutine("EffectAndSpawn");
    }

    IEnumerator EffectAndSpawn()
    {
        var tmp = Instantiate(warp, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        convertorState[state](shootSpeed);
    }

    private void Awake()
    {
        convertorState = new Dictionary<string, DelSpawn>()
        {
            { "Circle", SpawnCircle},
        { "Rotating", SpawnRotating},
        { "ToPlayer" , SpawnToPlayer}
        };
        audioPlayer = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioPlayer.Stop();
        audioPlayer.clip = clip;
        audioPlayer.time = 0;
        audioPlayer.Play();
    }

    public void SpawnCircle(float shootSpeed = 300.0f)
    {
        for (var idx = 0; idx < 10; idx++)
        {
            normalBall = GameManager.instance.GetBall();
            if (normalBall != null)
            {
                normalBall.transform.position = transform.position;
                normalBall.gameObject.SetActive(true);
                normalBall.ShootBall(360 - 36 * idx, shootSpeed);
            }
        }
    }

    public void SpawnRotating(float shootSpeed = 300.0f)
    {
        for (var idx = 0; idx < 10; idx++)
        {
            normalBall = GameManager.instance.GetBall();
            if (normalBall != null)
            {
                normalBall.transform.position = transform.position;
                normalBall.gameObject.SetActive(true);
                normalBall.ShootBall(360 - 36 * idx + rotateAngle, shootSpeed);
            }
        }
        rotateAngle += 30;
    }

    public void SpawnToPlayer(float shootSpeed = 300.0f)
    {
        normalBall = GameManager.instance.GetBall();
        if (normalBall != null)
        {
            Transform playerTr = GameManager.instance.player.transform;
            Vector3 shootDir = playerTr.position - transform.position;
            normalBall.transform.position = transform.position;
            normalBall.gameObject.SetActive(true);
            normalBall.ShootBall(shootDir, shootSpeed);
        }

    }


    IEnumerator AutoShoot()
    {
        if (shootCount != 0)
        {
            for (var count = 0; count < shootCount; count++)
            {
                Spawn(shootSpeed);
                yield return new WaitForSeconds(shootWait);
            }
        }
        else
        {
            for (; ; )
            {
                Spawn(shootSpeed);
                yield return new WaitForSeconds(shootWait);
            }
        }
    }
    public void StartSpawn()
    {
        if (!isTrigger)
        {
            runningShoot = StartCoroutine("AutoShoot");
        }
    }

    public void StopSpawn()
    {
        StopCoroutine(runningShoot);
    }

}
