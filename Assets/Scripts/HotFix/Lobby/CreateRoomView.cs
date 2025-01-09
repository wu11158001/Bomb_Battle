using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRoomView : BasePopUpView
{
    [SerializeField] TMP_InputField RoomName_If;
    [SerializeField] TextMeshProUGUI MaxRoomPlaye_Txt;
    [SerializeField] Slider MaxPlayer_Sli;
    [SerializeField] Button Confirm_Btn;

    private void Start()
    {
        EventListener();

        MaxRoomPlaye_Txt.text = $"{(int)MaxPlayer_Sli.value}";
    }

    /// <summary>
    /// 事件聆聽
    /// </summary>
    private void EventListener()
    {
        // 最大人數滑條
        MaxPlayer_Sli.onValueChanged.AddListener((value) =>
        {
            MaxRoomPlaye_Txt.text = $"{(int)value}";
        });

        // 確認按鈕
        Confirm_Btn.onClick.AddListener(() =>
        {
            string roomName = RoomName_If.text;
            int maxPlayers = (int)MaxPlayer_Sli.value;
            RoomManager.I.CreateRoom(roomName, maxPlayers);
        });
    }
}
