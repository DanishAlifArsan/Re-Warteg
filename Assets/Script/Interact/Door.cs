using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{

    public void OnInteract()
    {
        GameManager.instance.EndSession();
    }

    public string FlavorText()
    {
        if (GameManager.instance.currentSession == GameSession.Dungeon)
        {
            return "Enter";
        }   else {
            return "Leave";
        }
    }
}
