using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string prefabName;
    void Start()
    {
        PhotonNetwork.Instantiate(prefabName, gameObject.transform.position, Quaternion.identity);  
    }

}
