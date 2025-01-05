using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DataManager
{
    #region 用戶訊息資料
    public static UserInfoData UserInfoData { get; set; }                       // 用戶訊息資料

    /// <summary>
    /// 監聽用戶訊息資料回傳
    /// </summary>
    /// <param name="userInfoData"></param>
    public static void UserInfoDataListenerCallback(UserInfoData userInfoData)
    {
        UserInfoData = userInfoData;

        // 更新介面
        UnityMainThreadDispatcher.I.Enqueue(() =>
        {
            // 用戶訊息
            UserInfoView userInfoView = Object.FindFirstObjectByType<UserInfoView>();
            if (userInfoView) userInfoView.UpdateView();
        });
    }
    #endregion
}
