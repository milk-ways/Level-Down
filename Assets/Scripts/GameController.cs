using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int hp;

    public bool jumpEnabled;                // 0: False, 1: True
    public bool dashEnabled;
    public bool meleeEnabled;
    public bool rangedEnabled;
    public bool skillEnabled;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);

        LoadGame();
    }

    public void LoadGame()
    {
        hp = PlayerPrefs.GetInt("HP");
        jumpEnabled = PlayerPrefs.GetInt("JumpEnabled") == 1;
        dashEnabled = PlayerPrefs.GetInt("DashEnabled") == 1;
        meleeEnabled = PlayerPrefs.GetInt("MeleeEnabled") == 1;
        rangedEnabled = PlayerPrefs.GetInt("RangedEnabled") == 1;
        skillEnabled = PlayerPrefs.GetInt("SkillEnabled") == 1;
    }

    public void SaveGame(int hp)
    {
        PlayerPrefs.SetInt("HP", hp);
        PlayerPrefs.SetInt("JumpEnabled", jumpEnabled ? 1 : 0);
        PlayerPrefs.SetInt("DashEnabled", dashEnabled ? 1 : 0);
        PlayerPrefs.SetInt("MeleeEnabled", meleeEnabled ? 1 : 0);
        PlayerPrefs.SetInt("RangedEnabled", rangedEnabled ? 1 : 0);
        PlayerPrefs.SetInt("SkillEnabled", skillEnabled ? 1 : 0);
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("HP", 10);
        PlayerPrefs.SetInt("JumpEnabled", 1);
        PlayerPrefs.SetInt("DashEnabled", 1);
        PlayerPrefs.SetInt("MeleeEnabled", 1);
        PlayerPrefs.SetInt("RangedEnabled", 1);
        PlayerPrefs.SetInt("SkillEnabled", 1);
    }
}
