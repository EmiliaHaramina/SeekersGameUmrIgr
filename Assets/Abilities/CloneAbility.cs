using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        GameObject.Instantiate(Clone, caller.transform.position, caller.transform.rotation);
    }
}
