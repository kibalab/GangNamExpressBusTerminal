
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SubtitleSystem : UdonSharpBehaviour
{
    public TextAsset SUBTITLES;

    public AudioSource audio;

    public Text text;

    public int NextID = 0;

    public int[] times;
    public string[] texts;

    private void Start()
    {
        Read();
    }

    public void Read()
    {
        var subtitles = SUBTITLES.text.Split('\n');

        times = new int[subtitles.Length];
        texts = new string[subtitles.Length];

        for (var i = 0; i< subtitles.Length; i++)
        {
            var splited = subtitles[i].Split(',');
            times[i] = ParseSeconde(splited[0]); // Format : 00:00 
            texts[i] = splited[1];
        }
    }

    private int ParseSeconde(string time)
    {
        var times = time.Split(':');
        return int.Parse(times[0]) * 60 + int.Parse(times[1]);
    }


    private void Update()
    {
        Debug.Log($"{audio.time}, {times[NextID]}");
        if(audio.time >= times[NextID])
        {
            text.text = texts[NextID];

            NextID++;
        }
    }
}
