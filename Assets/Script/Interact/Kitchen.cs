using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour, Interactable
{
    [SerializeField] PlayerInteract player;
    [SerializeField] CookFood kitchenUI;
    [SerializeField] GameObject cookIndicator;
    [SerializeField] AudioClip cleanSound;

    private void Update() {
        cookIndicator.SetActive(QueueFood.instance.cookedQueue.Count > 0);
    }

    public string FlavorText()
    {
        if (QueueFood.instance.cookedQueue.Count > 0 && player.itemInHand == null)
        {
            return "Get Food";
        } else if (player.itemInHand?.itemType == ItemType.Plate) {
            return "Clean"; 
        } else {
            return "Cook";
        }
    }

    public void OnInteract()
    {
        if (QueueFood.instance.cookedQueue.Count > 0 && player.itemInHand == null)
        {
            player.itemInHand = QueueFood.instance.RemoveFromQueue();
            player.nampan.SetActive(true);
        } else if (player.itemInHand?.itemType == ItemType.Plate) {
            AudioManager.instance.PlaySound(cleanSound);
            PlateManager.instance.Wash(player.itemInHand as Plate);
            player.itemInHand = null;
        } else {
            kitchenUI.gameObject.SetActive(true);
        }
    }
}
