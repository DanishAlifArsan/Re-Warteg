using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public VolumeSetting bgmSetting;
    public VolumeSetting sfxSetting;
    public ControlSetting controlSetting;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private SelectionButton closeButton;
    public float bgm;
    public float sfx;
    public int control;

    // public void SetMusic(float _volume) {
    //     bgm = _volume;
    // }

    // public void SetSFX(float _volume) {
    //     sfx = _volume;
    // }

    public void SetMusicMixer(float _volume) {
        bgm = _volume;
        audioMixer.SetFloat("bgm",_volume);
    }
    public void SetSFXMixer(float _volume) {
        sfx = _volume;
        audioMixer.SetFloat("sfx",_volume);
    }

    // public void SetControl(int _control) {
    //     control = _control;
    //     if (_control > 0)
    //     {
    //         controlSetting.controllerToggle.isOn = true;
    //         controlSetting.keyboardToggle.isOn = false;
    //     } else {
    //         controlSetting.controllerToggle.isOn = false;
    //         controlSetting.keyboardToggle.isOn = true;
    //     } 
    // }

    public void SelectSetting(UISelection uISelection) {
        SettingMenu setting = uISelection.GetComponent<SettingMenu>();
        if (setting != null)
        {
            setting.Select();
        }
    }

    public void DeselectSetting(UISelection uISelection) {
        SettingMenu setting = uISelection.GetComponent<SettingMenu>();
        if (setting != null)
        {
            setting.Deselect();
        }
    }
    
    public void SaveSetting() {
        audioMixer.SetFloat("bgm",bgm);
        audioMixer.SetFloat("sfx",sfx);
        PlayerPrefs.SetFloat("bgm", bgm);
        PlayerPrefs.SetFloat("sfx", sfx);
        // PlayerPrefs.SetInt("control", control);
        // InputManager.instance.DeviceChange();
        closeButton.OnConfirm();
    }
}
