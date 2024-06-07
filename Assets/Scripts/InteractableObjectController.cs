using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableObjectController : XRGrabInteractable
{
    private bool resetPhysicsNextFrame = false;
//     void Start()
// {
//     Rigidbody rb = GetComponent<Rigidbody>();
//     if (rb != null)
//     {
//         rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
//     }
// }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        SetKinematic(false);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
    SetKinematic(true);
    resetPhysicsNextFrame = true; // Set flag to reset physics in next FixedUpdate
    }

    private void SetKinematic(bool isKinematic)
    {
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().isKinematic = isKinematic;
            // if (isKinematic)
            // {
            //     GetComponent<Rigidbody>().velocity = Vector3.zero;
            //     GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            // }
        }
    }

//     void Awake()
// {
//     Rigidbody rb = GetComponent<Rigidbody>();
//     if (rb != null)
//     {
//         rb.sleepThreshold = 0.0f;  // Adjust or remove if it affects performance
//     }
// }

    void FixedUpdate()
{
    if (resetPhysicsNextFrame)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        resetPhysicsNextFrame = false;
    }
}}

