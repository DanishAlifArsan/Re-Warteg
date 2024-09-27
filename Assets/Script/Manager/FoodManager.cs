using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public List<Food> foodList;
    public static FoodManager instance;
    [SerializeField] QueueFood queueFood;
    [SerializeField] SelectionList selectionList;
    [SerializeField] Recipe recipePrefab;
    [SerializeField] RectTransform scrollContent;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start() {
        foreach (Food item in FoodManager.instance.foodList)
        {
            Recipe instantiatedRecipe =  Instantiate(recipePrefab, scrollContent);
            selectionList.uiSelections.Add(instantiatedRecipe);
            instantiatedRecipe.Setup(item, queueFood);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
