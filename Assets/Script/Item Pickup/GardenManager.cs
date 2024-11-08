using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    [SerializeField] private List<Transform> gardenPos;
    [SerializeField] private List<Garden> gardenPrefab;
    [SerializeField] private ItemPickup itemPickup;
    [SerializeField] private int timeRate;
    public static GardenManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }
    // Start is called before the first frame update
    private void Start()    // ubah supaya dipanggil tiap kali masuk sesi dungeon
    {
        List<Transform> randomPos = SetRandomPos(gardenPos, gardenPrefab.Count);
        for (int i = 0; i < Mathf.Min(gardenPrefab.Count, randomPos.Count); i++)
        {
            Instantiate(gardenPrefab[i], randomPos[i].position, Quaternion.identity, randomPos[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Transform> SetRandomPos(List<Transform> list, int k) {
        List<Transform> collection = list;
        int n = collection.Count;
        for (int i = 0; i < k; i++)
        {
            int j = Random.Range(i, n - 1);
            Transform temp = collection[i];
            collection[i] = collection[j];
            collection[j] = temp;
        }
        return collection;
    }

    public void Pickup(DropItem item, int count) {
        TimeManager.instance.CountDungeonTime(timeRate);
        itemPickup.Pickup(item, count);
    }
}
