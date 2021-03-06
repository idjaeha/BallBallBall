﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// 스테이지의 준비와 끝맺음을 해주는 객체
/// 
/// </Summary>
public class StageManager : MonoBehaviour
{
    public int stage;
    public GameObject[] maps;
    private GameObject currentMap;
    private const int FINAL_STAGE = 3;
    public SpawnerManager rawSpawnerManager;
    private SpawnerManager spawnerManager;

    /// <Summary>
    /// stage는 총 4가지의 형태가 있다.
    /// 1.Idle : Preparing, Playing, Cleaning 상태가 아닌 상태를 의미한다.
    /// 2.Prepareing : 스테이지 준비과정을 의미한다.
    /// 3.Playing : 게임 진행 과정을 의미한다.
    /// 4.Cleaning : 게임이 마무리되어 정리하는 과정을 의미한다.
    /// </Summary>
    private string state;
    private void Awake()
    {
        stage = -1;
        state = "Idle";
        spawnerManager = Instantiate(rawSpawnerManager, Vector3.zero, Quaternion.identity) as SpawnerManager;
    }

    // 이것이 호출되면 다음 스테이지로 진행한다.    
    public void NextStage()
    {
        if (state.Equals("Playing"))
        {
            Clean();
        }
        stage++;
        if (stage == FINAL_STAGE)
        {
            GameManager.instance.EndPlay();
        }
        else
        {
            Prepare();
        }
    }

    private void Prepare()
    {
        state = "Preparing";
        CreateMap(stage);
        spawnerManager.CreateSpawners(stage);
        Play();
    }

    private void Play()
    {
        state = "Playing";
        spawnerManager.StartSpawner();
    }

    public void Clean()
    {
        state = "Cleaning";
        spawnerManager.CleanSpawners();
        CleanMap();
    }

    public void HandleCrash()
    {
        if (--GameManager.instance.playerLife < 0) GameManager.instance.EndPlay();
    }

    private void CreateMap(int stage)
    {
        currentMap = Instantiate(maps[0], Vector3.zero, Quaternion.identity);
    }

    private void CleanMap()
    {
        Destroy(currentMap);
        GameManager.instance.ClearBalls();
    }
}
