using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject menuPanel;

    public void LoadGame()
    {
        menuPanel.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Hajin");
        menuPanel.SetActive(false);
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
