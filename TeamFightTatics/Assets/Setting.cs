using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    bool _isOnSetting = false;

    public GameObject SettingUI;
    public void OpenSettingUI()
    {
        if(!_isOnSetting)
        {
            SettingUI.SetActive(true);
            _isOnSetting = true;
        }

        else
        {
            SettingUI.SetActive(false);
            _isOnSetting = false;
        }
    }
}
