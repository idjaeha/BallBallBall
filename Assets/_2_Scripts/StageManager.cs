using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// 스테이지의 준비와 끝맺음을 해주는 객체
/// 
/// </Summary>
public class StageManager : MonoBehaviour
{
    private int stage;

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
        stage = 0;
        state = "Idle";
    }

    // 이것이 호출되면 다음 단계로 진행한다.    
    public void NextStage()
    {
        if (state.Equals("Playing"))
        {
            Clean();
        }
        stage++;
        Prepare();
    }

    private void Prepare()
    {
        state = "Preparing";

    }

    private void Play()
    {


    }

    private void Clean()
    {

    }

    public void HandleCrash()
    {

    }
}
