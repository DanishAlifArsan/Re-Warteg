using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{
    [SerializeField] protected Color32 selectedImageColor;
    [SerializeField] protected Color32 selectedTextColor;
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI text;
    Color32 startingImageColor, startingTextColor;    

    public virtual void OnSelect() {
        startingImageColor = image.color;
        startingTextColor = text.color;
        image.color = selectedImageColor;
        text.color = selectedTextColor;
        AudioManager.instance.PlayButton();
    }

    public virtual void OnDeselect() {
        image.color = startingImageColor;
        text.color = startingTextColor;
        AudioManager.instance.StopButton();
    }

    public virtual void OnConfirm() {}
}
