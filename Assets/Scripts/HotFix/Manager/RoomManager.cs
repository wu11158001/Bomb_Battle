using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// 房間資料字典Key列表
/// </summary>
public enum LobbyDataKeyEnum
{
    PlayerName,             // 玩家名稱
    Map,                    // 地圖
}

public class RoomManager : UnitySingleton<RoomManager>
{
    private Lobby _hostLobby;
    private Lobby _joinLobby;

    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 處理房間心跳
    /// </summary>
    public async void HandleLobbyHeartbeat()
    {
        if (_hostLobby != null)
        {
            await LobbyService.Instance.SendHeartbeatPingAsync(_hostLobby.Id);
        }
    }

    /// <summary>
    /// 獲取房間玩家
    /// </summary>
    /// <returns></returns>
    private Player GetRoomPlayer()
    {
        return new Player()
        {
            Data = new Dictionary<string, PlayerDataObject>()
            {
                {  $"{LobbyDataKeyEnum.PlayerName}", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, DataManager.UserInfoData.Nickname)},
            }
        };
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

            callback?.Invoke(queryResponse);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"刷新房間列表錯誤:{e}");
        }
    }

    /// <summary>
    /// 創建房間
    /// </summary>
    /// <param name="roomName"></param>
    /// <param name="maxPlayers"></param>
    public async void CreateRoom(string roomName, int maxPlayers)
    {
        try
        {
            CreateLobbyOptions createLobbyOptions = new()
            {
                IsPrivate = false,
                Player = GetRoomPlayer(),
                Data = new Dictionary<string, DataObject>()
                {
                    { $"{LobbyDataKeyEnum.Map}", new DataObject(DataObject.VisibilityOptions.Public, "0")}
                },
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(roomName, maxPlayers, createLobbyOptions);
            _hostLobby = lobby;
            _joinLobby = _hostLobby;

            ViewManager.I.CloseCurrView();
            ViewManager.I.CloseCurrView();
            ViewManager.I.OpenView<RoomView>(ViewEnum.RoomView, (view) =>
            {
                view.SetRoomInfo(_joinLobby);
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
    public async void JoinRoom(Lobby joinLobby)
    {
        try
        {
            JoinLobbyByIdOptions joinLobbyByIdOptions = new()
            {
                Player = GetRoomPlayer(),
            };

            Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(joinLobby.Id, joinLobbyByIdOptions);
            _joinLobby = lobby;

            ViewManager.I.CloseCurrView();
            ViewManager.I.OpenView<RoomView>(ViewEnum.RoomView, (view) =>
            {
                view.SetRoomInfo(_joinLobby);
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"加入房間錯誤:{e}");
        }
    }

    /// <summary>
    /// 離開房間
    /// </summary>
    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(_joinLobby.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"離開房間錯誤:{e}");
        }
    }

    /// <summary>
    /// 踢除玩家
    /// </summary>
    /// <param name="playerIndex"></param>
    public async void KickPlayer(int playerIndex)
    {
        try
        {
            if (_hostLobby == null) return;

            await LobbyService.Instance.RemovePlayerAsync(_joinLobby.Id, _joinLobby.Players[playerIndex].Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"離開房間錯誤:{e}");
        }
    }

    /// <summary>
    /// 更新房間資料
    /// </summary>
    /// <param name="updateKey"></param>
    /// <param name="updateValue"></param>
    public async void UpdateLobbyData(LobbyDataKeyEnum updateKey, string updateValue)
    {
        try
        {
            if (_hostLobby == null) return;

            _hostLobby = await Lobbies.Instance.UpdateLobbyAsync(_hostLobby.Id, new UpdateLobbyOptions()
            {
                Data = new Dictionary<string, DataObject>()
                {
                    { $"{updateKey}", new DataObject(DataObject.VisibilityOptions.Public, updateValue)}
                },
            });

            _joinLobby = _hostLobby;

            // 接收房主
            if (_joinLobby.HostId == AuthenticationService.Instance.PlayerId)
            {
                _hostLobby = _joinLobby;
            }
            else
            {
                // 解除房主
                if (_hostLobby != null) _hostLobby = null;
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"更新房間資料錯誤:{e}");
        }
    }

    /// <summary>
    /// 更換房主
    /// </summary>
    /// <param name="playerIndex"></param>
    public async void MigrateLobbyHost(int playerIndex)
    {
        try
        {
            if (_hostLobby == null) return;

            _hostLobby = await Lobbies.Instance.UpdateLobbyAsync(_hostLobby.Id, new UpdateLobbyOptions()
            {
                HostId = _joinLobby.Players[playerIndex].Id,
            });

            _joinLobby = _hostLobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"更換房主錯誤:{e}");
        }
    }

    /// <summary>
    /// 刪除房間
    /// </summary>
    public async void DeleteLobby()
    {
        try
        {
            if (_hostLobby == null) return;

            await Lobbies.Instance.DeleteLobbyAsync(_joinLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"刪除房間錯誤:{e}");
        }
    }
}
