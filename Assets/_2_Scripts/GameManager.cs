using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float playerSpeed;
    private Vector3 playerDir;
    private bool isKeyDown;
    private float mapSizeX;
    private float mapSizeY;
    // Start is called before the first frame update
    void Start()
    {
        GameData.playtime = 0.0f;
        playerDir = new Vector3(0, 0, 0);
        isKeyDown = false;
        player = Instantiate(player, Vector3.zero, Quaternion.identity);
        playerSpeed = 30.0f;
        mapSizeX = 14;
        mapSizeY = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnd())
        {
            SceneManager.LoadScene("Ending");
        }
        else
        {
            GameData.playtime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        ControlMove();
    }

    private void ControlMove()
    {
        Vector3 pos = player.transform.position;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        pos += playerSpeed * Time.deltaTime * new Vector3(horizontal, vertical, 0);
        pos.x = Mathf.Abs(pos.x) <= mapSizeX ? pos.x : -pos.x / Mathf.Abs(pos.x) * mapSizeX;
        pos.y = Mathf.Abs(pos.y) <= mapSizeY ? pos.y : -pos.y / Mathf.Abs(pos.y) * mapSizeY;
        player.transform.position = pos;
    }

    private bool isGameEnd()
    {
        var targets = GameObject.FindGameObjectsWithTag("Target");
        //return targets.Length == 0;
        return false;
    }
}
