using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScroll : SelectionList
{
    [SerializeField] ScrollRect scrollRect;

    protected override void OnEnable() {
        base.OnEnable();
        RectTransform contentPanel = scrollRect.content;
        contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, 0);
    }

    protected override void Navigate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        base.Navigate(context);
        //automatic scrolling
        RectTransform selectedRectTransform = uiSelections[currentIndex].transform as RectTransform;
        RectTransform scrollRectTransform = scrollRect.transform as RectTransform;
        RectTransform contentPanel = scrollRect.content;
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;
        // The upper bound of the scroll view is the anchor position of the content we're scrolling.
        float scrollViewMinY = contentPanel.anchoredPosition.y;
        // The lower bound is the anchor position + the height of the scroll rect.
        float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;
        // If the selected position is below the current lower bound of the scroll view we scroll down.
        if (selectedPositionY > scrollViewMaxY) {
            float newY = selectedPositionY - scrollRectTransform.rect.height;
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        }
        // If the selected position is above the current upper bound of the scroll view we scroll up.
        else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY) {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, 0);
        }
    }
}
