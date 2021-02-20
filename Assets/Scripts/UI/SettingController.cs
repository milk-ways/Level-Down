using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingController : MonoBehaviour
{
    public GameObject keyPanel;
    public Text[] keyText;              // Key texts
    public string[] keyNames;           // Name of the keys (Up, Down, Left, Right, Jump, Dash, Swap, Fire1, Fire2)
    bool esc;

    string changeKeyName = "";
    bool changeKey = false;

    void Start()
    {
        ShowKeyNames();
        //keyPanel.SetActive(false);              //임시
        gameObject.SetActive(false);        //임시
    }

    void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey && changeKey &&keyEvent.keyCode != KeyCode.Escape)        // Key is pressed and need to change key
        {
            KeyCode pressedKey = keyEvent.keyCode;
            // Check if pressed key is being used
            for (int i = 0; i < keyNames.Length; i++)
            {
                // If pressed key exists
                if (InputManager.instance.keybinds.CheckKey(keyNames[i]) == pressedKey && keyNames[i] != changeKeyName)
                {
                    InputManager.instance.keybinds.ChangeKey(keyNames[i], KeyCode.None);
                }
            }
            InputManager.instance.keybinds.ChangeKey(changeKeyName, pressedKey);
            ShowKeyNames();

            keyPanel.SetActive(false);
            changeKey = false;
        }
    }
    private void Update()
    {
        esc = Input.GetKeyDown(KeyCode.Escape);
        if (changeKey && esc)
        {
            keyPanel.SetActive(false);
            changeKey = false;
            esc = false;
        }
        else if(!changeKey && esc)
        {
            exitSetting();
            esc = false;
        }
    }

    void ShowKeyNames()
    {
        for (int i = 0; i < keyNames.Length; i++)
        {
            keyText[i].text = InputManager.instance.keybinds.CheckKey(keyNames[i]).ToString();
        }
    }

    public void ChangeKeyButton()
    {
        keyPanel.SetActive(true);
        changeKey = true;               // Need to change key
        changeKeyName = EventSystem.current.currentSelectedGameObject.name;
    }

    public void exitSetting()
    {
        gameObject.SetActive(false);
    }
}

