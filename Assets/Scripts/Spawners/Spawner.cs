using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string prefabName;
    public virtual void Spawn()
    {
        PhotonNetwork.Instantiate(prefabName, gameObject.transform.position, Quaternion.identity);  
    }

}
