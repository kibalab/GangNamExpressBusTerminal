
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class SetOwner : UdonSharpBehaviour
{
    public GameObject[] targets;

    public override void Interact()
    {
        foreach (var t in targets)
        {
            Networking.SetOwner(Networking.LocalPlayer, t);
        }
        RequestSerialization();
    }
}
