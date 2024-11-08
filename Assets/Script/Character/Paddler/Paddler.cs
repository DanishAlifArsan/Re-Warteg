using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paddler : MonoBehaviour, Interactable
{
    [SerializeField] private Transform player;
    [SerializeField] private Shop shop;
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float range;
    [SerializeField] private GameObject bubbleTextObject;
    [SerializeField] private TextMeshProUGUI bubbleText;
    [SerializeField] private List<string> dialogue;
     private bool dialogueGenerated = true;
    public string FlavorText()
    {
        return "Buy";
    }

    public void OnInteract()
    {
        shop.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3( player.position.x,  transform.position.y, player.position.z );
        transform.LookAt(targetPos);
    }

    // private void LateUpdate() {     // kalau paddler bisa ngomong
    //     if (CheckPlayer())
    //     {
    //         bubbleTextObject.SetActive(true);
    //         if (dialogueGenerated)
    //         {
    //             bubbleText.text = dialogue[Random.Range(0, dialogue.Count)];
    //             dialogueGenerated = false;
    //         }
    //     } else {
    //         bubbleTextObject.SetActive(false);
    //     }
    // }

    private bool CheckPlayer() {
        Collider[] cols = Physics.OverlapSphere(interactPoint.position, range, LayerMask.GetMask("player"));
        return cols.Length > 0;
    }
}
