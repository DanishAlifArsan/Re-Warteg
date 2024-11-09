using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
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
    [SerializeField] private Result resultScene;
    [SerializeField] private NPCConversation startConversation;
    
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
                ConversationManager.Instance.StartConversation(startConversation);
                playerInput.Dungeon.Disable();
                playerInput.Kitchen.Disable();
                
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
        // SceneManager.LoadScene(sceneId);
    }

    private IEnumerator LoadSceneAsync(int sceneId) {
        // DontDestroyOnLoad(loadingScene);
        // DontDestroyOnLoad(this.gameObject); // supaya tetep load selama 1 detik

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScene.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }

        // yield return new WaitForSeconds(1);     // supaya tetep load selama 1 detik

        // loadingScene.SetActive(false);

        // Destroy(loadingScene);
        // Destroy(this.gameObject);
    }

    private int currentDay;

    public void EndSession() {      // pindah ke result
        // resultScene?.gameObject.SetActive(true);
        TimeManager.instance.endFlag = true;
        if (TimeManager.instance.EndGame() && currentSession == GameSession.Warteg)
        {
            //tambahkan perhitungan pajak
            //gameover kalau uang kurang dari pajak
            if (CurrencyManager.instance.CountRemainMoney(setoran)) // kalau masih bisa bayar pajak, lanjut hari
            {
                currentDay = TimeManager.instance.startingDay;
                // resultScene.OnContinue += NextDay;
                ShowResult(true, true);
                // NextDay();
                SaveGame();
            } else {
                // resultScene.OnContinue += GameOver;
                ShowResult(false, true);
                // GameOver();
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
                    // resultScene.OnContinue += NextDay;
                    ShowResult(true, false);
                    // NextDay();
                    SaveGame();
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

    private void NextDay() {
        LoadScene(1);
    }

    private void GameOver() {
        SaveManager.instance.NewGame();
        LoadScene(0);
    }

    private void ShowResult(bool status, bool ended) {
        resultScene.gameObject.SetActive(true);
        resultScene.isContinue = status;
        resultScene.isEnded = ended;
    }
}

public enum GameSession
{
    Home,
    Dungeon,
    Warteg,
}
