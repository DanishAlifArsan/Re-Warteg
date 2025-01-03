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
    [SerializeField] protected Animator anim;
    [SerializeField] protected ParticleSystem attackEffect;
    [SerializeField] protected AudioClip attackSound;
    protected float cooldownTimer;
    protected bool canAttack;
    protected PlayerAttack playerAttack;
    public virtual void Setup(PlayerAttack _playerAttack) {
        cooldownTimer = 0;
        canAttack = true;
        playerAttack = _playerAttack;
    }
    protected virtual void Update() {
        if (canAttack)
        {
            return;
        }
        cooldownImage.fillAmount = cooldownTimer / cooldown;
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) {
            canAttack = true;
            cooldownImage.fillAmount = 0;
        }
    }
    public abstract void Attack();

    public void MoveIcon(int index) {
        weaponIcon.SetSiblingIndex(index);
    }

    public virtual void Select() {
        weaponIcon.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public virtual void Deselect() {
        weaponIcon.localScale = Vector3.one;
    }
}
