using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public NormalBall normalBall;
    public string[] state = { "Circle", "Line" };

    void Start()
    {
        SpawnCircle();
    }

    public void SpawnCircle()
    {
        for (var idx = 0; idx < 10; idx++)
        {
            normalBall = Instantiate(normalBall, transform.position, Quaternion.identity);
            normalBall.startAngle = 360 - 36 * idx;
            normalBall.startSpeed = 300.0f;
            normalBall.ShootBall();
        }
    }
}
