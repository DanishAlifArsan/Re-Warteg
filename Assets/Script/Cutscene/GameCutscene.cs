using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class GameCutscene : MonoBehaviour
{
    [SerializeField] private GameObject gameplayObject;
    [SerializeField] private GameObject cutsceneObject;
    [SerializeField] private NPCConversation conversation;

    public void StartCutscene() {
        gameplayObject.SetActive(false);
        cutsceneObject.SetActive(true);
        PlayerInput playerInput = InputManager.instance.playerInput;
        playerInput.Player.Disable();
        playerInput.Dungeon.Disable();
        playerInput.UI.Enable();
        ConversationManager.Instance.StartConversation(conversation);
    }
}
