using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

/// <summary>
/// 房間資料字典Key列表
/// </summary>
public enum LobbyDataKeyEnum
{
    RelayJoinCode,          // Relay加入權杖
    RelayConnectionType,    // Relay通訊方式
    Map,                    // 地圖
}

/// <summary>
/// 房間玩家資料字典Key列表
/// </summary>
public enum LobbyPlayerDataKeyEnum
{
    PlayerName,             // 玩家名稱
    Character,              // 玩家角色
    IsPrepare,              // 準備狀態(True/False)
}

public class RoomManager : UnitySingleton<RoomManager>
{
    public Lobby JoinLobby { get; private set; }                // 加入的房間

    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 處理房間心跳
    /// </summary>
    public async void HandleLobbyHeartbeat()
    {
        if (IsRoomHost())
        {
            await LobbyService.Instance.SendHeartbeatPingAsync(JoinLobby.Id);
        }
    }

    /// <summary>
    /// 是否是室長
    /// </summary>
    /// <returns></returns>
    public bool IsRoomHost()
    {
        if (JoinLobby != null)
        {
            return JoinLobby.HostId == AuthenticationService.Instance.PlayerId;
        }

        return false;
    }

    /// <summary>
    /// 初始化房間玩家
    /// </summary>
    /// <returns></returns>
    private Player InitRoomPlayer()
    {
        return new Player()
        {
            Data = new Dictionary<string, PlayerDataObject>()
            {
                { $"{LobbyPlayerDataKeyEnum.PlayerName}", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, DataManager.UserInfoData.Nickname)},
                { $"{LobbyPlayerDataKeyEnum.Character}", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "0")},
                { $"{LobbyPlayerDataKeyEnum.IsPrepare}", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "Fales")},
            }
        };
    }

    /// <summary>
    /// 獲取本地玩家Player
    /// </summary>
    /// <returns></returns>
    public Player GetLocalPlayer()
    {
        foreach (Player player in JoinLobby.Players)
        {
            if (player.Id == AuthenticationService.Instance.PlayerId)
            {
                return player;
            }
        }

        Debug.LogError("Lobby未找到Player");
        return null;
    }

    /// <summary>
    /// 獲取房間列表
    /// </summary>
    /// <param name="callback"></param>
    public async void GetListRoom(UnityAction<QueryResponse> callback)
    {
        try
        {
            // 篩選排序房間
            QueryLobbiesOptions queryLobbiesOptions = new()
            {
                Order = new()
                {
                    new QueryOrder(true, QueryOrder.FieldOptions.Created)
                }
            };

            // 查詢房間
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            Debug.Log($"房間數量:{queryResponse.Results.Count}");

            UnityMainThreadDispatcher.I.Enqueue(() =>
            {
                callback?.Invoke(queryResponse);
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"刷新房間列表錯誤:{e}");
        }
    }

    /// <summary>
    /// 刷新房間
    /// </summary>
    /// <param name="callback"></param>
    public async void RefreshRoom(UnityAction<Lobby> callback)
    {
        try
        {
            Lobby lobby = await Lobbies.Instance.GetLobbyAsync(JoinLobby.Id);
            JoinLobby = lobby;

            UnityMainThreadDispatcher.I.Enqueue(() =>
            {
                callback?.Invoke(JoinLobby);
            }); 
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"刷新房間錯誤:{e}");
        }
    }

    /// <summary>
    /// 創建房間
    /// </summary>
    /// <param name="roomName"></param>
    /// <param name="maxPlayers"></param>
    /// <param name="callback"></param>
    public async void CreateRoom(string roomName, int maxPlayers, UnityAction<Lobby> callback)
    {
        try
        {
            CreateLobbyOptions createLobbyOptions = new()
            {
                IsPrivate = false,
                Player = InitRoomPlayer(),
                Data = new Dictionary<string, DataObject>()
                {
                    { $"{LobbyDataKeyEnum.RelayJoinCode}", new DataObject(DataObject.VisibilityOptions.Member, "")},
                    { $"{LobbyDataKeyEnum.RelayConnectionType}", new DataObject(DataObject.VisibilityOptions.Member, "")},
                    { $"{LobbyDataKeyEnum.Map}", new DataObject(DataObject.VisibilityOptions.Public, "0")},
                },
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(roomName, maxPlayers, createLobbyOptions);
            JoinLobby = lobby;

            UnityMainThreadDispatcher.I.Enqueue(() =>
            {
                callback?.Invoke(JoinLobby);
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"創建房間錯誤:{e}");
        }
    }

    /// <summary>
    /// 加入房間
    /// </summary>
    /// <param name="joinLobby"></param>
    /// <param name="callback"></param>
    public async void JoinRoom(Lobby joinLobby, UnityAction<Lobby> callback)
    {
        try
        {           
            JoinLobbyByIdOptions joinLobbyByIdOptions = new()
            {
                Player = InitRoomPlayer(),
            };

            Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(joinLobby.Id, joinLobbyByIdOptions);
            JoinLobby = lobby;

            UnityMainThreadDispatcher.I.Enqueue(() =>
            {
                callback?.Invoke(JoinLobby);
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"加入房間錯誤:{e}");
        }
    }

    /// <summary>
    /// 快速加入房間
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="notFindCallback"></param>
    public async void QuickJoinRoom(UnityAction<Lobby> callback, UnityAction notFindCallback)
    {
        try
        {
            QuickJoinLobbyOptions quickJoinLobbyOptions = new()
            {
                Player = InitRoomPlayer(),
            };

            Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync(quickJoinLobbyOptions);
            JoinLobby = lobby;

            UnityMainThreadDispatcher.I.Enqueue(() =>
            {
                callback?.Invoke(JoinLobby);
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log($"'快速加入房間'未找到房間:{e}");
            UnityMainThreadDispatcher.I.Enqueue(() =>
            {
                notFindCallback?.Invoke();
            });
        }
    }

    /// <summary>
    /// 離開房間
    /// </summary>
    public async void LeaveLobby()
    {
        try
        {
            if (JoinLobby != null)
            {
                CancelInvoke(nameof(HandleLobbyHeartbeat));
                await LobbyService.Instance.RemovePlayerAsync(JoinLobby.Id, AuthenticationService.Instance.PlayerId);
                JoinLobby = null;
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"離開房間錯誤:{e}");
        }
    }

    /// <summary>
    /// 踢除玩家
    /// </summary>
    /// <param name="playerId"></param>
    public async void KickPlayer(string playerId)
    {
        try
        {
            if (!IsRoomHost()) return;

            await LobbyService.Instance.RemovePlayerAsync(JoinLobby.Id, playerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"離開房間錯誤:{e}");
        }
    }

    /// <summary>
    /// 更新房間玩家資料
    /// </summary>
    /// <param name="dataDic"></param>
    public async void UpdatePlayerData(Dictionary<string, PlayerDataObject> dataDic)
    {
        try
        {
            if (JoinLobby != null)
            {
                await LobbyService.Instance.UpdatePlayerAsync(JoinLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions()
                {
                    Data = dataDic,
                });
            }  
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"更新房間玩家資料錯誤:{e}");
        }
    }

    /// <summary>
    /// 更新房間資料
    /// </summary>
    /// <param name="dataDic"></param>
    public async void UpdateLobbyData(Dictionary<string, DataObject> dataDic)
    {
        try
        {
            if (!IsRoomHost()) return;

            JoinLobby = await Lobbies.Instance.UpdateLobbyAsync(JoinLobby.Id, new UpdateLobbyOptions()
            {
                Data = dataDic,
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"更新房間資料錯誤:{e}");
        }
    }

    /// <summary>
    /// 轉讓室長
    /// </summary>
    /// <param name="playerId"></param>
    public async void TransferRoomHost(string playerId)
    {
        try
        {
            if (!IsRoomHost()) return;

            JoinLobby = await Lobbies.Instance.UpdateLobbyAsync(JoinLobby.Id, new UpdateLobbyOptions()
            {
                HostId = playerId,
            });

            Dictionary<string, PlayerDataObject> dataDic = new()
            {
                { $"{LobbyPlayerDataKeyEnum.IsPrepare}", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "False") }
            };
            UpdatePlayerData(dataDic);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"更換室長錯誤:{e}");
        }
    }

    /// <summary>
    /// 刪除房間
    /// </summary>
    public async void DeleteLobby()
    {
        try
        {
            if (!IsRoomHost()) return;

            await Lobbies.Instance.DeleteLobbyAsync(JoinLobby.Id);
            JoinLobby = null;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"刪除房間錯誤:{e}");
        }
    }
}
