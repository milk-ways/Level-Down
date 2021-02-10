using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    public GameObject settingPanel;

    private void Start()
    {
        settingPanel.SetActive(false);
    }
    public void exitSetting()
    {
        settingPanel.SetActive(false);
    }
}

