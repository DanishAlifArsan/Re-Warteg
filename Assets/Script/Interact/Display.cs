using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class Display : MonoBehaviour, Interactable
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera displayCamera;
    [SerializeField] ServeFood serveFoodUI; 
    [SerializeField] PlayerInteract player;
    [SerializeField] List<MenuDisplay> menuDisplays;

    // Start is called before the first frame update
    private void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract() {
        if (player.itemInHand?.itemType == ItemType.Food)
        {
            Food food = player.itemInHand as Food;
            try
            {
                menuDisplays.First(s => s.food != null && s.food.foodName.Equals(food.foodName));
            }
            catch (System.Exception)
            {
                menuDisplays.First(s=> s.food == null).Setup(food);
            }
            MenuManager.instance.GenerateList(food);
            player.itemInHand = null;
            TutorialManager.instance?.NextTutorial(3);
        } else {
            Plate plate = PlateManager.instance.PrepareFood();
            if (plate != null)
            {
                SetupCamera(true);
                serveFoodUI.Setup(this, plate);
            } else {
                Debug.Log("No more plate T-T. Please cleaning first");  // ganti jadi warning
            }
        }
    }

    public string FlavorText()
    {
        if (player.itemInHand?.itemType == ItemType.Food)
        {
            return "Place";
        } else {
            return "serve";
        } 
    }

    public void SetupCamera(bool status) {
        mainCamera.gameObject.SetActive(!status);
        displayCamera.gameObject.SetActive(status);
    }
}
