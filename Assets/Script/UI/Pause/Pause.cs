using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    PlayerInput playerInput;
    private void Start() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Pause.performed += OpenPause;
    }

    private void OpenPause(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        pauseUI.SetActive(true);
        playerInput.UI.Enable();
        playerInput.Player.Disable();
        Time.timeScale = 0;
        playerInput.UI.Cancel.performed += Continue;
        playerInput.Player.Pause.performed -= OpenPause;
    }

    public void Continue(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Resume();
    }

    public void Resume() {
        pauseUI.SetActive(false);
        playerInput.UI.Disable();
        playerInput.Player.Enable();
        Time.timeScale = 1;
        playerInput.UI.Cancel.performed -= Continue;
        playerInput.Player.Pause.performed += OpenPause;
    }

    public void Restart() {
        Resume();
        GameManager.instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit() {
        GameManager.instance.LoadScene(0);
    }
}
