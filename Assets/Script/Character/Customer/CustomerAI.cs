using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerAI : MonoBehaviour
{
    [SerializeField] private float range = 1f;
    [SerializeField] private Transform interactPoint;
    [SerializeField] private Transform platePos;
    [SerializeField] private Display display;
    [SerializeField] private GameObject bubbleTextObject;
    [SerializeField] private TextMeshProUGUI bubbleText;
    [SerializeField] private List<string> dialogue;
    public NavMeshAgent agent;
    public Transform cashierPoint;
    public Transform homePoint;
    public Table table;
    // public bool isWalking;
    public bool isBuying;
    public bool isEating;
    public bool isGetFood;
    public int maxNumberOfGoods;
    public float eatDuration;
    public float eatTimer;
    private StateManager stateManager;
    public Animator anim;
    public List<Food> foodToBuy = new List<Food>();
    public Plate plate;
    private bool setupFlag;

    private void OnEnable() {
        // isWalking = false;
        isBuying = false;
        isEating = false;
        isGetFood = false;
        setupFlag = true;
        CustomerManager.instance.isSpawned = true;
    }

    private void OnDisable() {
        stateManager = null;
        CustomerManager.instance.isSpawned = false;
    }

    private void Update() {
        if (setupFlag)
        {
            if (MenuManager.instance.listFoodOnSale.Count > 0)
            {
                Setup();
            } else {
                return;
            }
        }

        stateManager.currentState.UpdateState(this, stateManager);

        // if (isWalking)
        // {
        //     return;
        // }

        if (isEating)
        {
            eatTimer -= Time.deltaTime;
            if (eatTimer <= 0)
            {
                eatTimer = eatDuration;
                isEating = false;
                // isBuying = true;
                // isWalking = true;
            }
        }
    }

    private void Setup() {
        stateManager = new StateManager();
        stateManager.StartState(this);
        setupFlag = false;
    }

    public void SetFoodsToBuy() {
        foodToBuy = CustomerManager.instance.SetFoodsToBuy(maxNumberOfGoods);

        // int numberOfGoods = foodToBuy.Count;
        // for (int i = 0; i < numberOfGoods; i++)
        // {
        //     dialogueBubbles.Add(Instantiate(dialogueBubble, boxHolder));
        //     dialogueBubbles[i].Setup(foodToBuy.ElementAt(i));
        // }
        // boxHolder.anchoredPosition = new Vector3(98, (114 * (numberOfGoods - 1)) -14, 0);
    }

    public void ClearFoodsToBuy() {
        // foreach (var item in dialogueBubbles)
        // {
        //     Destroy(item.gameObject);
        // }
        // dialogueBubbles.Clear();
    }

    public int CountTotalPrice() {
        int totalPrice = 0;
        for (int i = 0; i < foodToBuy.Count; i++)
        {
            int price = foodToBuy.ElementAt(i).price;
            totalPrice += price;
        }
    
        return totalPrice;
    }

    public void SetPlate(Plate _plate) {
        plate = _plate;
        _plate.transform.parent = platePos;
        _plate.transform.position = Vector3.zero;
    }

    // public IState CurrentState() {
    //     return stateManager.currentState;
    // }

    private bool dialogueGenerated = true;

    private void LateUpdate() {
        if (CheckPlayer())
        {
            bubbleTextObject.SetActive(true);
            if (dialogueGenerated)
            {
                bubbleText.text = dialogue[Random.Range(0, dialogue.Count)];
                dialogueGenerated = false;
            }
        } else {
            bubbleTextObject.SetActive(false);
        }
    }

    private bool CheckPlayer() {
        Collider[] cols = Physics.OverlapSphere(interactPoint.position, range, LayerMask.GetMask("player"));
        return cols.Length > 0;
    }
}
