using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string _prefabName;
    [SerializeField] protected GameManager gm;
    public virtual void Spawn()
    {
        GameObject temp = PhotonNetwork.Instantiate(_prefabName, gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<Character>().Gm = gm;
    }

}
