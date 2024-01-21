using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationAbility : Ability
{
    [SerializeField] private List<XRRayInteractor> _teleportInteractors;

    private Transform leftHandRig;
    private Transform rightHandRig;
    private float _teleportationDuration = 2f;

    private void Start()
    {
        XROrigin rig = FindObjectOfType<XROrigin>();
        leftHandRig = rig.transform.Find("Camera Offset/Left Controller");
        rightHandRig = rig.transform.Find("Camera Offset/Right Controller");
    }

    public override string Name
    {
        get { return "Teleportation"; }
    }

    public override float Cooldown
    {
        get { return 5f; }
        set { }
    }

    public override Ability Get()
    {
        return this;
    }

    public override void Use(GameObject caller)
    {
        Transform leftTeleportInteractor = leftHandRig.Find("Teleport Interactor");
        leftTeleportInteractor.gameObject.GetComponent<XRRayInteractor>().enabled = true;
        //XRRayInteractor leftRay = leftHandRig.GetComponentInChildren<XRRayInteractor>();
        //Debug.Log(leftRay);
        //leftRay.enabled = true;

        //XRRayInteractor rightRay = rightHandRig.GetComponentInChildren<XRRayInteractor>() ;
        //rightRay.enabled = true;
        Transform rightTeleportInteractor = rightHandRig.Find("Teleport Interactor");

        Debug.Log(rightTeleportInteractor);

        rightTeleportInteractor.gameObject.GetComponent<XRRayInteractor>().enabled = true;

        Invoke("DisableTeleportation", _teleportationDuration);
    }

    private void DisableTeleportation()
    {
        XRRayInteractor leftRay = leftHandRig.GetComponentInChildren<XRRayInteractor>();
        leftRay.enabled = false;

        XRRayInteractor rightRay = rightHandRig.GetComponentInChildren<XRRayInteractor>();
        rightRay.enabled = false;
    }
}
