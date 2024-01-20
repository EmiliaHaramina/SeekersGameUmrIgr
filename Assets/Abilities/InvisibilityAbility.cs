using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using UnityEngine;
using Photon.Pun;

public class InvisibilityAbility : Ability
{
    private GameObject _caller;
    private float _invisibilityDuration = 3f;

    public override string Name
    {
        get { return "Invisibility"; }
    }

    public override float Cooldown
    {
        get { return 30f; }
        set { }
    }

    public override void Use(GameObject caller)
    {
        _caller = caller;
        PhotonView _photonView = caller.GetComponent<PhotonView>();
        _photonView.RPC("RPC_TurnInvisible", RpcTarget.All);

        caller.transform.Find("PlayerTest").gameObject.SetActive(false);
        //caller.transform.Find("Armors").gameObject.SetActive(false);
        Invoke("TurnVisible", _invisibilityDuration);
    }

    [PunRPC]
    public void RPC_TurnInvisible() {
        if (GetComponent<PhotonView>().IsMine) { return; }

        transform.Find("PlayerTest").gameObject.SetActive(false);
        //transform.Find("Armors").gameObject.SetActive(false);
    }

    public void TurnVisible()
    {
        PhotonView _photonView = _caller.GetComponent<PhotonView>();
        _photonView.RPC("RPC_TurnVisible", RpcTarget.Others);

        _caller.transform.Find("PlayerTest").gameObject.SetActive(true);
        //_caller.transform.Find("Armors").gameObject.SetActive(true);

    }

    [PunRPC]
    public void RPC_TurnVisible()
    {
        if (GetComponent<PhotonView>().IsMine) { return; }

        transform.Find("PlayerTest").gameObject.SetActive(true);
        //transform.Find("Armors").gameObject.SetActive(true);
    }

    public override Ability Get()
    {
        return this;
    }
}
