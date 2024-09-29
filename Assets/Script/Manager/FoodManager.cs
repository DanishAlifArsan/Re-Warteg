using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public List<Food> foodList;
    public static FoodManager instance;
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
        foreach (Food item in foodList)
        {
            item.menuDisplay = null;
            Recipe instantiatedRecipe =  Instantiate(recipePrefab, scrollContent);
            selectionList.uiSelections.Add(instantiatedRecipe);
            instantiatedRecipe.Setup(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
