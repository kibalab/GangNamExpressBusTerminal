
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

public class TicketSpawner : UdonSharpBehaviour
{
    public TIcketInfo Info;

    public VRCObjectPool TicketPool;

    public Transform SpawnPosition;

    public override void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, TicketPool.gameObject);

        var o = TicketPool.TryToSpawn();

        if (!o) { Debug.Log("Ticket Spawn Failed"); return; }

        Networking.SetOwner(Networking.LocalPlayer, o);

        o.transform.SetPositionAndRotation(SpawnPosition.position, SpawnPosition.rotation);

        SetInfo(o.GetComponent<Ticket>());

        o.SetActive(true);
    }

    public void SetInfo(Ticket t)
    {
        t.No = Info.NumText.text;
        t.Date = Info.dayText.text.Replace("년", ".").Replace("월", ".").Replace("일", ".");
        t.Time = $"{Random.Range((int)0, 23).ToString("00")}:{Random.Range((int)0, 59).ToString("00")}";
        t.Seat = Random.Range((int)0, 45);
        t.Platform = Random.Range((int)0, 99);
        t.Name = t.NameText[0].text = Info.nameText.text;
    }
}
