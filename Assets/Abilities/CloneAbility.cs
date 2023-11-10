using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneAbility : Ability
{
    public override string Name
    {
        get { return "Clone"; }
    }
    public override float Cooldown
    {
        get { return 5f; }
        set { }
    }

    public GameObject Clone;

    public CloneAbility(GameObject clone)
    {
        Clone = clone;
    }

    public override void Use(GameObject caller)
    {
        GameObject.Instantiate(Clone, caller.transform.position, caller.transform.rotation);
    }
}
