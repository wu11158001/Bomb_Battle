using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RoomName_Txt;
    [SerializeField] TextMeshProUGUI PlayerCount_Txt;
    [SerializeField] Button Join_Btn;

    /// <summary>
    /// 設置房間項目訊息
    /// </summary>
    /// <param name="lobby"></param>
    public void SetRoomItemInfo(Lobby lobby)
    {
        RoomName_Txt.text = lobby.Name;
        PlayerCount_Txt.text = $"{lobby.Players.Count} / {lobby.MaxPlayers}";

        Join_Btn.onClick.RemoveAllListeners();
        Join_Btn.onClick.AddListener(() =>
        {
            RoomManager.I.JoinRoom(lobby, (joinLobby) =>
            {
                ViewManager.I.CloseCurrView();
                ViewManager.I.OpenView<RoomView>(ViewEnum.RoomView, (view) =>
                {
                    view.SetRoomInfo(joinLobby);
                });
            });
        });
    }
}
