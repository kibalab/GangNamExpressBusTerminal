
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class PhysicsRotateSync : UdonSharpBehaviour
{
    public Rigidbody rigidbody;

    [UdonSynced, FieldChangeCallback(nameof(Rotate))] public Vector3 RotationAxis = Vector3.zero;

    public Vector3 Rotate
    {
        set
        {
            if (!Networking.IsOwner(gameObject)) rigidbody.angularVelocity = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
    }

    private void LateUpdate()
    {
        if (Networking.IsOwner(gameObject))
        {
            RotationAxis = rigidbody.angularVelocity;
            RequestSerialization();
        }
    }
}
