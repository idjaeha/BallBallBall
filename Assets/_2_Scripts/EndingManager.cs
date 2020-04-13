using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    private GameObject clearTime;
    // Start is called before the first frame update
    void Start()
    {
        clearTime = GameObject.FindGameObjectWithTag("ClearTime");
        float record = GameManager.instance.playtime;
        clearTime.GetComponent<Text>().text = $"Your Record : {record:F3}s";
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }

        if (Application.isMobilePlatform && Input.GetMouseButtonDown(0))
        {
            ChangeScene();
        }

    }

    private void ChangeScene()
    {
        GameManager.instance.StartGame();
    }
}