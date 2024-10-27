using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    // [SerializeField] private GameObject skipButton;
    // [SerializeField] private UnityEvent OnDialogueStart;
    [SerializeField] private UnityEvent OnDialogueEnd;

    // Start is called before the first frame update
    private void Start()
    {
        ConversationManager.OnConversationStarted += StartDialogue;
        ConversationManager.OnConversationEnded += EndDialogue;
    }

    private void EndConversation(UnityEngine.InputSystem.InputAction.CallbackContext context)   // trigger ketika ada input buat skip dialog
    {
        ConversationManager.Instance.EndConversation();
    }

    public void SomeEvent() {   // trigger ketika dialog muncul
        Debug.Log("event");
    }

    private void StartDialogue() {
        // OnDialogueStart?.Invoke();
        // skipButton.SetActive(true);
        InputManager.instance.playerInput.UI.Apply.performed += EndConversation;

    }

    private void EndDialogue() {    // trigger ketika dialog selesai
        // skipButton.SetActive(false);
        InputManager.instance.playerInput.UI.Apply.performed -= EndConversation;
        OnDialogueEnd?.Invoke();
    }
}
