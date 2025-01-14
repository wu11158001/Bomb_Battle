using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using TMPro;
using Unity.Services.Authentication;
using System.Linq;
using System;

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

    [Space(30)]
    [Header("選擇角色")]
    [SerializeField] List<Button> SelectCharacterBtnList;

    private List<RoomPlayerItem> _roomPlayerItemsList;
    private bool _isPrepare;

    public void OnDestroy()
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

        // 選擇角色按鈕
        for (int i = 0; i < SelectCharacterBtnList.Count; i++)
        {
            int index = i;
            SelectCharacterBtnList[i].onClick.AddListener(() =>
            {
                Dictionary<string, PlayerDataObject> dataDic = new()
                {
                    { $"{LobbyPlayerDataKeyEnum.Character}", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, $"{index}") }
                };
                RoomManager.I.UpdatePlayerData(dataDic);
            });
        }

        // 開始/準備按鈕
        Start_Btn.onClick.AddListener(async () =>
        {
            _isPrepare = !_isPrepare;
            if (RoomManager.I.IsRoomHost())
            {
                /*室長*/

                bool isAllPrepare = true;
                foreach (Player player in RoomManager.I.JoinLobby.Players)
                {
                    bool isPrepare = player.Data[$"{LobbyPlayerDataKeyEnum.IsPrepare}"].Value == "True";
                    if (!isPrepare)
                    {
                        isAllPrepare = false;
                        break;
                    }
                }

                if (isAllPrepare)
                {
                    /*所有玩家已準備*/

                    ViewManager.I.OpenSceneLoadView();

                    RelayConnectionTypeEnum relayConnectionType = RelayConnectionTypeEnum.dtls;
                    string relayJoinCode = await RelayManager.I.CreateRelay(RoomManager.I.JoinLobby.MaxPlayers, relayConnectionType);

                    Dictionary<string, DataObject> dataDic = new()
                    {
                        { $"{LobbyDataKeyEnum.RelayJoinCode}", new DataObject(DataObject.VisibilityOptions.Member, relayJoinCode) },
                        { $"{LobbyDataKeyEnum.RelayConnectionType}", new DataObject(DataObject.VisibilityOptions.Member, $"{relayConnectionType}") },
                    };
                    RoomManager.I.UpdateLobbyData(dataDic);
                }
                else
                {
                    /*有玩家未準備*/
                }
            }
            else
            {
                /*一般玩家*/

                Dictionary<string, PlayerDataObject> dataDic = new()
                {
                    { $"{LobbyPlayerDataKeyEnum.IsPrepare}", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, $"{_isPrepare}") }
                };
                RoomManager.I.UpdatePlayerData(dataDic);
            }            
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

            Dictionary<string, PlayerDataObject> dataDic = new()
            {
                { $"{LobbyPlayerDataKeyEnum.IsPrepare}", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "True") }
            };
            RoomManager.I.UpdatePlayerData(dataDic);
        }
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
        RoomManager.I.RefreshRoom(async (joinLobby) =>
        {
            // 被踢除或房間關閉
            if (!joinLobby.Players.Any(x => x.Id == AuthenticationService.Instance.PlayerId))
            {
                ViewManager.I.CloseCurrView();
                ViewManager.I.OpenView<RectTransform>(ViewEnum.LobbyView);
                return;
            }

            // 更新玩家列表
            for (int i = 0; i < DataManager.MaxRoomPlayers; i++)
            {
                bool isLock = i >= joinLobby.MaxPlayers;
                _roomPlayerItemsList[i].SetEmptyRoomPlayerItem(isLock);
            }

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

            // 進入遊戲
            if (!string.IsNullOrEmpty(joinLobby.Data[$"{LobbyDataKeyEnum.RelayJoinCode}"].Value) &&
                !string.IsNullOrEmpty(joinLobby.Data[$"{LobbyDataKeyEnum.RelayConnectionType}"].Value))
            {
                ViewManager.I.OpenSceneLoadView();

                if (!RoomManager.I.IsRoomHost())
                {
                    string relayJoinCode = joinLobby.Data[$"{LobbyDataKeyEnum.RelayJoinCode}"].Value;
                    RelayConnectionTypeEnum relayConnectionType =
                        (RelayConnectionTypeEnum)Enum.Parse(typeof(RelayConnectionTypeEnum), joinLobby.Data[$"{LobbyDataKeyEnum.RelayConnectionType}"].Value);
                    await RelayManager.I.JoinRelay(relayJoinCode, relayConnectionType);
                }

                SceneChangeManager.I.ChangeScene(SceneEnum.Game);
            }
        });
    }
}
