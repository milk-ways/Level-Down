using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class SettingController : MonoBehaviour
{
    [Header("Keybinds")]
    public GameObject keyPanel;
    public Text[] keyText;              // Key texts
    public string[] keyNames;           // Name of the keys (Up, Down, Left, Right, Jump, Dash, Swap, Fire1, Fire2)
    string changeKeyName = "";
    [SerializeField] bool changeKey = false;

    [Header("Audio")]
    public AudioMixer bgAudioMixer;
    public AudioMixer sfxAudioMixer;

    public Sprite[] button;
    public Button settingButton;
    Button btn;    
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
            btn.image.sprite = button[0];
            changeKey = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (changeKey)
            {
                keyPanel.SetActive(false);
                btn.image.sprite = button[0];
                
            }
            else
            {
                exitSetting();
            }
        }
    }

    void ShowKeyNames()
    {
        for (int i = 0; i < keyNames.Length; i++)
        {
            keyText[i].text = InputManager.instance.keybinds.CheckKey(keyNames[i]).ToString();
        }
    }

    public void ChangeKeyButton(Button but)
    {
        btn = but;
        but.image.sprite = button[1];
        keyPanel.SetActive(true);
        changeKey = true;               // Need to change key
        changeKeyName = EventSystem.current.currentSelectedGameObject.name;        
    }

    public void SetBGMusicVolume(float volume)
    {
        bgAudioMixer.SetFloat("Volume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioMixer.SetFloat("Volume", volume);
    }

    public void exitSetting()
    {
        settingButton.image.sprite = button[0];
        gameObject.SetActive(false);
    }
}

