using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TestDIalogue : MonoBehaviour
{
    public NPCConversation conversation;
    // Start is called before the first frame update
    void Start()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }
}
