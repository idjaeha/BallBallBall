using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public NormalBall normalBall;
    // Start is called before the first frame update
    void Start()
    {
        for (var idx = 0; idx < 10; idx++) 
        normalBall = Instantiate(normalBall, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
