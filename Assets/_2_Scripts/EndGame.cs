using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private GameObject clearTime;
    // Start is called before the first frame update
    void Start()
    {
        clearTime = GameObject.FindGameObjectWithTag("ClearTime");
        string record = GameData.playtime.ToString();
        clearTime.GetComponent<Text>().text = $"Your Record : {record:F3}s";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Loading");
        }

        if (Application.isMobilePlatform && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Loading");
        }

    }
}
