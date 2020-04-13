using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    void Start()
    {

    }

    void Update()
    {
        ControlMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // other.GetComponent<NormalBall>().PlaySound();
            other.gameObject.SetActive(false);
            GameManager.instance.HandleMailbox("CrashBall");
        }
    }

    private void ControlMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(horizontal, vertical, 0).normalized;
        Vector3 pos = transform.position;
        pos += moveDir * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -GameManager.instance.mapSizeX, GameManager.instance.mapSizeX);
        pos.y = Mathf.Clamp(pos.y, -GameManager.instance.mapSizeY, GameManager.instance.mapSizeY);
        transform.position = pos;

    }

}
