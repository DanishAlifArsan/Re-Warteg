using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    // [SerializeField] private NPCConversation conversation;
    [SerializeField] private UnityEvent OnDialogueStart;
    [SerializeField] private UnityEvent OnDialogueEnd;
    // Start is called before the first frame update
    void Start()
    {
        // ConversationManager.Instance.StartConversation(conversation);
        ConversationManager.OnConversationStarted += StartDialogue;
        ConversationManager.OnConversationEnded += EndDialogue;
        
        // InputManager.instance.playerInput.UI.Apply.performed += EndConversation;
    }

    private void EndConversation(UnityEngine.InputSystem.InputAction.CallbackContext context)   // trigger ketika ada input buat skip dialog
    {
        ConversationManager.Instance.EndConversation();
    }

    public void SomeEvent() {   // trigger ketika dialog muncul
        Debug.Log("event");
    }

    private void StartDialogue() {
        OnDialogueStart?.Invoke();
    }

    private void EndDialogue() {    // trigger ketika dialog selesai
        OnDialogueEnd?.Invoke();
    }
}
