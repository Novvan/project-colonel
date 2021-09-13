using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterController : MonoBehaviourPun
{
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        if (!photonView.IsMine) Destroy(this);
    }
    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        if (v != 0 || h != 0) _character.Move(dir);
    }
}
