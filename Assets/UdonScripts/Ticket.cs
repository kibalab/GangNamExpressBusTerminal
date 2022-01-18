
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class Ticket : UdonSharpBehaviour
{
    [UdonSynced, FieldChangeCallback(nameof(No))] public string m_no;
    [UdonSynced, FieldChangeCallback(nameof(Date))] public string m_date;
    [UdonSynced, FieldChangeCallback(nameof(Time))] public string m_time;
    [UdonSynced, FieldChangeCallback(nameof(Seat))] public int m_seat;
    [UdonSynced, FieldChangeCallback(nameof(Platform))] public int m_platform;
    [UdonSynced, FieldChangeCallback(nameof(Name))] public string m_name;

    public Text[] NoText, DateText, TimeText, SeatText, PlatformText, NameText;

    public string No
    {
        set
        {
            m_no = value;
            NoText[0].text = value;
            NoText[1].text = value;
        }
        get => m_no;
    }

    public string Date
    {
        set
        {
            m_date = value;
            DateText[0].text = value;
            DateText[1].text = value;
        }
        get => m_date;
    }
    public string Time
    {
        set
        {
            m_time = value;
            TimeText[0].text = value;
            TimeText[1].text = value;
        }
        get => m_time;
    }
    public int Seat
    {
        set
        {
            m_seat = value;
            SeatText[0].text = value.ToString("00");
            SeatText[1].text = value.ToString("00");
        }
        get => m_seat;
    }
    public int Platform
    {
        set
        {
            m_platform = value;
            PlatformText[0].text = value.ToString("00");
        }
        get => m_platform;
    }

    public string Name
    {
        set
        {
            m_name = value;
            NameText[0].text = value;
        }
        get => m_name;
    }
}
