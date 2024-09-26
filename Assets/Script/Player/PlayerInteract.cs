using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private Transform interactPoint;
    [SerializeField] private RectTransform indicator;
    [SerializeField] private TextMeshProUGUI indicatorText;
    PlayerInput playerInput;

    private void Start() {
        playerInput = InputManager.instance.playerInput;
        playerInput.Player.Interact.performed += Interact;
    }

    private void LateUpdate() {
        ShowIndicator(CheckItem() != null, CheckItem()?.GetComponent<Interactable>()?.FlavorText());
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        CheckItem()?.GetComponent<Interactable>().OnInteract();
    }

    private void ShowIndicator(bool status, string name) {
        indicatorText.text = name;
        indicator.gameObject.SetActive(status);
    }

    private Collider CheckItem() {
        Collider[] cols = Physics.OverlapSphere(interactPoint.position, range, LayerMask.GetMask("interact"));
        Collider selectedItem = null;
        float minDistance = float.PositiveInfinity;
        foreach (var item in cols)
        {
            float dist = Vector3.Distance(interactPoint.position, item.transform.position);
            if (dist < minDistance)
            {
                selectedItem = item;
                minDistance = dist;
            }
        }
        return selectedItem;
    }

    // private void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(interactPoint.position, range);
    // }
}
