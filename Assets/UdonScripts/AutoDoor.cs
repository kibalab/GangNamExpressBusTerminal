
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AutoDoor : UdonSharpBehaviour
{
    public Animator anim;
    public string parmName = "";

    public bool isHere = false;
    public bool lastHere = false;

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        isHere = true;
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        isHere = false;
    }

    private void Update()
    {
        if(isHere != lastHere)
        {
            lastHere = isHere;
            anim.SetBool(parmName, isHere);
        }
    }
}
