using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public NormalBall normalBall;
    public string state;
    private Dictionary<string, DelSpawn> convertorState;
    public delegate void DelSpawn(float speed);
    public void Spawn(float speed = 300.0f)
    {
        convertorState[state](speed);
    }

    void Awake()
    {
        convertorState = new Dictionary<string, DelSpawn>()
    {
        {"Circle", SpawnCircle},
        {"Player" , SpawnPlayer}
    };
        state = "Circle";
    }
    void Start()
    {
    }

    public void SpawnCircle(float shootSpeed = 300.0f)
    {
        for (var idx = 0; idx < 10; idx++)
        {
            if (normalBall != null)
            {
                normalBall = Instantiate(normalBall, transform.position, Quaternion.identity);
                normalBall.ShootBall(360 - 36 * idx, shootSpeed);
            }
        }
    }

    public void SpawnPlayer(float shootSpeed = 300.0f)
    {
        if (normalBall != null)
        {
            Transform playerTr = GameManager.instance.player.transform;
            Vector3 shootDir = playerTr.position - transform.position;
            normalBall = Instantiate(normalBall, transform.position, Quaternion.identity);
            normalBall.ShootBall(shootDir, shootSpeed);
        }
    }
}
