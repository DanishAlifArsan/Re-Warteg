using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{
    [SerializeField] private Color32 selectedImageColor;
    [SerializeField] private Color32 selectedTextColor;
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI text;
    Color32 startingImageColor, startingTextColor;    

    public virtual void OnSelect() {
        startingImageColor = image.color;
        startingTextColor = text.color;
        image.color = selectedImageColor;
        text.color = selectedTextColor;
    }

    public virtual void OnDeselect() {
        image.color = startingImageColor;
        text.color = startingTextColor;
    }

    public virtual void OnConfirm() {}
}
