using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Setting settingScreen;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip pressStartSound;
    [SerializeField] private CanvasGroup continueButton;
    GameData data;
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;
        Setup();

        data = SaveManager.instance.LoadGame();
        // if (data == null)
        // {
        //     GameManager.instance.LoadScene(1);  // pindah ke scene tutorial
        // }
        if (data == null) {
            continueButton.alpha = 0.5f;
        }
        button.SetActive(true);
        InputManager.instance.playerInput.UI.Apply.performed += PressStart;
    }

    private void PressStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        AudioManager.instance.PlaySound(pressStartSound);
        anim.SetTrigger("move");
        InputManager.instance.playerInput.UI.Apply.performed -= PressStart;
    }

    private void Setup() {
        float bgm =  PlayerPrefs.GetFloat("bgm", -10f);
        settingScreen.bgmSetting.volumeSlider.value = bgm;
        settingScreen.SetMusicMixer(bgm);

        float sfx =  PlayerPrefs.GetFloat("sfx", -10f);
        settingScreen.sfxSetting.volumeSlider.value = sfx;
        settingScreen.SetSFXMixer(sfx);

        int control = PlayerPrefs.GetInt("control", 0);
        settingScreen.SetControl(control);
    }

    public void NewGame() {
        SaveManager.instance.NewGame();
        GameManager.instance.LoadScene(3);  // pindah ke scene tutorial
    }

    public void Play() {
        if (data == null) { // kalau gak jadi ada tutorial
            return;
        }
        else 
        if (data.isWarteg)
        {
            GameManager.instance.LoadScene(2);
        } else {
            GameManager.instance.LoadScene(1);
        }
    }

    public void Quit() {
        SaveManager.instance.NewGame(); // karena save error jadinya kalau quit auto new game
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Credit(bool status) {
        button.SetActive(!status);
        creditScreen.SetActive(status);
        if (status)
        {
            InputManager.instance.playerInput.UI.Cancel.performed += CloseCredit;
        } else {
            InputManager.instance.playerInput.UI.Cancel.performed -= CloseCredit;
        }
    }

    private void CloseCredit(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Credit(false);
    }

    public void OpenSetting(bool status) {
        button.SetActive(!status);
        settingScreen.gameObject.SetActive(status);
    }
}
