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

    private GameManager _gm;
    public GameManager Gm { get => _gm; set => _gm = value; }
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
            _currentAttackTimer = 0;
        }
    }
    public void GetDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            if (gameObject.GetComponent<AIController>() != null)
            {
                if (_gm != null)
                {
                    Debug.Log("GM no es null pa");
                }
                else
                {
                    Debug.Log("GM en null pa");
                    _gm = FindObjectOfType<GameManager>();
                }
                _gm.ZombiesAlive--;
                _gm.ZombieCount++;
            }
            _currentHealth = 0;
            KillCharacter();
        }
    }



    public void KillCharacter()
    {
        Destroy(gameObject);
    }
}
