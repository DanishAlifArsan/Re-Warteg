using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private int maxCustomer;
    [SerializeField] private float maxProfit;
    [SerializeField] private TextMeshProUGUI customerAmountText;
    [SerializeField] private TextMeshProUGUI profitText;
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameCutscene pajakCutscene;
    [SerializeField] private GameCutscene gagalCutscene;
    [SerializeField] private Gameover gameoverScene;
    [SerializeField] private AudioClip paperSound;

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
        AudioManager.instance.PlaySound(paperSound);

        int customerAmount = CustomerManager.instance.customerAmount;
        int profit = CustomerManager.instance.profit;
        string score;
        int bonus;

        customerAmountText.text = customerAmount.ToString();
        profitText.text = profit.ToString();
        if (profit > maxProfit)
        {
            score = "S";
            bonus = 5000;
        } else if (customerAmount > maxCustomer)   // kalau ada waktu benerin lagi logicnya
        {
            score = "A";
            bonus = 2000;
        } else if (customerAmount > 0 && profit > 0) 
        {
            score = "B";
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
        if (!TimeManager.instance.endFlag)  // kalau belum selesai, dialog cuma nutup
        {
            InputManager.instance.playerInput.Player.Enable();
            InputManager.instance.playerInput.Kitchen.Enable();
            return;
        }

        if (isContinue)
        {
            if (isEnded)
            {
                Time.timeScale = 1;
                pajakCutscene.StartCutscene();
                isEnded = false;
                return;
            }
            GameManager.instance.LoadScene(1);
        } else {
            if (isEnded)  // kalau ada gagal
            {
                Time.timeScale = 1;
                gagalCutscene.StartCutscene();
                isEnded = false;
                return; 
            }
            // Gameover();
            gameoverScene.gameObject.SetActive(true);
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

    // private void Gameover() {
    //     SaveManager.instance.NewGame(); // kemungkinan reset ke tutorial
    //     GameManager.instance.LoadScene(0);
    // }
}
