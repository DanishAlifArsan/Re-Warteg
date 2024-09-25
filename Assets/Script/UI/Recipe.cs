using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    [SerializeField] private string recipeName;
    [SerializeField] private Color32 selectedImageColor;
    [SerializeField] private Color32 selectedTextColor;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    Color32 startingImageColor, startingTextColor;    

    public void Select() {
        startingImageColor = image.color;
        startingTextColor = text.color;
        image.color = selectedImageColor;
        text.color = selectedTextColor;
    }

    public void Deselect() {
        image.color = startingImageColor;
        text.color = startingTextColor;
    }

    public void Confirm() {
        Debug.Log(recipeName);
    }
}
