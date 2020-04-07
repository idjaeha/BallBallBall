using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GameData.playtime = 0.0f;
        GameData.gameover = false;
        player = Instantiate(player, Vector3.zero, Quaternion.identity) as Player;
        playerSpeed = 30.0f;

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


    }


    private bool isGameEnd()
    {
        return GameData.gameover;
    }
}
