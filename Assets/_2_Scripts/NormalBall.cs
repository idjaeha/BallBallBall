using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startForce;
    private float startAngle;
    public float startSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startAngle = Random.Range(0f, 360.0f);
        startSpeed = 500.0f;
        rb = GetComponent<Rigidbody>();
        Vector3 vector = Quaternion.AngleAxis(startAngle, Vector3.forward) * Vector3.right;
        rb.AddForce(vector * startSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            if (other.transform.position.y == 0) // 오른쪽, 왼쪽에 존재하는 벽인 경우
            {
                rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, 0);
            }
            else if (other.transform.position.x == 0) // 위쪽, 아래쪽에 존재하는 벽인 경우
            {
                rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, 0);
            }
        }
        
    }
}
