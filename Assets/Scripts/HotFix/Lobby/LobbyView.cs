using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyView : MonoBehaviour
{
    [SerializeField] Button UserInfo_Btn;

    [Space(30)]
    [Header("房間按鈕")]
    [SerializeField] Button QuickJoinRoom_Btn;
    [SerializeField] Button CreateRoom_Btn;
    [SerializeField] Button RefreshRoom_Btn;

    [Space(30)]
    [Header("房間列表")]
    [SerializeField] RectTransform ListRoomNode;
    [SerializeField] GameObject RoomItemSample;

    private ObjPool _objPool;

    private void OnDestroy()
    {
        CancelInvoke(nameof(GetRoomList));
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(GetRoomList));
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(GetRoomList), 1.5f, 15);
    }

    private void Awake()
    {
        _objPool = new ObjPool(transform);
    }

    private void Start()
    {
        EventListener();
        
        RoomItemSample.SetActive(false);
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

        #region 房間按鈕
        // 快速加入房間按鈕
        QuickJoinRoom_Btn.onClick.AddListener(async () =>
        {
            try
            {
                await Lobbies.Instance.QuickJoinLobbyAsync();
            }
            catch(LobbyServiceException e)
            {
                Debug.LogError($"喎速加入房間錯誤:{e}");
            }
        });
        // 創建房間
        CreateRoom_Btn.onClick.AddListener(() =>
        {
            ViewManager.I.OpenView<RectTransform>(ViewEnum.CreateRoomView);
        });

        // 刷新房間列表按鈕
        RefreshRoom_Btn.onClick.AddListener(() =>
        {
            GetRoomList();
        });
        #endregion
    }

    /// <summary>
    /// 獲取房間列表
    /// </summary>
    private void GetRoomList()
    {
        RoomManager.I.GetListRoom(RefreshListRooms);
    }

    /// <summary>
    /// 刷新房間列表
    /// </summary>
    /// <param name="queryResponse"></param>
    private void RefreshListRooms(QueryResponse queryResponse)
    {
        if (queryResponse.Results.Count > 0)
        {
            List<GameObject> roomItems = _objPool.GetObjList(RoomItemSample);
            int index = 0;
            foreach (var lobby in queryResponse.Results)
            {
                // 產生房間項目
                Debug.Log($"房間名:{lobby.Name}, 最大人數:{lobby.MaxPlayers}, HostId:{lobby.HostId}, Id:{lobby.Id}, code:{lobby.LobbyCode}");
                RoomItem roomItem = null;
                if (index >= roomItems.Count)
                {
                    roomItem = _objPool.CreateObj<RoomItem>(RoomItemSample, ListRoomNode);
                }
                else
                {
                    roomItem = roomItems[index].GetComponent<RoomItem>();
                }
                roomItem.SetRoomItemInfo(lobby);
                index++;
            }

            Utils.I.SetGridLayoutSize(ListRoomNode, false, 4);
        }
    }
}
