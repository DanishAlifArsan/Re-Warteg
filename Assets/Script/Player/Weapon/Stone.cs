using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Weapon
{
    [SerializeField] private StoneObject[] stoneObject;
    [SerializeField] private SpriteRenderer skillArea;
    private int currentIndex;

    public override void Setup(PlayerAttack _playerAttack)
    {
        base.Setup( _playerAttack);
        foreach (var item in stoneObject)
        {
            item.Setup(transform, attack);
        }
        currentIndex = 0;
    }

    protected override void Update() {
        base.Update();
        Vector3 targetPos = new Vector3( cursorOnTransform.x,  stoneObject[currentIndex].transform.position.y, cursorOnTransform.z );
        stoneObject[currentIndex].transform.LookAt(targetPos);

        Vector3 relativePos = cursorOnTransform - skillArea.transform.position;
        Vector3 rotation = Quaternion.LookRotation(relativePos).eulerAngles;
        skillArea.transform.localEulerAngles = new Vector3(-90, 0, rotation.y);
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
        skillArea.enabled = true;
    }

    public override void Deselect()
    {
        base.Deselect();
        skillArea.enabled = false;
    }

    private Vector3 cursorWorldPosOnNCP {
        get {
            Vector2 mousePosition = InputManager.instance.activeGameDevice == GameDevice.KeyboardMouse? 
            playerAttack.playerInput.Player.MousePosition.ReadValue<Vector2>() : playerAttack.playerInput.Player.VirtualMouse.ReadValue<Vector2>();
            return playerAttack.cam.ScreenToWorldPoint(
                new Vector3(mousePosition.x, 
                mousePosition.y, 
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
