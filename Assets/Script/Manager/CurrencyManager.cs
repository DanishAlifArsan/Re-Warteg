using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI increaseText;
    [SerializeField] private TextMeshProUGUI decreaseText;
    [SerializeField] private int startingCurrency;
    // private Animator increaseAnim, decreaseAnim;
    public int totalCurrency = 0;

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
        GameData data = SaveManager.instance.LoadGame();
        if (data != null)
        {
            startingCurrency = SaveManager.instance.totalCurrency;
        }
        // increaseAnim = increaseText.GetComponent<Animator>();
        // decreaseAnim = decreaseText.GetComponent<Animator>();
        AddCurrency(startingCurrency);
    }

    public void AddCurrency(int value) {
        totalCurrency += value;
        // increaseText.text = "+"+value;
        // increaseAnim.SetTrigger("increase");
        Updatecurrency();
    }

    public void RemoveCurrency(int value){
        // decreaseText.text = "-"+value;
        // decreaseAnim.SetTrigger("decrease");
        totalCurrency -= value;
        Updatecurrency();
    }

    private void Updatecurrency() {
        // currencyText.text = totalCurrency.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        currencyText.text = totalCurrency.ToString();
    }

    public bool CanBuy(int cost) {
        return totalCurrency - cost >= 0;
    }

    public bool CountRemainMoney(int value) {
        totalCurrency -= value;
        return totalCurrency >= 0;
    }
}
