using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System;

public class TestDIalogue : MonoBehaviour
{
    public NPCConversation conversation;
    // Start is called before the first frame update
    void Start()
    {
        ConversationManager.Instance.StartConversation(conversation);
        ConversationManager.OnConversationEnded += End;
        
        InputManager.instance.playerInput.UI.Apply.performed += EndConversation;

        
    }

    private void EndConversation(UnityEngine.InputSystem.InputAction.CallbackContext context)   // trigger ketika ada input buat skip dialog
    {
        ConversationManager.Instance.EndConversation();
    }

    public void SomeEvent() {   // trigger ketika dialog muncul
        Debug.Log("event");
    }

    private void End() {    // trigger ketika dialog selesai
        Debug.Log("ended");
    }
}
