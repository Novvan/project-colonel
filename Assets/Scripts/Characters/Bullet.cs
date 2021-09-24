using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPun
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    private float _currentLifeTime;
    private GameObject _owner;
    public GameObject Owner { get => _owner; set => _owner = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_currentLifeTime < _lifeTime) _currentLifeTime += Time.deltaTime;
        else Destroy(gameObject);
        _rb.velocity = transform.forward * _speed;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.ToLower() == "enemy")
        {
            collision.gameObject.GetComponent<Character>().GetDamage(_damage, _owner);
            Destroy(gameObject);
        }
    }
}
