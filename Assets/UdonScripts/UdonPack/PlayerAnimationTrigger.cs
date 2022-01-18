
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerAnimationTrigger : UdonSharpBehaviour
{
    public Animator anim;
    public string parameterName;
    
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!player.Equals(Networking.LocalPlayer)) return;
        anim.SetBool(parameterName, true);
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (!player.Equals(Networking.LocalPlayer)) return;
        anim.SetBool(parameterName, false);
    }
}
