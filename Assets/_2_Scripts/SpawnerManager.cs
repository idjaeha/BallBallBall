using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <Summary>
/// Stage별로 spawner를 생성, 제거를 도와주는 매니저
/// </Summary>
public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawnerGroup;
    private GameObject currentSpawnerGroup;
    private List<GameObject> runningSpawners;
    private void Awake()
    {
        runningSpawners = new List<GameObject>();
    }

    // 스테이지 별로 정해진 스포너들을 생성한다.
    // 생성 후에 스포너들은 runningSpawner를 통해 관리한다.
    public void CreateSpawners(int stage)
    {
        currentSpawnerGroup = Instantiate(spawnerGroup[stage], Vector3.zero, Quaternion.identity);
        int spawnersCount = currentSpawnerGroup.transform.childCount;

        for (int idx = 0; idx < spawnersCount; idx++)
        {
            runningSpawners.Add(currentSpawnerGroup.transform.GetChild(idx).gameObject);
        }
    }

    // 스포너의 동작(스폰 행위)를 멈추고 삭제해야한다.
    // 배열을 비워주고 해당 스폰 그룹 자체를 삭제한다.
    public void CleanSpawners()
    {
        foreach (var runningSpawner in runningSpawners)
        {
            runningSpawner.GetComponent<Spawner>().StopSpawn();
            Destroy(runningSpawner);
        }
        runningSpawners.Clear();
        Destroy(currentSpawnerGroup);
    }

    public void StartSpawner()
    {
        foreach (var runningSpawner in runningSpawners)
        {
            runningSpawner.GetComponent<Spawner>().StartSpawn();
        }
    }


}
