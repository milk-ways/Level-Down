using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        DontDestroyOnLoad(this);
    }
    #endregion

    public Keybinds keybinds;

    public bool KeyDown(string key)
    {
        if (Input.GetKeyDown(keybinds.CheckKey(key)))
            return true;
        else
            return false;
    }

    public float HorizontalAxis()
    {
        if (Input.GetKey(keybinds.CheckKey("Right")))
            return 1;
        else if (Input.GetKey(keybinds.CheckKey("Left")))
            return -1;
        else
            return 0;
    }

    public float VerticalAxis()
    {
        if (Input.GetKey(keybinds.CheckKey("Up")))
            return 1;
        else if (Input.GetKey(keybinds.CheckKey("Down")))
            return -1;
        else
            return 0;
    }
}
