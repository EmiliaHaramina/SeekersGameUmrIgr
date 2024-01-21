using UnityEngine;
using Photon.Pun;
using Photon.Voice;
using UnityEngine.UIElements;

public class CloneAbility : Ability
{
    [SerializeField] private GameObject Clone;

    public override string Name
    {
        get { return "Clone"; }
    }

    public override float Cooldown
    {
        get { return 5f; }
        set { }
    }

    public CloneAbility(GameObject clone)
    {
        Clone = clone;
    }

    public override void Use(GameObject caller)
    {
        PhotonView _photonView = caller.GetComponent<PhotonView>();
        //GameObject.Instantiate(Clone, caller.transform.position, caller.transform.rotation);
        Debug.Log(caller.name + "---------------------------------------------------------------CLONE");
        Debug.Log(caller.transform.GetChild(0).GetChild(1).name + "------------------------------CLONE");
        PhotonNetwork.Instantiate(Clone.name, gameObject.transform.position, gameObject.transform.rotation);

        //_photonView.RPC("RPC_Clone", RpcTarget.All, caller.transform.position, caller.transform.rotation);
    }

    [PunRPC]
    public void RPC_Clone(Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.Instantiate(Clone.name, position, rotation);
    }

    public override Ability Get()
    {
        return this;
    }
}
