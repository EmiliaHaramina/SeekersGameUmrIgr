using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationAbility : Ability
{
    [SerializeField] private TeleportationProvider _teleportationProvider;

    private float _teleportationDuration = 4f;

    public override string Name
    {
        get { return "Teleportation"; }
    }

    public override float Cooldown
    {
        get { return 6f; }
        set { }
    }

    public override void Use(GameObject caller)
    {
        _teleportationProvider.enabled = true;

        Invoke("DisableTeleportation", _teleportationDuration);
    }

    private void DisableTeleportation()
    {
        _teleportationProvider.enabled = false;
    }
}
