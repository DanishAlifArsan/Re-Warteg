using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneObject : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float lifeTime;
    [SerializeField] private float power;
    [SerializeField] private float range;
    [SerializeField] private GameObject stone;
    [SerializeField] private ParticleSystem stoneEffect;
    private float lifeTimeDuration = 0;
    private Transform startPoint;
    private float attack;
    private bool isActive = false;
    private AudioClip attackSound;

    public void Setup(Transform _start, float _attack, AudioClip _attackSound)
    {
        startPoint = _start;
        attack = _attack;
        attackSound = _attackSound;
        rb = GetComponent<Rigidbody>();   
        rb.isKinematic = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isActive)
        {
            lifeTimeDuration += Time.deltaTime;
            if (lifeTimeDuration >= lifeTime)
            {
                Destroy();
            }
        }
    }

    public void Throw() {
        rb.isKinematic = false;
        transform.position = startPoint.position;
        transform.parent = null;
        stone.SetActive(true);
        rb.AddForce(transform.forward * power, ForceMode.Impulse);
        isActive = true;
    }

    private void Destroy() {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        if (EnemyInSight().Length > 0)
        {
            foreach (var item in EnemyInSight())
            {
                item.GetComponent<MonsterAI>().Damage(attack);
            }
        } else {
            PlayerHealth.instance.DecreaseHealth(1);
        }

        stone.SetActive(false);
        lifeTimeDuration = 0;
        isActive = false;

        StartCoroutine(PlayParticle());
    }
    

    private void OnCollisionEnter(Collision other) {
        Destroy();
    }

    private Collider[] EnemyInSight() {
        return Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("enemy"));
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, range);
    }

    public IEnumerator PlayParticle() {
        stoneEffect.Play();
        AudioManager.instance.PlaySound(attackSound);
        yield return new WaitWhile(() => stoneEffect.isPlaying);
        stoneEffect.Stop();
        transform.position = startPoint.position;
        transform.parent = startPoint;
        transform.localRotation = Quaternion.identity;
    }
}
