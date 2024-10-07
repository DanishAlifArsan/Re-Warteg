using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameSession currentSession;
    [SerializeField] private NavMeshAgent player;
    [SerializeField] private GameObject wartegObject;
    [SerializeField] private Transform wartegSpawn;
    [SerializeField] private GameObject dungeonObject;
    [SerializeField] private Transform dungeonSpawn;
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
                player.transform.position = dungeonSpawn.position;
                wartegObject.SetActive(false);
                dungeonObject.SetActive(true);
                playerInput.Dungeon.Enable();
                playerInput.Kitchen.Disable();
                break;
            case GameSession.Warteg:
                player.transform.position = wartegSpawn.position;
                wartegObject.SetActive(true);
                dungeonObject.SetActive(false);
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
}

public enum GameSession
{
    Home,
    Dungeon,
    Warteg,
}
