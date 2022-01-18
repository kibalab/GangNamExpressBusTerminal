
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Video.Components;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class PlayerQueue : UdonSharpBehaviour
{
    public bool isFirstWake = false;

    public VRCUrl[] Urls;

    public VRCUnityVideoPlayer player;

    public PlayerQueue nextLoad;

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(Index))]public int currentIndex = -1;

    public int Index
    {
        set
        {
            if(value >= Urls.Length) value = 0;

            player.PlayURL(Urls[value]);
            currentIndex = value;
            RequestSerialization();
        }
        get => currentIndex;
    }

    private void Start()
    {
        if (!isFirstWake) return;
#if !UNITY_EDITOR
        player.enabled = true;

        player.Loop = false;

        if(Networking.IsOwner(Networking.LocalPlayer, gameObject)) Index++;
#endif
    }

    public void Wake()
    {
        isFirstWake = true;
        SendCustomEventDelayedSeconds(nameof(Start), 15, VRC.Udon.Common.Enums.EventTiming.Update);
    }

    public override void OnVideoPlay()
    {
        if(nextLoad) nextLoad.Wake();
    }

    public override void OnVideoEnd()
    {
        Index++;
    }
}
