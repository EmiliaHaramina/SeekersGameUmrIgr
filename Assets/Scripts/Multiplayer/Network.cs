using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Network : MonoBehaviour
{
    private GameObject spawnedPlayerPrefab;

    [SerializeField] private GameObject seekerSpawnPoint;
    [SerializeField] private GameObject hiderSpawnPoint;

    private void Start()
    {
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", seekerSpawnPoint.transform.position, seekerSpawnPoint.transform.rotation);
    }

    //public override void OnJoinedRoom()
    //{
    //    base.OnJoinedRoom();
    //    spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", seekerSpawnPoint.transform.position, seekerSpawnPoint.transform.rotation);
    //}

    //public override void OnLeftRoom()
    //{
    //    base.OnLeftRoom();
    //    PhotonNetwork.Destroy(spawnedPlayerPrefab);
    //}
}
