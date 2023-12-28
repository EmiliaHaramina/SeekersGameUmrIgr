using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomItemUI : MonoBehaviour
{
    public LobbyNetworkManager lobbyNetworkParent;
    [SerializeField] private TMP_Text _roomName;

    public void SetName(string roomName)
    {
        _roomName.text = roomName;
    }

    public void OnJoinPressed()
    {
        lobbyNetworkParent.JoinRoom(_roomName.text);
    }
}
