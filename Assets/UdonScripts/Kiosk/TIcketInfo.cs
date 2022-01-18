
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class TIcketInfo : UdonSharpBehaviour
{
    public Text NumText, nameText, birthText, dayText;
    [UdonSynced, FieldChangeCallback(nameof(Num))] public string m_num;
    [UdonSynced, FieldChangeCallback(nameof(Name))] public string m_name;
    [UdonSynced, FieldChangeCallback(nameof(Birth))] public string m_birth;
    [UdonSynced, FieldChangeCallback(nameof(Day))] public string m_day;

    public string Num
    {
        set
        {
            m_num = value;
            NumText.text = value;
        }
        get => m_num;
    }

    public string Name
    {
        set
        {
            m_name = value;
            nameText.text = value;
        }
        get => m_name;
    }

    public string Birth
    {
        set
        {
            m_birth = value;
            birthText.text = value;
        }
        get => m_birth;
    }

    public string Day
    {
        set
        {
            m_day = value;
            dayText.text = value;
        }
        get => m_day;
    }

    private void OnEnable()
    {
#if !UNITY_EDITOR
        if (!Networking.IsOwner(gameObject))
            return;
#endif

        Num = UnityEngine.Random.Range(0, 99999999999).ToString("00000000000");
#if !UNITY_EDITOR
        Name = Networking.LocalPlayer.displayName + $".{Networking.LocalPlayer.playerId}";
#endif
        DateTime start = new DateTime(1920, 1, 1);
        int range = (DateTime.Today - start).Days;
        Birth = start.AddDays(UnityEngine.Random.Range(0, 36500)).ToString("yyyy년 MM월 dd일");
        Day = DateTime.Now.ToString("yyyy년 MM월 dd일");

        RequestSerialization();
    }
}
