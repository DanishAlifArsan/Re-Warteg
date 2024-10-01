using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<Customer> customerList;
    [SerializeField] private float spawnInterval;
    private float spawnTimer = 0;
    public Transform homePoint;
    public Transform cashierPoint;
    public List<Transform> chairPoint;
    public static CustomerManager instance;
    public bool isSpawned = false;
    public CustomerAI currentCustomer;
    private List<CustomerAI> customerQueue = new List<CustomerAI>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start() {
        spawnTimer = spawnInterval;

        for (int i = 0; i < customerList.Count; i++)
        {
            Customer customer = customerList[i];
            CustomerAI instantiatedCustomer =  Instantiate(customer.prefab, homePoint.position, Quaternion.identity);
            SetupCustomer(instantiatedCustomer, customer, homePoint);
        }
    }

    private void Update() {
        if (currentCustomer == null)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0 && !isSpawned)
            {   
                SpawnCustomer();
                spawnTimer = spawnInterval;
            }
        }
    }

    private void SetupCustomer(CustomerAI instantiatedCustomer, Customer origin, Transform homePoint) {
        instantiatedCustomer.cashierPoint = cashierPoint;
        instantiatedCustomer.homePoint = homePoint;
        instantiatedCustomer.agent.speed = origin.walkSpeed;
        instantiatedCustomer.agent.acceleration = origin.acceleration;
        instantiatedCustomer.maxNumberOfGoods = origin.maxNumberOfFoods;
        instantiatedCustomer.eatDuration = origin.eatDuration;
        instantiatedCustomer.gameObject.SetActive(false);
        customerQueue.Add(instantiatedCustomer);
    }

    public List<Food> SetFoodsToBuy(int maxNumberOfGoods) {
        List<Food> foodsToBuy = new List<Food>();
        for (int i = 0; i < SetNumberOfFoods(maxNumberOfGoods); i++)
        {
            foodsToBuy.Add(MenuManager.instance.listFoodOnSale[i]);
        }
        return foodsToBuy;
    }

    private int SetNumberOfFoods(int maxNumberOfGoods) {
        int foodsCount = MenuManager.instance.listFoodOnSale.Count;
        if (foodsCount < maxNumberOfGoods) {
            return Random.Range(1, foodsCount + 1);
        } else {
            return Random.Range(1, maxNumberOfGoods + 1);
        }
    }

    private void SpawnCustomer() {
        int index = Random.Range(0, customerQueue.Count - 1);
        customerQueue[index].gameObject.SetActive(true);
    }

    public void DespawnCustomer(CustomerAI customer) {
       customer.gameObject.SetActive(false);
    }
}
