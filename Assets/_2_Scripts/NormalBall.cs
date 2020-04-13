using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startForce;
    public float shootSpeed { get; set; }
    public Vector3 shootDir { get; set; }
    [SerializeField] private GameObject explosion;
    private AudioSource audioPlayer;
    public AudioClip explosionSound;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    public void PlaySound(AudioClip clip = null)
    {
        if (clip == null) clip = explosionSound;
        audioPlayer.Stop();
        audioPlayer.clip = clip;
        audioPlayer.time = 0;
        audioPlayer.Play();
    }

    private void OnDisable()
    {
        // PlaySound();
        Instantiate(explosion, transform.position, Quaternion.identity);
        transform.position = Vector3.zero;
        rb.Sleep();
    }

    public void ShootBall(float angle, float speed)
    {
        shootDir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        shootSpeed = speed;
        rb.AddForce(shootDir * shootSpeed);
    }
    public void ShootBall(Vector3 dir, float speed)
    {
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
