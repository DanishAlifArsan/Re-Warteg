using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    private int currentTutorialId = 0;
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
        Debug.Log("Tutorial Masak");
    }

    public void NextTutorial(int nextId) {
        if (nextId <= currentTutorialId)  {
            return;
        }   

        currentTutorialId = nextId;
        switch (currentTutorialId)
        {
            case 0: Debug.Log("Tutorial Masak"); break;
            case 1: Debug.Log("Tutorial Antrian"); break;
            case 2: Debug.Log("Tutorial Siapkan masakan"); break;
            case 3: Debug.Log("Tutorial daftar menu"); break;
            case 4: Debug.Log("Tutorial melayani kustomer"); break;
            case 5: Debug.Log("Tutorial memilih masakan"); break;
            case 6: Debug.Log("Tutorial kustomer makan"); break;
            case 7: Debug.Log("Tutorial bersihkan piring"); break;
            case 8: Debug.Log("Tutorial beli item"); break;
        }
        
    }
}
