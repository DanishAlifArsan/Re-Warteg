using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour, Interactable
{
    [SerializeField] CookFood kitchenUI;

    public string FlavorText()
    {
        return "Cook";
    }

    public void OnInteract()
    {
        kitchenUI.gameObject.SetActive(true);
    }
}
