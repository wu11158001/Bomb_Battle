using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class LauncherManager : UnitySingleton<LauncherManager>
{
    /// <summary>
    /// 讀取資料狀態列表
    /// </summary>
    private enum ReadDataStatusEnum
    {
        UserInfoData,                       // 用戶資料
    }

    /// <summary>
    /// 讀取資料狀態
    /// </summary>
    private Dictionary<ReadDataStatusEnum, bool> _readDataStatus = new()
    {
        { ReadDataStatusEnum.UserInfoData, false},                  // 用戶資料
    };

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        new GameObject("UnityMainThreadDispatcher").AddComponent<UnityMainThreadDispatcher>();
    }

    /// <summary>
    /// 遊戲啟動
    /// </summary>
    public void GameLauncher()
    {
        Debug.Log("遊戲啟動");

#if UNITY_EDITOR
        DataManager.UserInfoData = new()
        {
            UserId = "EditorUserId",
            Nickname = "EditorNickname",
        };
        ReadUserData();
#else
         GPGSManager.I.LoginGoogle(ReadUserData);
#endif
    }

    /// <summary>
    /// 讀取用戶資料
    /// </summary>
    private void ReadUserData()
    {
        FirebaseManager.I.Init();
        FirebaseManager.I.ReadData<UserData>(
            $"{FirebaseManager.I.GetUserDataRoot()}",
            ReadUserDataCallback);
    }

    /// <summary>
    /// 讀取用戶資料回傳
    /// </summary>
    /// <param name="userData"></param>
    private void ReadUserDataCallback(UserData userData)
    {
        if (userData.UserInfoData == null)
        {
            /*新用戶*/

            // 用戶訊息
            Dictionary<string, object> data = new()
            {
                { FirebaseManager.USER_ID, DataManager.UserInfoData.UserId},
                { FirebaseManager.USER_NICKNAME, DataManager.UserInfoData.Nickname },
            };
            FirebaseManager.I.WriteData(
                $"{FirebaseManager.I.GetUserInfoDataRoot()}",
                data,
                () =>
                {
                    CheckDataStatus(ReadDataStatusEnum.UserInfoData);
                });
        }
        else
        {
            /*設置資料*/

            // 用戶訊息
            DataManager.UserInfoData = userData.UserInfoData;
            CheckDataStatus(ReadDataStatusEnum.UserInfoData);
        }
    }

    /// <summary>
    /// 檢查資料獲取狀態
    /// </summary>
    /// <param name="key"></param>
    private void CheckDataStatus(ReadDataStatusEnum key)
    {
        Debug.Log($"{key}:Database資料讀取完成。");
        _readDataStatus[key] = true;

        bool isLoadComplete = _readDataStatus.All(x => x.Value == true);
        if (isLoadComplete)
        {
            /*資料載入完成*/

            // 開始監聽用戶訊息
            FirebaseManager.I.ListeningForChanges<UserInfoData>(
                $"{FirebaseManager.I.GetUserInfoDataRoot()}",
                (userInfoData) =>
                {
                    DataManager.UserInfoDataListenerCallback(userInfoData);
                });

            // 專案初始準備
            StartCoroutine(IProjectInit());
        }
    }

    /// <summary>
    /// 專案初始準備
    /// </summary>
    /// <returns></returns>
    private IEnumerator IProjectInit()
    {
        yield return UnityServices.InitializeAsync();
        yield return AuthenticationService.Instance.SignInAnonymouslyAsync();
        yield return ViewManager.I.IPrepare();
        yield return LanguageManager.I.Init();

        FirebaseManager.I.StartListenerForOnline();
        SceneChangeManager.I.ChangeScene(SceneEnum.Lobby);
    }
}
