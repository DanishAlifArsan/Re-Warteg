using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    // [SerializeField] private int numberOfPlates;
    [SerializeField] List<Plate> listOfPlates;
    [SerializeField] Transform platePos;
    [SerializeField] List<Transform> randPlatePos;
    private List<Plate> cleanPlate = new List<Plate>();
    public List<Plate> dirtyPlate = new List<Plate>();
    public static PlateManager instance;
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
        GeneratePlate();
    }

    public void GeneratePlate() {
        foreach (Plate item in listOfPlates)
        {
            item.gameObject.SetActive(false);
            cleanPlate.Add(item);
        }
    }

    public void Wash(Plate plate) {
        plate.gameObject.SetActive(false);
        cleanPlate.Add(plate);
        plate.transform.parent = platePos;
        plate.transform.position = Vector3.zero;
    }

    public void TakePlate(Plate plate) {
        if (dirtyPlate.Contains(plate))
        {
            dirtyPlate.Remove(plate); 
        }
    }

    public void TakeFood() {
        if (cleanPlate.Count > 0)
        {
            Plate activePlate = cleanPlate[0];
            activePlate.gameObject.SetActive(true);
            cleanPlate.RemoveAt(0);
            dirtyPlate.Add(activePlate);

            activePlate.transform.position = randPlatePos[Random.Range(0, randPlatePos.Count - 1)].position;    // ganti dengan menangani script customer
        } else {
            Debug.Log("No more plate T-T. Please cleaning first");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
