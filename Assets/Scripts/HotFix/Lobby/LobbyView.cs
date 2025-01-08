using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

/// <summary>
/// 房間資料
/// </summary>
public class RoomData
{
    public string RoomName;             // 房間名
    public int MaxPlayer;               // 最大房間數量
}

public class LobbyView : MonoBehaviour
{
    [SerializeField] Button UserInfo_Btn;
    [SerializeField] Button CreateRoom_Btn;

    private void Start()
    {
        EventListener();
        InvokeRepeating(nameof(UpdateListLobbyies), 0, 5);
    }

    /// <summary>
    /// 事件聆聽
    /// </summary>
    private void EventListener()
    {
        // 用戶訊息按鈕
        UserInfo_Btn.onClick.AddListener(() =>
        {
            ViewManager.I.OpenView<RectTransform>(ViewEnum.UserInfoView);
        });

        // 創建房間
        CreateRoom_Btn.onClick.AddListener(() =>
        {
            ViewManager.I.OpenView<RectTransform>(ViewEnum.CreateRoomView);
        });
    }

    /// <summary>
    /// 刷新房間列表
    /// </summary>
    private async void UpdateListLobbyies()
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
            foreach (var lobby in queryResponse.Results)
            {
                Debug.Log($"房間名:{lobby.Name}, 最大人數:{lobby.MaxPlayers}, HostId:{lobby.HostId}, Id:{lobby.Id}");
            }
        }
        catch(LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }
}
