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
    [SerializeField] private int setoran;
    [SerializeField] private GameObject loadingScene;
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

        if (currentSession == GameSession.Home)
        {
            return;
        }

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
    public void LoadScene(int sceneId) {
        // PlaySound();
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    private IEnumerator LoadSceneAsync(int sceneId) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScene.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    private int currentDay;

    public void EndSession() {      // pindah ke result
        if (TimeManager.instance.EndGame() && currentSession == GameSession.Warteg)
        {
            //tambahkan perhitungan pajak
            //gameover kalau uang kurang dari pajak
            if (CurrencyManager.instance.CountRemainMoney(setoran)) // kalau masih bisa bayar pajak, lanjut hari
            {
                currentDay = TimeManager.instance.startingDay;
                SaveGame();
                LoadScene(1);
            } else {
                SaveManager.instance.NewGame();
                LoadScene(0);
            }
        } else {
            //lanjut hari
            // SaveManager.instance.totalCurrency = CurrencyManager.instance.totalCurrency;
            switch (currentSession)
            {
                case GameSession.Dungeon:
                    currentDay = TimeManager.instance.currentDay;
                    SaveGame();
                    LoadScene(2);
                    break;
                case GameSession.Warteg:
                    currentDay = TimeManager.instance.currentDay-1;
                    SaveGame();
                    LoadScene(1);
                    break;
            }
        }
    }

    private void SaveGame() {
        SaveManager.instance.day = currentDay;
        SaveManager.instance.inventoryItem = Inventory.instance.inventoryItem;
        SaveManager.instance.totalCurrency = CurrencyManager.instance.totalCurrency;
        SaveManager.instance.isWarteg = currentSession == GameSession.Dungeon;
        SaveManager.instance.SaveGame();
    }
}

public enum GameSession
{
    Home,
    Dungeon,
    Warteg,
}
