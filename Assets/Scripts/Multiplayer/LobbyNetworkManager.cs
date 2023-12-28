using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _roomInput;
    [SerializeField] private RoomItemUI _roomItemUIPrefab;
    [SerializeField] private Transform _roomListParent;

    [SerializeField] private RoomItemUI _playerItemUIPrefab;
    [SerializeField] private Transform _playerListParent;

    [SerializeField] private TMP_Text _statusField;
    [SerializeField] private Button _leaveRoomButton;

    private List<RoomItemUI> _roomList = new List<RoomItemUI>();
    private List<RoomItemUI> _playerList = new List<RoomItemUI>();
    
    void Start()
    {
        Connect();
        Initialize();
    }

    #region PhotonCallbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected.");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.");
    }

    public override void OnJoinedRoom()
    {
        _statusField.text = "Joined " + PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        _leaveRoomButton.interactable = true;
    }

    public override void OnLeftRoom()
    {
        _statusField.text = "LOBBY";
        Debug.Log("Left room");
        _leaveRoomButton.interactable = false;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }

    #endregion

    private void Initialize()
    {
        _leaveRoomButton.interactable = false;
    }

    private void Connect()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        // Clear the current list of rooms
        for (int i = 0; i < _roomList.Count; i++)
        {
            Destroy(_roomList[i].gameObject);
        }

        _roomList.Clear();

        // Generate a new list with the updated info
        for (int i = 0; i < roomList.Count; i++)
        {
            // skip empty rooms
            if (roomList[i].PlayerCount == 0)
            {
                continue;
            }

            RoomItemUI newRoomItem = Instantiate(_roomItemUIPrefab);
            newRoomItem.lobbyNetworkParent = this;
            newRoomItem.SetName(roomList[i].Name);
            newRoomItem.transform.SetParent(_roomListParent);

            _roomList.Add(newRoomItem);
        }
    }

    private void UpdatePlayerList()
    {
        // Clear the current player list

        // Generate a new list of players
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(_roomInput.text))
        {
            PhotonNetwork.CreateRoom(_roomInput.text, new RoomOptions() { MaxPlayers = 5 }, null);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
