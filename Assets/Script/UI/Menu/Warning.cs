using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private AudioClip warningSound;
    private Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    public void ShowWarning(string text) {
        warningText.text = text;
        AudioManager.instance.PlaySound(warningSound);
        anim.SetTrigger("appear");
    }
}
