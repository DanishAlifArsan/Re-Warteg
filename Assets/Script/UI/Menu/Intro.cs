using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] NPCConversation conversation;
    public void EndIntro() {
        // GameManager.instance.LoadScene(2);
        SceneManager.LoadScene(2);
    }

    private void Start() {
        ConversationManager.Instance.StartConversation(conversation);
    }
}
