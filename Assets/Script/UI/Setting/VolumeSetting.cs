using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : UISelection
{
    [SerializeField] private string type;
    [SerializeField] private Setting setting;
    [SerializeField] private float range;
    public Slider volumeSlider;
    PlayerInput playerInput;

    public void Select() {
        playerInput = InputManager.instance.playerInput;
        playerInput.UI.Navigate.performed += StartSetting;
    }


    public void Deselect() {
        playerInput.UI.Navigate.performed -= StartSetting;
    }
    public override void OnConfirm()
    {
       
    }
    private void StartSetting(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 pos = playerInput.UI.Navigate.ReadValue<Vector2>();
        SetVolume(pos.x);
    }

    public void SetVolume(float _volume) {
        float vol = _volume * range;
        volumeSlider.value += vol;
        if (type.Equals("bgm") )
        {
            setting.bgm = Mathf.Clamp(setting.bgm + vol, -80, 0);
        } else {
            setting.sfx = Mathf.Clamp(setting.sfx + vol, -80, 0);
        }
    }

}
