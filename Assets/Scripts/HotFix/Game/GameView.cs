using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class GameView : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating(nameof(HandleLobbyHeartbeat), 3, 15);

        Player player = RoomManager.I.GetLocalPlayer();
        Debug.Log($"角色編號:{player.Data[$"{LobbyPlayerDataKeyEnum.Character}"].Value}");

        SceneChangeManager.I.CloseSceneLoadView();
    }

    /// <summary>
    /// 處理房間心跳
    /// </summary>
    private void HandleLobbyHeartbeat()
    {
        RoomManager.I.HandleLobbyHeartbeat();
    }
}
