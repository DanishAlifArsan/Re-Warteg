using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    // [SerializeField] private List<Customer> customerList;
    [SerializeField] private List<CustomerAI> customerList;
    [SerializeField] private List<Table> tableList;
    [SerializeField] private float spawnInterval;
    private float spawnTimer = 0;
    public Transform homePoint;
    public Transform cashierPoint;
    public static CustomerManager instance;
    // public bool isSpawned = false;
    public CustomerAI currentCustomer;
    private List<CustomerAI> customerQueue = new List<CustomerAI>();
    [SerializeField] private ItemPickup itemPickup;
    [SerializeField] private DropItem money;

    public int customerAmount = 0;
    public int profit = 0;

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
            // Customer customer = customerList[i];
            // CustomerAI instantiatedCustomer =  Instantiate(customer.prefab, homePoint.position, Quaternion.identity);
            // SetupCustomer(instantiatedCustomer, customer, homePoint);
            SetupCustomer(customerList[i]);
        }
    }

    private void Update() {
        // spawnTimer -= Time.deltaTime;   // pakai ini kalau mau pelanggan langsung spawn 
        if (MenuManager.instance.BuyCondition())
        {
            spawnTimer -= Time.deltaTime; // pakai ini kalau mau pelanggan nunggu dulu sebelum spawn
            if (spawnTimer <= 0 && tableList.Any(s => !s.isOccupied) && customerQueue.Count < customerList.Count && currentCustomer == null && !TimeManager.instance.EndWarteg())
            {   
                SpawnCustomer();
                spawnTimer = spawnInterval;
            }
        }
    }

    // private void SetupCustomer(CustomerAI instantiatedCustomer, Customer origin, Transform homePoint) {
    //     instantiatedCustomer.cashierPoint = cashierPoint;
    //     instantiatedCustomer.homePoint = homePoint;
    //     instantiatedCustomer.agent.speed = origin.walkSpeed;
    //     instantiatedCustomer.agent.acceleration = origin.acceleration;
    //     instantiatedCustomer.maxNumberOfGoods = origin.maxNumberOfFoods;
    //     instantiatedCustomer.eatDuration = origin.eatDuration;
    //     instantiatedCustomer.gameObject.SetActive(false);
    //     customerQueue.Add(instantiatedCustomer);
    // }

    private void SetupCustomer(CustomerAI instantiatedCustomer) {
        instantiatedCustomer.cashierPoint = cashierPoint;
        instantiatedCustomer.homePoint = homePoint;
        instantiatedCustomer.gameObject.SetActive(false);
        // customerQueue.Add(instantiatedCustomer);
    }

    // public List<Food> SetFoodsToBuy(int maxNumberOfGoods) { // ubah logic ini jadi seperti pt arudam kalau mau random
    //     List<Food> foodsToBuy = new List<Food>();           // ubah jadi sistem baru kalau mau fix harus beli nasi
    //     for (int i = 0; i < SetNumberOfFoods(maxNumberOfGoods); i++)
    //     {
    //         foodsToBuy.Add(MenuManager.instance.listFoodOnSale[i]);
    //     }
    //     return foodsToBuy;
    // }

    // private int SetNumberOfFoods(int maxNumberOfGoods) {
    //     int foodsCount = MenuManager.instance.listFoodOnSale.Count;
    //     if (foodsCount < maxNumberOfGoods) {
    //         return Random.Range(1, foodsCount + 1);
    //     } else {
    //         return Random.Range(1, maxNumberOfGoods + 1);
    //     }
    // }

    public List<Food> SetFoodsToBuy(int maxNumberOfGoods) {
        List<Food> foodsToBuy = new List<Food>();
        int numberOfGoods = SetNumberOfFoods(maxNumberOfGoods);
        List<Food> randomGoods = SetRandomFood(MenuManager.instance.GetLauk(), numberOfGoods);
        foodsToBuy.Add(MenuManager.instance.GetRice());
        for (int i = 0; i < numberOfGoods; i++)
        {
            foodsToBuy.Add(randomGoods[i]);
        }
        return foodsToBuy;
    }
    private int SetNumberOfFoods(int maxNumberOfGoods) {
        int goodsCount = MenuManager.instance.GetLauk().Count;
        if (goodsCount < maxNumberOfGoods) {
            return Random.Range(1, goodsCount + 1);
        } else {  
           return Random.Range(1, maxNumberOfGoods + 1);
        }
    }

    private List<Food> SetRandomFood(List<Food> list, int k) {
        List<Food> collection = list;
        int n = collection.Count;
        for (int i = 0; i < k; i++)
        {
            int j = Random.Range(i, n - 1);
            Food temp = collection[i];
            collection[i] = collection[j];
            collection[j] = temp;
        }
        return collection;
    }

    public Table SetTable() {
        return tableList.First(s => !s.isOccupied);
    }

    private void SpawnCustomer() {
        // int index = Random.Range(0, customerQueue.Count);
        // customerQueue[index].gameObject.SetActive(true);
        // customerQueue[index].table = tableList.First(s => s.isOccupied = false);
        int index = Random.Range(0, customerList.Count);
        customerList[index].gameObject.SetActive(true);
        // customerList[index].table = tableList.First(s => !s.isOccupied); // pindah ke pas kustomer dapat makanan
        customerQueue.Add(customerList[index]);
    }

    public void DespawnCustomer(CustomerAI customer) {
       customer.gameObject.SetActive(false);
       customerQueue.Remove(customer);

       Debug.Log(customerQueue.Count);
    }

    public bool ActiveCustomer() {
        return customerQueue.Count > 0;
    }

    public void Pickup(int count) {
        itemPickup.Pickup(money, count);
    }
}
