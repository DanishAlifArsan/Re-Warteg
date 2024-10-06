using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int startingDay;
    [SerializeField] private int startHour;
    [SerializeField] private int endHour;
    [SerializeField] private float cycleRate;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI dayText;
    public int currentDay;
    private int hours;
    private float minutes;
    public static TimeManager instance;
    public bool isWarteg;

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
        Setup();
    }

    // Update is called once per frame
    private void Update()
    {
        if (minutes > 59)
        {
            hours++;
            minutes = 0;
        }

        if (isWarteg)
        {
            minutes += Time.deltaTime * cycleRate;
            timeText.text = String.Format("{0:00}:{1:00}", hours, (int) minutes);

            if (EndWarteg())
            {
                Setup();
            }
        }    
    }

    private void Setup() {
        currentDay = startingDay;
        dayText.text = "Day Remaining: " + currentDay.ToString();
        hours = startHour;
        minutes = 0;

        // currentDay--; // hapus aja nanti ini
    }

    public void CountDungeonTime(int timeIncrease) {    // panggil di item di dungeon
        minutes += timeIncrease;

        if (EndDungeon())
        {
            Setup();
        }
    }

    public bool EndDungeon() {      // buat akhir sesi dungeon
        return hours >= endHour;
    }

    public bool EndWarteg() {       // buat akhir sesi warteg
        return hours >= endHour;
    }

    public bool EndGame() {
        return currentDay <= 0;
    }
}