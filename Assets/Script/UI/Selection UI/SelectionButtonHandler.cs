using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButtonHandler : MonoBehaviour
{
    public void SelectButton(UISelection uISelection) {
        SelectionButton button = uISelection as SelectionButton;
        button.indicator.SetActive(true);
    }
    public void DeselectButton(UISelection uISelection) {
        SelectionButton button = uISelection as SelectionButton;
        button.indicator.SetActive(false);
    }
}
