using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using Newtonsoft.Json;

/// <summary>
/// 用戶資料
/// </summary>
[Serializable]
public class UserData
{
    public UserInfoData UserInfoData;               // 用戶訊息資料
}

/// <summary>
/// 用戶訊息資料
/// </summary>
[Serializable]
public class UserInfoData
{
    public string UserId;                       // 用戶ID
    public string Nickname;                     // 用戶暱稱
}

public class FirebaseManager : UnitySingleton<FirebaseManager>
{
    [Header("主節點_用戶資料")]
    public const string USER_ROOT = "User";                                                     // 用戶資料節點

    [Header("用戶訊息資料")]
    public const string USER_INFO_DATA_ROOT = "UserInfoData";                                   // 用戶訊息資料節點
    public const string USER_ID = "UserId";                                                     // 用戶ID
    public const string USER_NICKNAME = "Nickname";                                             // 用戶暱稱

    private DatabaseReference _databaseRef;

    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        _databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Firebase 初始化完成。");
    }

    /// <summary>
    /// 重新連接
    /// </summary>
    public void OnGoOnline()
    {
        FirebaseDatabase.DefaultInstance.GoOffline();       // 段開連接
        FirebaseDatabase.DefaultInstance.GoOnline();        // 重新連接
    }

    /// <summary>
    /// 獲取用戶資料路徑
    /// </summary>
    /// <returns></returns>
    public string GetUserDataRoot()
    {
        return $"{USER_ROOT}/{DataManager.UserInfoData.UserId}";
    }

    /// <summary>
    /// 獲取用戶訊息資料路徑
    /// </summary>
    /// <returns></returns>
    public string GetUserInfoDataRoot()
    {
        return $"{GetUserDataRoot()}/{USER_INFO_DATA_ROOT}";
    }

    /// <summary>
    /// 寫入資料
    /// </summary>
    /// <param name="path">資料路徑</param>
    /// <param name="data">寫入資料</param>
    /// <param name="callback">callback</param>
    public void WriteData(string path, Dictionary<string, object> data, UnityAction callback = null)
    {
        _databaseRef.Child(path).SetValueAsync(data).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                UnityMainThreadDispatcher.I.Enqueue(() =>
                {
                    callback?.Invoke();
                });
            }
            else
            {
                Debug.LogError("資料寫入失敗: " + task.Exception);
            }
        });
    }

    /// <summary>
    /// 移除資料
    /// </summary>
    /// <param name="path">資料路徑</param>
    /// <param name="callback">callback</param>
    public void RemoveData(string path, UnityAction callback = null)
    {
        _databaseRef.Child(path).RemoveValueAsync().ContinueWith(task => {
            if (task.IsCompleted)
            {
                UnityMainThreadDispatcher.I.Enqueue(() =>
                {
                    callback?.Invoke();
                });
            }
            else
            {
                Debug.LogError("移除資料失敗: " + task.Exception);
            }
        });
    }

    /// <summary>
    /// 讀取資料
    /// </summary>
    /// <param name="path">資料路徑</param>
    /// <param name="callback">讀取結果回傳</param>
    public void ReadData<T>(string path, UnityAction<T> callback) where T : class, new()
    {
        _databaseRef.Child(path).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                DataSnapshot snapshot = task.Result;

                T data = default;
                if (snapshot.Exists)
                {
                    data = JsonUtility.FromJson<T>(snapshot.GetRawJsonValue());
                }

                UnityMainThreadDispatcher.I.Enqueue(() =>
                {
                    if (data == null)
                    {
                        data = new T();
                    }
                    callback?.Invoke(data);
                });
            }
            else
            {
                Debug.LogError("讀取資料失敗: " + task.Exception);
            }
        });
    }

    /// <summary>
    /// 更新資料
    /// </summary>
    /// <param name="path">資料路徑</param>
    /// <param name="updates">更新資料</param>
    /// <param name="callback">callback</param>
    public void UpdateData(string path, Dictionary<string, object> updates, UnityAction callback = null)
    {
        _databaseRef.Child(path).UpdateChildrenAsync(updates).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                UnityMainThreadDispatcher.I.Enqueue(() =>
                {
                    callback?.Invoke();
                });
            }
            else
            {
                Debug.LogError("更新資料失敗: " + task.Exception);
            }
        });
    }

    /// <summary>
    /// 監聽數據變化
    /// </summary>
    /// <param name="path">資料路徑</param>
    /// <param name="callback">callback</param>
    public void ListeningForChanges<T>(string path, UnityAction<T> callback) where T : class
    {
        _databaseRef.Child(path).ValueChanged += (sender, args) =>
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError("Database error: " + args.DatabaseError.Message);
                return;
            }

            if (args.Snapshot.Exists)
            {
                // 將 snapshot 轉換為 Dictionary<string, object>
                Dictionary<string, object> userDataDict = args.Snapshot.Value as Dictionary<string, object>;

                if (userDataDict != null)
                {
                    string jsonString = JsonConvert.SerializeObject(userDataDict);
                    T userData = JsonConvert.DeserializeObject<T>(jsonString);

                    UnityMainThreadDispatcher.I.Enqueue(() =>
                    {
                        callback?.Invoke(userData);
                    });
                }
                else
                {
                    Debug.LogWarning($"{path}:監聽資料不是預期的格式!");
                }
            }
            else
            {
                Debug.LogWarning($"{path}:監聽資料不存在!");
            }
        };
    }

    /// <summary>
    /// 開始監聽在線偵測
    /// </summary>
    public void StartListenerForOnline()
    {
        DatabaseReference userStatusRef = _databaseRef.Child($"{GetUserDataRoot()}/isOnline");

        FirebaseDatabase.DefaultInstance.GetReference(".info/connected").ValueChanged += (sender, args) =>
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError("Database error: " + args.DatabaseError.Message);
                return;
            }

            bool isConnected = (bool)args.Snapshot.Value;
            if (isConnected)
            {
                // 當連接時，設置在線狀態為 true
                userStatusRef.SetValueAsync(true).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        // 設置斷開連接時，Firebase 自動將在線狀態設置為 false
                        userStatusRef.OnDisconnect().SetValue(false);
                        RoomManager.I.UpdatePlayerData(LobbyPlayerDataKeyEnum.IsOnline, "False");
                    }
                });
            }
            else
            {
                Debug.Log("與 Firebase 斷開連線!!!");
            }
        };
    }
}
