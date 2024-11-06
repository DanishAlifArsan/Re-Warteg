using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public VolumeSetting bgmSetting;
    public VolumeSetting sfxSetting;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private SelectionButton closeButton;
    public float bgm;
    public float sfx;

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

    public void SelectSetting(UISelection uISelection) {
        VolumeSetting volumeSetting = uISelection.GetComponent<VolumeSetting>();
        if (volumeSetting != null)
        {
            volumeSetting.Select();
        }
    }

    public void DeselectSetting(UISelection uISelection) {
        VolumeSetting volumeSetting = uISelection.GetComponent<VolumeSetting>();
        if (volumeSetting != null)
        {
            volumeSetting.Deselect();
        }
    }
    

    public void SaveSetting() {
        audioMixer.SetFloat("bgm",bgm);
        audioMixer.SetFloat("sfx",sfx);
        PlayerPrefs.SetFloat("bgm", bgm);
        PlayerPrefs.SetFloat("sfx", sfx);
        closeButton.OnConfirm();
    }
}
