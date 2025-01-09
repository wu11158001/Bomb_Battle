using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;
using Unity.Services.Authentication;

public class RoomView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RoomName_Txt;
    [SerializeField] Button LeaveRoom_Btn;

    [SerializeField] Button TestBtn;

    private Lobby _joinLobby;
    private float _heartbeatTimer;

    private void OnDestroy()
    {
        CancelInvoke(nameof(HandleLobbyHeartbeat));
        CancelInvoke(nameof(UpdateLobby));
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(HandleLobbyHeartbeat));
        CancelInvoke(nameof(UpdateLobby));
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(HandleLobbyHeartbeat), 15, 15);
        InvokeRepeating(nameof(UpdateLobby), 1.1f, 1.1f);
    }


    private void Start()
    {
        EventListener();
    }

    /// <summary>
    /// 事件聆聽
    /// </summary>
    private void EventListener()
    {
        // 離開房間按鈕
        LeaveRoom_Btn.onClick.AddListener(() =>
        {
            RoomManager.I.LeaveLobby();
            ViewManager.I.CloseCurrView();
            ViewManager.I.OpenView<RectTransform>(ViewEnum.LobbyView);
        });




        TestBtn.onClick.AddListener(() =>
        {
            RoomManager.I.UpdateLobbyData(LobbyDataKeyEnum.Map, "3");
        });
    }

    /// <summary>
    /// 處理房間心跳
    /// </summary>
    private void HandleLobbyHeartbeat()
    {
        RoomManager.I.HandleLobbyHeartbeat();
    }

    /// <summary>
    /// 更新房間
    /// </summary>
    private async void UpdateLobby()
    {
        if (_joinLobby != null)
        {
            Lobby lobby = await LobbyService.Instance.GetLobbyAsync(_joinLobby.Id);

            // 被踢除或房間關閉
            if (lobby == null)
            {
                ViewManager.I.CloseCurrView();
                ViewManager.I.OpenView<RectTransform>(ViewEnum.LobbyView);
                return;
            }
            _joinLobby = lobby;
        }

        Debug.Log($"房間地圖:{_joinLobby.Data[$"{LobbyDataKeyEnum.Map}"].Value}");
        Debug.Log($"房主:{_joinLobby.HostId}");
        foreach (Player player in _joinLobby.Players)
        {
            Debug.Log($"房間玩家:{player.Data[$"{LobbyDataKeyEnum.PlayerName}"].Value}");
        }
    }

    /// <summary>
    /// 設置房間訊息
    /// </summary>
    /// <param name="joinLobby"></param>
    public void SetRoomInfo(Lobby joinLobby)
    {
        _joinLobby = joinLobby;

        RoomName_Txt.text = $"{joinLobby.Name}";
    }
}
