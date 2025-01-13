using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Authentication;

public class CreateRoomView : BasePopUpView
{
    [SerializeField] TMP_InputField RoomName_If;
    [SerializeField] TextMeshProUGUI MaxRoomPlaye_Txt;
    [SerializeField] Slider MaxPlayer_Sli;
    [SerializeField] Button Confirm_Btn;

    private void Start()
    {
        EventListener();
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
            RoomManager.I.CreateRoom(roomName, maxPlayers, (joinLobby) =>
            {
                ViewManager.I.CloseCurrView();
                ViewManager.I.CloseCurrView();
                ViewManager.I.OpenView<RoomView>(ViewEnum.RoomView, (view) =>
                {
                    view.SetRoomInfo(joinLobby);
                });
            });
        });
    }

    /// <summary>
    /// 設置創建房間介面
    /// </summary>
    public void SetCreateRoomView()
    {
        MaxPlayer_Sli.minValue = 2;
        MaxPlayer_Sli.maxValue = DataManager.MaxRoomPlayers;
        MaxPlayer_Sli.value = DataManager.MaxRoomPlayers;

        MaxRoomPlaye_Txt.text = $"{(int)MaxPlayer_Sli.value}";

        RoomName_If.text = DataManager.UserInfoData.Nickname;
    }
}
