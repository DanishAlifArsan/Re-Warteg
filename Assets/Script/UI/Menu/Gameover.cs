using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource gameoverMusic;
    private void OnEnable() {
        gameMusic.Stop();
        gameoverMusic.Play();
    }
    public void GameOver() {
        SaveManager.instance.NewGame(); // kemungkinan reset ke tutorial
        GameManager.instance.LoadScene(0);
    }
}
