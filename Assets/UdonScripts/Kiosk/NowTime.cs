
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class NowTime : UdonSharpBehaviour
{
    public Text dateText;
    public Text timeText;

    private void Update()
    {
        dateText.text = DateTime.Now.ToString("yyyy-MM-dd");
        timeText.text = DateTime.Now.ToString("HH:mm");
    }
}
