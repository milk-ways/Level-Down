using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingController : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject keyPanel;
    private KeyCode Akey;
    public Text[] txt;
    public string[] str;    //영어 키 명칭
    public string[] strKor; //한국어 키 명칭
    [SerializeField] Text keyname;
    int key = -1;
    private void Start()
    {
        for (int i=0; i<9; i++)
        {
            txt[i].text = InputManager.instance.keybinds.CheckKey(str[i]).ToString();
        }
        keyPanel.SetActive(false);              //임시
        //settingPanel.SetActive(false);        //임시
    }
    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey && key >= 0)
        {
            keyPanel.SetActive(false);
            Akey = keyEvent.keyCode;
            for (int i =0; i<9; i++)
            {
                if(InputManager.instance.keybinds.CheckKey(str[i]) == Akey && i != key)
                {
                    InputManager.instance.keybinds.ChangeKey(str[i], KeyCode.None);
                    txt[i].text = "";
                }
            }
            InputManager.instance.keybinds.ChangeKey(str[key], Akey);
            txt[key].text = Akey.ToString();
            key = -1;
        }
    }
    public void ChangeKeyButton(int num)
    {
        keyPanel.SetActive(true);
        key= num;
        keyname.text = strKor[num];
    }
    public void exitSetting()
    {
        settingPanel.SetActive(false);
    }
}

