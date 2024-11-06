using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    [SerializeField] private AudioClip buttonSound;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void PlaySound(AudioClip sfx)
    {
        audioSource.PlayOneShot(sfx);
    }

    public void PlayButton() {
        audioSource.PlayOneShot(buttonSound);
    }
    public void StopButton() {
        audioSource.Stop();
    }
}
