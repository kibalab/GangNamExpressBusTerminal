
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ColliderToggle : UdonSharpBehaviour
{

    public Collider[] colliders;

    public override void Interact()
    {
        foreach (var col in colliders)
        {
            col.enabled = !col.enabled;
        }
    }
}
