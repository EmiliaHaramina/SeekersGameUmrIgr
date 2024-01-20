using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public bool isSeeker = false;

    [PunRPC]
    public void SetSeeker()
    {
        GameObject.FindGameObjectsWithTag("SeekerSpawnPoint");
        isSeeker = true;
        gameObject.tag = "seeker";
        gameObject.transform.position = GameObject.FindGameObjectWithTag("SeekerSpawnPoint").transform.position;
    }

    [PunRPC]
    public void SetHider()
    {
        Debug.Log("Hiders set");
        gameObject.tag = "hider";

        float randomX = Random.Range(11f, 16f);
        float randomZ = Random.Range(6f, 11f);
        float fixedY = 1.5f;

        Vector3 randomVector = new Vector3(randomX, fixedY, randomZ);

        gameObject.transform.position = randomVector;
    }
}
