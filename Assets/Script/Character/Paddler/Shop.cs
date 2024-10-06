using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI ownedText;
    [SerializeField] private Image itemImage;
    PlayerInput playerInput;
    private void OnEnable() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Disable();
        playerInput.UI.Enable();
        playerInput.UI.Cancel.performed += Cancel;
    }

    public void SelectItem(UISelection uISelection) {
        ShopList shopList = uISelection as ShopList;
        itemImage.color = Color.white;
        itemImage.sprite = shopList.item.itemImage;
        nameText.text = shopList.item.itemName;
        descriptionText.text = shopList.item.desc;
        shopList.SetOwned(ownedText);
    }

    public void DeselectItem(UISelection uISelection) {
        itemImage.color = new Color(1,1,1,0);
        nameText.text =""; 
        descriptionText.text =""; 
        ownedText.text =""; 
    }

    private void LateUpdate() {
        currencyText.text = CurrencyManager.instance.totalCurrency.ToString();
    }

    private void Cancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        playerInput.Player.Enable();
        playerInput.UI.Disable();
    }
}
