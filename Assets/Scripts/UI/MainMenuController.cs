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

    
    public void LoadGame()
    {
        GameController.instance.LoadGame();
        SceneManager.LoadScene(2);      // Load map 1
    }

    public void NewGame()
    {
        GameController.instance.Reset();
        SceneManager.LoadScene(2);      // Load map 1
    }

    public void Setting()
    {
        settingPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
