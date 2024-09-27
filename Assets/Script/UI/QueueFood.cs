using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class QueueFood : MonoBehaviour
{
    private Queue<FoodList> foodQueue = new Queue<FoodList>();
    [SerializeField] private List<FoodList> foodList;
    [SerializeField] TextMeshProUGUI recipeDuration;
    [SerializeField] TextMeshProUGUI queueText;
    [SerializeField] Image recipeImage;
    [SerializeField] List<Material> materialList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()       // benerin bug ini cuma jalan kalau ui nya aktif
    {
        if (foodQueue.Count > 0)
        {
            FoodList cookedFood = foodQueue.Peek();
            cookedFood.StartCooking();
            if (cookedFood.isFinished)
            {
                foodQueue.Dequeue();
            }
        }
    }

    public void AddToQueue(Food food) {
        if (foodQueue.Count == foodList.Count)
        {
            return;
        }
        FoodList activeFoodList = foodList.First(s => s.isEmpty);
        activeFoodList.Setup(food);
        foodQueue.Enqueue(activeFoodList);
        queueText.text = "Queue ("+foodQueue.Count + "/" + foodList.Count+")";     // benerin bug jumlahnya gak ngesave
    }

    public void RemoveFromQueue(Food food) { // todo 

    }

    public void SelectFood(UISelection uISelection) {
        FoodList foodList = uISelection.GetComponent<FoodList>();
        if (!foodList.isEmpty)
        {
            recipeImage.color = Color.white;
            recipeImage.sprite = foodList.food.foodImage;
            recipeDuration.text = foodList.remainingTime <= 0? "finished" : "duration"+(int) foodList.remainingTime+"s";

            for (int i = 0; i < Mathf.Min(foodList.food.materialsItem.Count,materialList.Count)  ; i++)
            {
                materialList[i].Setup(foodList.food.materialsItem[i],foodList.food.materialsCount[i]);
            }    
        }
    }

    public void DeselectFood(UISelection uISelection) {
        recipeImage.color = new Color(1,1,1,0);
        recipeDuration.text ="";

        for (int i = 0; i < materialList.Count  ; i++)
        {
            materialList[i].InActive();
        }
    }
} 
