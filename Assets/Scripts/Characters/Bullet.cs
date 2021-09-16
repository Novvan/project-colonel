using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPun
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] float damage;
    private float currentLifeTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (currentLifeTime < lifeTime) currentLifeTime += Time.deltaTime;
        else Destroy(gameObject);
        _rb.velocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToLower() == "enemy") 
        {
            collision.gameObject.GetComponent<Character>().GetDamage(damage);
        }
        Destroy(gameObject);
    }
}
