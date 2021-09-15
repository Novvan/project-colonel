using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private string prefabName;
    public void Spawn() 
    {
        PhotonNetwork.Instantiate(prefabName, gameObject.transform.position, Quaternion.identity);
    }
}
