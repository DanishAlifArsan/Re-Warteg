using System;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int startingDay;
    [SerializeField] private int startHour;
    [SerializeField] private int endHour;
    [SerializeField] private float cycleRate;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private NPCConversation dungeonConversation;
    public int currentDay;
    private int hours;
    private float minutes;
    public static TimeManager instance;
    private bool timeStart;

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
            currentDay = SaveManager.instance.day;
        } else {
            currentDay = startingDay;
        }
        Setup();
    }

    private bool endFlag = false;

    // Update is called once per frame
    private void Update()
    {
        if (minutes > 59)
        {
            hours++;
            minutes = 0;
        }

        if (GameManager.instance.currentSession == GameSession.Warteg)
        {
            if (timeStart)
            {
                minutes += Time.deltaTime * cycleRate;
                timeText.text = String.Format("{0:00}:{1:00}", hours, (int) minutes);

                if (EndWarteg())
                {
                    timeStart = false;
                    // GameManager.instance.EndSession();
                }
            } else if (!CustomerManager.instance.ActiveCustomer() && !endFlag)
            {
                endFlag = true;
                GameManager.instance.EndSession();
            }
        }    

         if (GameManager.instance.currentSession == GameSession.Dungeon && timeStart) {
            CountDungeonTime(0);
            timeStart = false;
         }
    }

    private void Setup() {
        dayText.text = "Day Remaining: " + currentDay.ToString();
        hours = startHour;
        minutes = 0;

        timeStart = true;
    }

    public void CountDungeonTime(int timeIncrease) {    // panggil di item di dungeon
        minutes += timeIncrease;
        if (minutes > 59)
        {
            hours++;
            minutes = 0;
        }

        timeText.text = String.Format("{0:00}:{1:00}", hours, (int) minutes);

        if (EndDungeon())
        {
            // Setup();
            // GameManager.instance.EndSession();
            PlayerInput playerInput = InputManager.instance.playerInput;
            playerInput.Player.Disable();
            playerInput.Dungeon.Disable();
            playerInput.UI.Enable();
            ConversationManager.Instance.StartConversation(dungeonConversation);
        }
    }

    public bool EndDungeon() {      // buat akhir sesi dungeon
        return hours >= endHour;
    }

    public bool EndWarteg() {       // buat akhir sesi warteg
        return hours >= endHour;
    }

    public bool EndGame() {
        return currentDay <= 1;
    }
}
