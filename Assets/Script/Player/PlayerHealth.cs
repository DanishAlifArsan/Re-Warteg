using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private int startingCoffee;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private DropItem coffee;
    [SerializeField] private GameCutscene deathCutscene;
    [SerializeField] private Transition transition;
    private float currentHealth;
    public int hitRecieved;
    public int numberOfAttack;
    private DropItem prevItem;
    public int coffeeAmount {get; private set;}
    public static PlayerHealth instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        GameData data = SaveManager.instance.LoadGame();
        if (data != null)
        {
            int amount = Inventory.instance.GetItemCount(coffee);
            // AddCoffee(SaveManager.instance.coffeeAmount);
            AddCoffeeFromSave(amount);
        } else {
            AddCoffee(startingCoffee);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddHealth(int amount) {  // pas ngopi
        if (currentHealth >= maxHealth)
        {
            return false;
        }
        
        float newHealth = currentHealth + amount;
        currentHealth = newHealth >= maxHealth? maxHealth : newHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
        return true;
    }

    public void DecreaseHealth(int amount) { // di batu kalau serangannya gak kena
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            //dead
            transition.gameObject.SetActive(true);
            transition.OnEndTransition += DeathCutscene;
            healthBar.fillAmount = 0;
        }
    }

    private void DeathCutscene() {
        deathCutscene.StartCutscene();
         transition.gameObject.SetActive(false);
    }

    public void ResetCounter() {
        hitRecieved = 0;
        numberOfAttack = 0;
    }

    public void SetHitRecieved() {
        hitRecieved++;
        if (hitRecieved > 5)
        {
            hitRecieved = 0;
            DecreaseHealth(1);
        }
    }

    public void SetNumberOfAttack() {
        numberOfAttack++;
        if (numberOfAttack > 3)
        {
            numberOfAttack = 0;
            DecreaseHealth(1);
        }
    }

    public void SetPrevItem(DropItem item) {
        if (prevItem == item)
        {
            DecreaseHealth(1);
        }
        prevItem = item;
    }

    public void AddCoffee(int amount) {
        // coffeeAmount += amount;
        if (amount > 0)
        {
            Inventory.instance.AddItem(coffee, amount);    
        } else {
            Inventory.instance.RemoveItem(coffee, Mathf.Abs(amount));
        }

        AddCoffeeFromSave(amount);

        // if (GameManager.instance.currentSession == GameSession.Dungeon) amountText.text = coffeeAmount.ToString();
    }

    private void AddCoffeeFromSave(int amount) {
        coffeeAmount += amount;
        if (GameManager.instance.currentSession == GameSession.Dungeon) amountText.text = coffeeAmount.ToString();
    }
}
