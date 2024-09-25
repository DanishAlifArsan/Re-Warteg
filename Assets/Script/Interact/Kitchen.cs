using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour, Interactable
{
    public void OnInteract()
    {
        Debug.Log(name);
    }
}
