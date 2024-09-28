using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour, Interactable, HoldItem
{
    [SerializeField] private PlayerInteract player;
    [SerializeField] private ItemType type = ItemType.Plate;
    public ItemType itemType { get => type; set => itemType = type; }

    public string FlavorText()
    {
        return "Take";
    }

    public void OnInteract()
    {
        if (player.itemInHand == null)
        {
            gameObject.SetActive(false);      // ganti dengan pindah piring ke player
            PlateManager.instance.TakePlate(this);
            player.itemInHand = this;
        }
    }
}
