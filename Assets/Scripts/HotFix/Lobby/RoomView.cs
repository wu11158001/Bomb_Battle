using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;
using Unity.Services.Authentication;
using System.Linq;

public class RoomView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RoomName_Txt;
    [SerializeField] Button LeaveRoom_Btn;
    [SerializeField] Button Start_Btn;
    [SerializeField] TextMeshProUGUI StartBtn_Txt;

    [Space(30)]
    [Header("玩家列表")]
    [SerializeField] RectTransform ListPlayersNode;
    [SerializeField] GameObject RoomPlayerItemSample;

    private List<RoomPlayerItem> _roomPlayerItemsList;
    private bool _isPrepare;

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
        InvokeRepeating(nameof(UpdateLobby), 0, 1.1f);
    }

    private void Start()
    {
        EventListener();
        CreatePlayerItem();
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

        // 開始/準備按鈕
        Start_Btn.onClick.AddListener(() =>
        {
            _isPrepare = !_isPrepare;
            if (RoomManager.I.IsRoomHost())
            {
                /*室長*/

            }
            else
            {
                RoomManager.I.UpdatePlayerData(LobbyPlayerDataKeyEnum.IsPrepare, $"{_isPrepare}");
            }            
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
    /// 創建玩家項目
    /// </summary>
    private void CreatePlayerItem()
    {
        _roomPlayerItemsList = new();
        RoomPlayerItemSample.SetActive(false);
        for (int i = 0; i < DataManager.MaxRoomPlayers; i++)
        {
            GameObject obj = Instantiate(RoomPlayerItemSample, ListPlayersNode);
            obj.SetActive(true);
            _roomPlayerItemsList.Add(obj.GetComponent<RoomPlayerItem>()); 
        }
    }

    /// <summary>
    /// 更新房間
    /// </summary>
    private void UpdateLobby()
    {
        RoomManager.I.RefreshRoom((joinLobby) =>
        {
            // 被踢除或房間關閉
            if (!joinLobby.Players.Any(x => x.Id == AuthenticationService.Instance.PlayerId))
            {
                ViewManager.I.CloseCurrView();
                ViewManager.I.OpenView<RectTransform>(ViewEnum.LobbyView);
                return;
            }

            for (int i = 0; i < DataManager.MaxRoomPlayers; i++)
            {
                bool isLock = i >= joinLobby.MaxPlayers;
                _roomPlayerItemsList[i].SetEmptyRoomPlayerItem(isLock);
            }

            Debug.Log($"房間地圖:{joinLobby.Data[$"{LobbyDataKeyEnum.Map}"].Value}");
            int index = 0;
            foreach (Player player in joinLobby.Players)
            {
                bool isPlayerHost = joinLobby.HostId == player.Id;
                bool isSelfHost = RoomManager.I.IsRoomHost();
                _roomPlayerItemsList[index++].SetRoomPlayerItem(player, isPlayerHost, isSelfHost);
            }

            // 準備/開始按鈕
            StartBtn_Txt.text = RoomManager.I.IsRoomHost() ?
                LanguageManager.I.GetString(LocalizationTableEnum.Room_Table, "Start") :
                LanguageManager.I.GetString(LocalizationTableEnum.Room_Table, "Prepare");
        });
    }

    /// <summary>
    /// 設置房間訊息
    /// </summary>
    /// <param name="joinLobby"></param>
    public void SetRoomInfo(Lobby joinLobby)
    {
        RoomName_Txt.text = $"{joinLobby.Name}";

        if (RoomManager.I.IsRoomHost())
        {
            /*室長*/
            
            RoomManager.I.UpdatePlayerData(LobbyPlayerDataKeyEnum.IsPrepare, $"{true}");
        }
    }
}
