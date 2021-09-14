using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WinTrigger : MonoBehaviourPun
{
    bool _hasWinner;
    public GameObject winnerScreen;
    public GameObject loserScreen;
    void OnTriggerEnter(Collider coll)
    {
        if (PhotonNetwork.IsMasterClient && !_hasWinner)
        {
            var character = coll.GetComponent<Character>();
            if (character != null)
            {
                _hasWinner = true;
                var photonViewCharacter = character.GetComponent<PhotonView>();
                var playerClient = photonViewCharacter.Owner;
                photonView.RPC("Win", RpcTarget.All, playerClient);
            }
        }
    }
    [PunRPC]
    public void Win(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            //Win
            winnerScreen.SetActive(true);
            loserScreen.SetActive(false);
        }
        else
        {
            //Lose
            loserScreen.SetActive(true);
            winnerScreen.SetActive(false);
        }
    }

}
