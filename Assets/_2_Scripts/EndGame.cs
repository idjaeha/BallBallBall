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
        clearTime.GetComponent<Text>().text = $"Your Record : {record}s";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Loading");
        }
        
    }
}
