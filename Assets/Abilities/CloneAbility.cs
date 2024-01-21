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
        Debug.Log(caller.name + "---------------------------------------------------------------CLONE");
        Debug.Log(caller.transform.GetChild(0).GetChild(1).name + "------------------------------CLONE");
        PhotonNetwork.Instantiate(Clone.name, gameObject.transform.position, gameObject.transform.rotation);
    }

    public override Ability Get()
    {
        return this;
    }
}
