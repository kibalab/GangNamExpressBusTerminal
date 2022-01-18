
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class KioskUILog : UdonSharpBehaviour
{
    public GameObject UIBox;

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        AddLogBox("연결", $"누군가의 호출로부터 인스턴스의 연결을 재확인합니다.");
        if (player != null) AddLogBox("입장 안내", $"{player.displayName}님이 서울경부에 도착하였습니다.");
    }

    public void AddLogBox(string title, string ctx)
    {
        var log = VRCInstantiate(UIBox);
        log.transform.parent = transform;
        log.transform.localScale = new Vector3(1, 1, 1);
        log.transform.rotation = new Quaternion(0, 0, 0, 0);
        log.transform.localPosition = new Vector3(0, 0, 0);
        log.transform.GetChild(0).GetComponent<Text>().text = title;
        log.transform.GetChild(1).GetComponent<Text>().text = ctx;
        log.SetActive(true);
    }

    private void Start()
    {
        AddLogBox("인스턴스 생성", $"인스턴스가 생성 되었습니다.");
        AddLogBox("키오스크 준비", $"키오스크 시스템이 준비되었습니다.");
        AddLogBox("비디오 플레이어", $"비디오 플레이어 재생을 준비합니다.");
        AddLogBox("아무말 1", $"아 타코야키 먹고싶다.");
        AddLogBox("아무말 2", $"개발은 정말 재밌어");
        SendCustomEventDelayedSeconds(nameof(DeleteLogByTop), 5.0f, VRC.Udon.Common.Enums.EventTiming.Update);
    }

    public void DeleteLogByTop()
    {
        if (transform.childCount >= 5) DeleteLog(0);
        SendCustomEventDelayedSeconds(nameof(DeleteLogByTop), 5.0f, VRC.Udon.Common.Enums.EventTiming.Update);
        return;
    }

    public void DeleteLog(int index)
    {
        Destroy(transform.GetChild(index).gameObject);
    }

}
