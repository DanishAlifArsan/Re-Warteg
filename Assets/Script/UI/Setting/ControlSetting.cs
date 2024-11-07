using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSetting : SettingMenu
{
    [SerializeField] private Setting setting;
    public Toggle keyboardToggle;
    public Toggle controllerToggle;

    public override void Select() {
        
    }


    public override void Deselect() {
        
    }

    public override void OnConfirm()
    {
       if (keyboardToggle.isOn)
        {
            controllerToggle.isOn = true;
            keyboardToggle.isOn = false;
            setting.control = 1;
        } else {
            controllerToggle.isOn = false;
            keyboardToggle.isOn = true;
            setting.control = 0;
        }
    }
}
