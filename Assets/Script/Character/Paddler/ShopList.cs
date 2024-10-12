using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : UISelection
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image recipeImage;
    private List<Material> materials = new List<Material>();
    public DropItem item;
    TextMeshProUGUI ownedText;

    public void Setup(DropItem _item) {
        item = _item;
        nameText.text = item.itemName;
        recipeImage.sprite = item.itemImage;
        priceText.text = item.price.ToString();
    }   

    public override void OnConfirm()
    {
        if (CurrencyManager.instance.totalCurrency >= item.price)
        {
            CurrencyManager.instance.RemoveCurrency(item.price);
            if (item.type == DropType.Item)
            {
                Inventory.instance.AddItem(item, 1);
            } else {
                PlayerHealth.instance.AddCoffee(1);
            }
            SetOwned(ownedText);
            
        } else {
            Debug.Log("Not enough money");
        }
    }

    public void SetOwned(TextMeshProUGUI _ownedText) {
        ownedText = _ownedText;
        int amount = item.type == DropType.Item? Inventory.instance.GetItemCount(item) : PlayerHealth.instance.coffeeAmount;
        ownedText.text = "Owned: " + amount;
    }
}
