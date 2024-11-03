using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject dungeonObject;
    [SerializeField] private GameObject reviveObject;
    [SerializeField] private NPCConversation deathConversation;

    public void StartCutscene() {
        dungeonObject.SetActive(false);
        reviveObject.SetActive(true);
        PlayerInput playerInput = InputManager.instance.playerInput;
        playerInput.Player.Disable();
        playerInput.Dungeon.Disable();
        playerInput.UI.Enable();
        ConversationManager.Instance.StartConversation(deathConversation);
    }
}
