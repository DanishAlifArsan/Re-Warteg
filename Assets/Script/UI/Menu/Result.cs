using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private int maxCustomer;
    [SerializeField] private TextMeshProUGUI customerAmountText;
    [SerializeField] private TextMeshProUGUI profitText;
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameCutscene pajakCutscene;
    [SerializeField] private GameCutscene gagalCutscene;

    private PlayerInput playerInput;
    public bool isContinue;
    public bool isEnded;
    private void OnEnable() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Disable();
        playerInput.Kitchen.Disable();
        playerInput.Dungeon.Disable();
        playerInput.UI.Enable();
        Time.timeScale = 0;

        //tambahkan setup teks
        Setup();
    }

    private void Setup() {
        int customerAmount = CustomerManager.instance.customerAmount;
        int profit = CustomerManager.instance.profit;
        string score;
        int bonus;

        customerAmountText.text = customerAmount.ToString();
        profitText.text = profit.ToString();
        if (customerAmount > maxCustomer)   // kalau ada waktu benerin lagi logicnya
        {
            score = "S";
            bonus = 5000;
        } else if (customerAmount > 0) 
        {
            score = "A";
            bonus = 1000;
        } else {
            score = "C";
            bonus = 0;
        }
        scoreText.text = score;
        bonusText.text = bonus.ToString(); 
    }

    private void OnDisable() {
        playerInput.Player.Enable();
        playerInput.Kitchen.Enable();
        playerInput.Dungeon.Enable();
        playerInput.UI.Disable();
        Time.timeScale = 1;
    }

    public void Continue() {
        if (isContinue)
        {
            if (isEnded)
            {
                pajakCutscene.StartCutscene();
                isEnded = false;
                return;
            }
            GameManager.instance.LoadScene(1);
        } else {
            // if (isEnded)  // kalau ada gagal
            // {
            //     gagalCutscene.StartCutscene();
            //     isEnded = false;
            //     return; 
            // }
            Gameover();
        }

        // if (isEnded)
        // {
        //     if (isContinue)
        //     {
        //         pajakCutscene.StartCutscene();
        //     } 
        //     // else {
        //     //     gagalCutscene.StartCutscene();
        //     // } // kalau gagal bayar pajak
        //     isEnded = false;
        //     return;
        // }
        // if (isContinue)
        // {
        //    GameManager.instance.LoadScene(1);
        // } else {
        //     SaveManager.instance.NewGame(); // kemungkinan reset ke tutorial
        //     GameManager.instance.LoadScene(0);
        // }
        // gameObject.SetActive(false);
    }

    private void Gameover() {
        SaveManager.instance.NewGame(); // kemungkinan reset ke tutorial
        GameManager.instance.LoadScene(0);
    }
}
