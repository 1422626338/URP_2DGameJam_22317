using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 /// <summary>
 ///注释
 /// <summary>

public class GameManager : SingletonMono<GameManager>
{
    public GameObject p1;
    public GameObject p2;
    public GameObject GamingPanel;
    public GameObject GameWinPanel;

    public void HideGameObject()
    {
      
        p1.SetActive(false);
        p2.SetActive(false);
        GamingPanel.SetActive(false);
        GameWinPanel.SetActive(false);
    }

    public void SetActiveGameObject()
    {
        GamingPanel.SetActive(true);
        
    }

    public void OnGameWinEvent()
    {
        GameWinPanel.SetActive(true);
    }
}
