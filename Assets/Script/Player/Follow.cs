using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public GameObject following;

    void LateUpdate () {
        Vector3 followPos = new Vector3(following.transform.position.x, transform.position.y, following.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, followPos, 1);
    }
}