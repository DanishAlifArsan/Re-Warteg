using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public int hitRecieved;
    public int numberOfAttack;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealth(int amount) {  // pas ngopi
        if (currentHealth + amount > maxHealth)
        {
            return;
        }
        
        float newHealth = currentHealth + amount;
        currentHealth = newHealth >= maxHealth? maxHealth : newHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void DecreaseHealth(int amount) { // di batu kalau serangannya gak kena
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            //dead
            Debug.Log("dead");
            healthBar.fillAmount = 0;
        }
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
}
