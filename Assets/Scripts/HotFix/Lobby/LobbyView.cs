using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    [SerializeField] Button UserInfo_Btn;

    private void Start()
    {
        EventListener();
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
    }
}
