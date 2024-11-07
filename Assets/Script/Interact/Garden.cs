using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour, Interactable
{
    [SerializeField] private List<DropItem> item;
    [SerializeField] private AudioClip pickSound;
    public string FlavorText()
    {
       return "Pick";
    }

    public void OnInteract()
    {
        gameObject.SetActive(false);
        int randItem = Random.Range(0, item.Count);
        int randAmount = Random.Range(5, 11);

        Inventory.instance.AddItem(item[randItem], randAmount);
        GardenManager.instance.Pickup(item[randItem], randAmount);

        PlayerHealth.instance.SetPrevItem(item[randItem]);

        AudioManager.instance.PlaySound(pickSound);
    }
}
