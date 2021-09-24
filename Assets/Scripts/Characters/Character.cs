using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Character : MonoBehaviourPun, IMove, IAttack, IDamageable
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bulletPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHealth;
    private Rigidbody _rb;
    private float _currentHealth;
    private float _currentAttackTimer;
    private float _attackTimer = 0.2f;
    private float _killcount;

    private GameManager gm;
    public GameManager Gm { get => gm; set => gm = value; }
    public float Killcount { get => _killcount; set => _killcount = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void Move(Vector3 dir)
    {
        dir.y = 0;
        dir *= _speed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }
    public void AimTo(Vector3 mdir)
    {
        transform.forward = mdir - transform.position;
    }
    public void Attack()
    {
        if (_currentAttackTimer < _attackTimer)
        {
            _currentAttackTimer += Time.deltaTime;
        }
        else
        {
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", _bulletPoint.transform.position, _bulletPoint.transform.rotation);
            bullet.gameObject.GetComponent<Bullet>().Owner = this.gameObject;
            _currentAttackTimer = 0;
        }
    }
    public void GetDamage(float damage, GameObject damageInstigator)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            if (gameObject.GetComponent<AIController>() != null)
            {
                gm.ZombiesAlive--;
                gm.ZombieCount++;
                damageInstigator.GetComponent<Character>().Killcount++;
            }
            _currentHealth = 0;
            photonView.RPC("killCharacter", RpcTarget.All);
        }
    }



    [PunRPC]
    private void killCharacter()
    {
        Destroy(this.gameObject);
    }
}
