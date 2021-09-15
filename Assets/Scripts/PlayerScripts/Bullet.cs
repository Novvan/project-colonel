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
    private float currentLifeTime;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLifeTime < lifeTime) currentLifeTime += Time.deltaTime;
        else Destroy(gameObject);
        _rb.velocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
