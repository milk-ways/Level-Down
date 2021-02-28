using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject menuPanel;
    public Sprite[] button;

    
    public void LoadGame(Button btn)
    {
        btn.image.sprite = button[1];        
        menuPanel.SetActive(false);
    }

    public void NewGame(Button btn)
    {
        btn.image.sprite = button[1];
        menuPanel.SetActive(false);
    }

    public void Setting(Button btn)
    {
        btn.image.sprite = button[1];
        settingPanel.SetActive(true);
    }

    public void Exit(Button btn)
    {
        btn.image.sprite = button[1];
        Application.Quit();
    }
}
