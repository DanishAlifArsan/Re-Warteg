using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : UISelection
{
    [SerializeField] private string recipeName;

    public string GetName() {
        return recipeName;
    }

    public override void OnConfirm()
    {
        Debug.Log(recipeName);
    }
}
