using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void OnEndClick()
    {
        GameManager.instance.EndPlay();
    }

    public void OnStartClick()
    {
        GameManager.instance.togglePause();
    }

}
