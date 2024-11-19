using System;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    bool endFlag = false;
    public void EndIntro() {
        if (endFlag)
        {
            return;
        }
        GameManager.instance.LoadScene(2);
        endFlag = true; 
        // SceneManager.LoadScene(2);
    }
    // [SerializeField] NPCConversation conversation;   // buat kalau pakai dialog

    // private void Start() {
    //     ConversationManager.Instance.StartConversation(conversation);
    // }

    //buat kalau pakai video
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start() {
        InputManager.instance.playerInput.UI.Action2.performed += SkipCutscene;
        videoPlayer.loopPointReached += EndCutscene;
    }

    private void EndCutscene(VideoPlayer source)
    {
        EndIntro();
    }

    private void SkipCutscene(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        EndIntro();
    }
}
