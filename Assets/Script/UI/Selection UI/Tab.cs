using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : UISelection
{
    [SerializeField] private GameObject menu;

    public void ShowMenu(bool status) {
        menu.SetActive(status);
    }
}
