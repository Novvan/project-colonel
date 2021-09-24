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
        if (!photonView.IsMine) Destroy(this);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (_currentLifeTime < _lifeTime) _currentLifeTime += Time.deltaTime;
            else Destroy(gameObject);
            _rb.velocity = transform.forward * _speed;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag.ToLower() == "enemy")
            {
                Character character = collision.gameObject.GetComponent<Character>();
                character.photonView.RPC("GetDamage", character.photonView.Owner, _damage);
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}
