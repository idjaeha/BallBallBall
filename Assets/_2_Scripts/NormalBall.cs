using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startForce;
    public float shootSpeed { get; set; }
    public Vector3 shootDir { get; set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShootBall(float angle, float speed)
    {
        rb = GetComponent<Rigidbody>();
        shootDir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        shootSpeed = speed;
        rb.AddForce(shootDir * shootSpeed);

    }
    public void ShootBall(Vector3 dir, float speed)
    {
        rb = GetComponent<Rigidbody>();
        shootDir = dir.normalized;
        shootSpeed = speed;
        rb.AddForce(shootDir * shootSpeed);
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
