using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;

public class RoomView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RoomName_Txt;

    private Lobby _hostLobby;
    private float _heartbeatTimer;

    private void Update()
    {
        HandleLobbyHeartbeat();
    }

    /// <summary>
    /// 處理房間心跳
    /// </summary>
    private async void HandleLobbyHeartbeat()
    {
        if (_hostLobby != null)
        {
            _heartbeatTimer -= Time.deltaTime;
            if (_heartbeatTimer < 0)
            {
                _heartbeatTimer = 15;
                await LobbyService.Instance.SendHeartbeatPingAsync(_hostLobby.Id);
            }
        }
    }

    /// <summary>
    /// 設置房間訊息
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="roomData"></param>
    public void SetRoomInfo(Lobby lobby, RoomData roomData)
    {
        _hostLobby = lobby;
        RoomName_Txt.text = $"{roomData.RoomName}";
    }
}
