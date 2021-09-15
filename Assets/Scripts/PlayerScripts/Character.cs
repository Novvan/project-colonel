using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Character : MonoBehaviourPun
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    private Rigidbody _rb;
    private float currentHealth;
    private float currentAttackTimer;
    private float attackTimer = 0.2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void Move(Vector3 dir) 
    {
        dir.y = 0;
        dir *= speed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }
    public void AimTo(Vector3 mdir) 
    {
        transform.forward = mdir - transform.position;
    }
    public void Attack() 
    {
        if (currentAttackTimer < attackTimer) 
        {
            currentAttackTimer += Time.deltaTime; 
        }
        else
        {
            PhotonNetwork.Instantiate("Bullet", transform.position, transform.rotation);
            currentAttackTimer = 0;
        }
    }
    public void GetDamage(float damage) 
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }
}
