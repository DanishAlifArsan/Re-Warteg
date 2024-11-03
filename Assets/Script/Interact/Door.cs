using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private ConfirmMenu confirmDialogue;
    public void OnInteract()
    {
        // confirmDialogue.gameObject.SetActive(true);
        // confirmDialogue.OnConfirm += EndSession;

        EndSession();
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
