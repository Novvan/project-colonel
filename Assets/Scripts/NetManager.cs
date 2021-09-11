using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Button button;
    [SerializeField] InputField inputFieldRoom;
    [SerializeField] InputField inputFieldPlayer;

    void Start()
    {
        button.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() 
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        button.interactable = true;
    }

    public void Connect() 
    {
        if (string.IsNullOrWhiteSpace(inputFieldRoom.text) || string.IsNullOrEmpty(inputFieldRoom.text)) return;
        if (string.IsNullOrWhiteSpace(inputFieldPlayer.text) || string.IsNullOrEmpty(inputFieldPlayer.text)) return;

        PhotonNetwork.NickName = inputFieldPlayer.text;
        RoomOptions option = new RoomOptions();
        option.IsOpen = true;
        option.IsVisible = true;
        option.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom(inputFieldRoom.text, option, TypedLobby.Default);
        button.interactable = false;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        button.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Western");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        button.interactable = true;
    }

}
