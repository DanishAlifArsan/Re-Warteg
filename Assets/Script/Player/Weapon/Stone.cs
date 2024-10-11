using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Weapon
{
    [SerializeField] private StoneObject[] stoneObject;
    [SerializeField] private GameObject skillArea;
    private int currentIndex;

    public override void Setup(PlayerHealth _health, PlayerAttack _playerAttack)
    {
        base.Setup(_health, _playerAttack);
        foreach (var item in stoneObject)
        {
            item.Setup(transform, health, attack);
        }
        currentIndex = 0;
    }

    protected override void Update() {
        base.Update();
        // Vector2 mousePosition = playerAttack.playerInput.Player.MousePosition.ReadValue<Vector2>();
        // Ray ray = playerAttack.cam.ScreenPointToRay(mousePosition);
        // RaycastHit hit;
        // Physics.Raycast(ray, out hit);

        // var mousePosition = playerAttack.playerInput.Player.MousePosition.ReadValue<Vector2>();
        // // hit.z = 10.0;
        // Vector3 hit = playerAttack.cam.ScreenToWorldPoint(mousePosition);
        Vector3 targetPos = new Vector3( cursorOnTransform.x,  stoneObject[currentIndex].transform.position.y, cursorOnTransform.z );
        stoneObject[currentIndex].transform.LookAt(targetPos);

        Vector3 relativePos = cursorOnTransform - skillArea.transform.position;
        Vector3 rotation = Quaternion.LookRotation(relativePos).eulerAngles;
        skillArea.transform.localEulerAngles = new Vector3(-90, 0, rotation.y);
        
        // Vector3 areaTargetPos = new Vector3(-90,  skillArea.transform.position.y, cursorOnTransform.z);
        // skillArea.transform.LookAt(areaTargetPos);
    }

    public override void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            cooldownTimer = cooldown;
            attackEffect.Play();
            // todo jalankan animasi serangan

            stoneObject[currentIndex].Throw();
            currentIndex++;
            if (currentIndex > stoneObject.Length - 1)
            {
                currentIndex = 0;
            }
        }
    }

    public override void Select()
    {
        base.Select();
        skillArea.SetActive(true);
    }

    public override void Deselect()
    {
        base.Deselect();
        skillArea.SetActive(false);
    }

    private Vector3 cursorWorldPosOnNCP {
        get {
            return playerAttack.cam.ScreenToWorldPoint(
                new Vector3(playerAttack.playerInput.Player.MousePosition.ReadValue<Vector2>().x, 
                playerAttack.playerInput.Player.MousePosition.ReadValue<Vector2>().y, 
                playerAttack.cam.nearClipPlane));
        }
    }

    private Vector3 cameraToCursor {
        get {
            return cursorWorldPosOnNCP - playerAttack.cam.transform.position;
        }
    }

    private Vector3 cursorOnTransform {
        get {
            Vector3 camToTrans = transform.position - playerAttack.cam.transform.position;
            return playerAttack.cam.transform.position + 
                cameraToCursor * 
                (Vector3.Dot(playerAttack.cam.transform.forward, camToTrans) / Vector3.Dot(playerAttack.cam.transform.forward, cameraToCursor));
        }
    }
}
