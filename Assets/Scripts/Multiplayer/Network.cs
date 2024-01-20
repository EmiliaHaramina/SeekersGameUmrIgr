using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Network : MonoBehaviour
{
    public MasterClient masterClient;

    private GameObject spawnedPlayerPrefab;
    private int nextSpawnIndex = 0;

    [SerializeField] private GameObject[] hiderSpawnPoint;


    private void Start()
    {

        PhotonNetwork.Instantiate("NetworkPlayerPrefab", GetNextSpawnPoint(), Quaternion.identity);
   


        if (PhotonNetwork.IsMasterClient)
        {
            masterClient.Initialize();
        }
    }

    Vector3 GetNextSpawnPoint()
    {
        if (hiderSpawnPoint.Length == 0)
        {
            Debug.LogError("No spawn points assigned.");
            return Vector3.zero;
        }

        Vector3 spawnPoint = hiderSpawnPoint[nextSpawnIndex].transform.position;
        nextSpawnIndex = (nextSpawnIndex + 1) % hiderSpawnPoint.Length; // Move to the next spawn point

        return spawnPoint;
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
