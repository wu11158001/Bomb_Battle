using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
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
            RoomData roomData = new()
            {
                RoomName = RoomName_If.text,
                MaxPlayer = (int)MaxPlayer_Sli.value,
            };
            CreateRoom(roomData);
        });
    }

    /// <summary>
    /// 創建房間
    /// </summary>
    /// <param name="roomData"></param>
    private async void CreateRoom(RoomData roomData)
    {
        try
        {
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(roomData.RoomName, roomData.MaxPlayer);

            ViewManager.I.CloseCurrView();
            ViewManager.I.OpenView<RoomView>(ViewEnum.RoomView, (view) =>
            {
                view.SetRoomInfo(lobby, roomData);
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }
}
