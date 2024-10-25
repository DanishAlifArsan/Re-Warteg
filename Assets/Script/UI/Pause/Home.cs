using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject button;
    GameData data;
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;
        data = SaveManager.instance.LoadGame();
        if (data == null)
        {
            GameManager.instance.LoadScene(1);  // pindah ke scene tutorial
        }
        button.SetActive(true);
    }

    public void Play() {
        if (data.isWarteg)
        {
            GameManager.instance.LoadScene(2);
        } else {
            GameManager.instance.LoadScene(1);
        }
    }

    public void Quit() {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
