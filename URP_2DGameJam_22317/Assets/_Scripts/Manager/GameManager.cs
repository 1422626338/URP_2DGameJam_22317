using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;


/// <summary>
///注释
/// <summary>

public class GameManager : SingletonMono<GameManager>
{
    public GameObject p1;
    public GameObject p2;
    public GameObject GamingPanel;
    public GameObject GameWinPanel;
    public GameObject GameOverPanel;
    public GameObject StaffPanel;
    public GameObject Light2D;

    public Light2D light2D;
    private void OnEnable()
    {
        Light2D.SetActive(true);
        light2D = Light2D.GetComponent<Light2D>();
        light2D.intensity = 1;
    }

    private void Update()
    {
       
    }
    public void HideGameObject()
    {
      
        p1.SetActive(false);
        p2.SetActive(false);
        GamingPanel.SetActive(false);
        GameWinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void SetActiveGameObject()
    {
        GamingPanel.SetActive(true);
        
    }

    public void OnGameWinEvent()
    {
        GameWinPanel.SetActive(true);
    }

    public void OnGameOverEvent()
    {
        GameOverPanel.SetActive(true);
    }

    public void ONStaffEvent()
    {
        StaffPanel.SetActive(true);
    }

    public void setLigth2DOpen()
    {
        light2D.intensity = 1.0f;
    }

    public void setLigth2DClose()
    {
        light2D.intensity = 0.03f;
    }

    
}
