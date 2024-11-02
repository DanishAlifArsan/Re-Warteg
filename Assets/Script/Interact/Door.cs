using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private ConfirmMenu confirmDialogue;
    public void OnInteract()
    {
        confirmDialogue.OnConfirm += EndSession;
        confirmDialogue.gameObject.SetActive(true);
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

    private void EndSession() {
        GameManager.instance.EndSession();
    }
}
