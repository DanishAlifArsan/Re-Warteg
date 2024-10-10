using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float attack;
    [SerializeField] protected float cooldown;
    [SerializeField] protected RectTransform weaponIcon;
    [SerializeField] protected Image cooldownImage;
    [SerializeField] protected ParticleSystem attackEffect;
    protected float cooldownTimer;
    protected bool canAttack;
    protected virtual void Start() {
        cooldownTimer = 0;
        canAttack = true;
    }
    protected virtual void Update() {
        if (canAttack)
        {
            return;
        }
        cooldownImage.fillAmount = cooldownTimer / cooldown;
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) canAttack = true;
    }
    public abstract void Attack();

    public void MoveIcon(int index) {
        weaponIcon.SetSiblingIndex(index);
    }

    public void Select() {
        weaponIcon.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void Deselect() {
        weaponIcon.localScale = Vector3.one;
    }
}
