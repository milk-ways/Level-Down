using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }
    #endregion

    public bool jumpEnabled;
    public bool dashEnabled;
    public bool meleeEnabled;
    public bool rangedEnabled;
    public bool skillEnabled;
}
