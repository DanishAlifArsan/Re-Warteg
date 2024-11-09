using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] NPCConversation conversation;
    public void EndIntro() {
        GameManager.instance.LoadScene(2);
    }

    private void Awake() {
        ConversationManager.Instance.StartConversation(conversation);
    }
}
