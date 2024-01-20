using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class CloneAbility : Ability
{
    [SerializeField] private GameObject Clone;

    public override string Name
    {
        get { return "Clone"; }
    }

    public override float Cooldown
    {
        get { return 30f; }
        set { }
    }

    public CloneAbility(GameObject clone)
    {
        Clone = clone;
    }

    public override void Use(GameObject caller)
    {
        PhotonView _photonView = caller.GetComponent<PhotonView>();
        GameObject.Instantiate(Clone, caller.transform.position, caller.transform.rotation);
        _photonView.RPC("RPC_Clone", RpcTarget.Others, Clone, caller.transform.position, caller.transform.rotation);
    }

    [PunRPC]
    public void RPC_Clone(GameObject clone, Vector3 position, Quaternion rotation)
    {
        if (!GetComponent<PhotonView>().IsMine) { return; }

        GameObject.Instantiate(clone, position, rotation);
    }
}
