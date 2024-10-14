using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameSession currentSession;
    [SerializeField] private NavMeshAgent player;
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
        PlayerInput playerInput = InputManager.instance.playerInput;

        player.enabled = false;

        switch (currentSession)
        {
            case GameSession.Dungeon:
                playerInput.Dungeon.Enable();
                playerInput.Kitchen.Disable();
                break;
            case GameSession.Warteg:
                playerInput.Dungeon.Disable();
                playerInput.Kitchen.Enable();
                break;
        }

        player.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int currentDay;

    public void EndSession() {      // pindah ke result
        if (TimeManager.instance.EndGame())
        {
            //tambahkan perhitungan pajak
            //gameover kalau uang kurang dari pajak
            SaveManager.instance.NewGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ganti kalau game over load ke home. kalau gak game over ke session dungeon
        } else {
            //lanjut hari
            SaveManager.instance.totalCurrency = CurrencyManager.instance.totalCurrency;
            switch (currentSession)
            {
                case GameSession.Dungeon:
                    currentDay = TimeManager.instance.currentDay;
                    SaveGame();
                    SceneManager.LoadScene(1);
                    break;
                case GameSession.Warteg:
                    currentDay = TimeManager.instance.currentDay-1;
                    SaveGame();
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }

    private void SaveGame() {
        SaveManager.instance.day = currentDay;
        SaveManager.instance.inventoryItem = Inventory.instance.inventoryItem;
        // SaveManager.instance.coffeeAmount = PlayerHealth.instance.coffeeAmount; // ini gak perlu kalau misal kopi juga masuk ke inventory
        SaveManager.instance.SaveGame();
    }
}

public enum GameSession
{
    Home,
    Dungeon,
    Warteg,
}
