using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [Header("Generate Food")]
    public List<Food> foodList;
    public static FoodManager instance;
    [SerializeField] SelectionList selectionList;
    [SerializeField] Recipe recipePrefab;
    [SerializeField] RectTransform scrollContent;

    [Header("Generate Drop Item")]
    public List<DropItem> itemList;
    [SerializeField] SelectionList itemSelectionList;
    [SerializeField] ShopList shopListPrefab;
    [SerializeField] RectTransform shopScrollContent;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start() {
        foreach (Food item in foodList)
        {
            item.menuDisplay = null;
            Recipe instantiatedRecipe =  Instantiate(recipePrefab, scrollContent);
            selectionList.uiSelections.Add(instantiatedRecipe);
            instantiatedRecipe.Setup(item);
        }
        foreach (DropItem item in itemList)
        {
            ShopList instantiatedShopList =  Instantiate(shopListPrefab, shopScrollContent);
            itemSelectionList.uiSelections.Add(instantiatedShopList);
            instantiatedShopList.Setup(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
