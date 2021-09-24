using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AIController : MonoBehaviourPun
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject detectionArea;
    private Rigidbody _rb;
    private Character _character;
    private GameManager gm;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _character = GetComponent<Character>();

    }

    private void Start()
    {
        gm = _character.Gm;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (target != null)
            {
                Vector3 targerDir = target.transform.position - transform.position;
                _character.Move(targerDir.normalized);
            }
        }
    }

    public void SetTarget(GameObject nextTarget)
    {
        if (target == null)
        {
            target = nextTarget;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Character character = other.gameObject.GetComponent<Character>();
            character.photonView.RPC("GetDamage", character.photonView.Owner, 20);
        }
    }

}
