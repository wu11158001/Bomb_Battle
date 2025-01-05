using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using System.Linq;
using System;

public class Entry : MonoBehaviour
{
    [SerializeField] Button Retry_Btn;
    [SerializeField] TextMeshProUGUI UpdateStr_Txt;

    private static Entry _instance;
    public static Entry I { get { return _instance; } }

    [Header("使用Debug工具")]
    public bool IsUsingDebug;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        if (IsUsingDebug)
        {
            //Debug工具初始化
            Reporter.I.Initialize();
            Reporter.I.show = false;
        }
    }

    private void Start()
    {
        EventListener();
        Retry_Btn.gameObject.SetActive(false);

        StartCoroutine(IUpdateAddressable());
    }

    /// <summary>
    /// 事件聆聽
    /// </summary>
    private void EventListener()
    {
        Retry_Btn.onClick.AddListener(() =>
        {
            Retry_Btn.gameObject.SetActive(false);
            StartCoroutine(IUpdateAddressable());
        });
    }

    /// <summary>
    /// 更新熱更資源
    /// </summary>
    /// <returns></returns>
    private IEnumerator IUpdateAddressable()
    {
        Debug.Log("檢測更新...");

        // 檢測更新
        var checkHandle = Addressables.CheckForCatalogUpdates();
        yield return checkHandle;
        if (checkHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"檢測更新錯誤: {checkHandle.OperationException}");
            OnUpdateError($"Check for catalog updates error: {checkHandle.OperationException}");
            yield break;
        }

        // 更新資源
        if (checkHandle.Result.Count > 0)
        {
            var updateHamdle = Addressables.UpdateCatalogs(checkHandle.Result);
            yield return updateHamdle;

            if (updateHamdle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"更新資源錯誤: {updateHamdle.OperationException}");
                OnUpdateError($"Update cataloge error: {updateHamdle.OperationException}");
                yield break;
            }

            // 更新列表迭代器
            List<IResourceLocator> locators = updateHamdle.Result;
            foreach (var locator in locators)
            {
                List<object> keys = new();
                keys.AddRange(locator.Keys);

                // 獲取等待下載的文件總大小
                var sizeHandle = Addressables.GetDownloadSizeAsync(keys.GetEnumerator());
                yield return sizeHandle;
                if (sizeHandle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"獲取下載文件大小錯誤: {sizeHandle.OperationException}");
                    OnUpdateError($"Get download size async error: {sizeHandle.OperationException}");
                    yield break;
                }

                long totalDownloadSize = sizeHandle.Result;
                UpdateStr_Txt.text = $"Download size:{totalDownloadSize / (1024 * 1024)} MB";
                Debug.Log($"Download size:{ totalDownloadSize / (1024 * 1024)} MB");
                if (totalDownloadSize > 0)
                {
                    // 下載
                    var downloadHandle = Addressables.DownloadDependenciesAsync(keys, Addressables.MergeMode.UseFirst);
                    while (!downloadHandle.IsDone)
                    {
                        if (downloadHandle.Status == AsyncOperationStatus.Failed)
                        {
                            Debug.LogError($"下載錯誤: {downloadHandle.OperationException}");
                            OnUpdateError($"Download dependencies async error: {downloadHandle.OperationException}");
                            yield break;
                        }

                        // 下載進度
                        int progress = (int)downloadHandle.PercentComplete;
                        Debug.Log($"已下載:{progress}");
                        UpdateStr_Txt.text = $"{progress}%";
                        yield return null;

                        if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
                        {
                            Debug.Log("下載完成。");
                            UpdateStr_Txt.text = "下載完成。";
                        }
                    }

                    Addressables.Release(downloadHandle);
                }         
            }

            Addressables.Release(updateHamdle);
        }
        else
        {
            Debug.Log("沒有檢測到更新。");            
        }

        Addressables.Release(checkHandle);

        UpdateStr_Txt.text = "Update complete!";
        StartGame();
    }

    /// <summary>
    /// 更新異常
    /// </summary>
    /// <param name="msg"></param>
    private void OnUpdateError(string msg)
    {
        UpdateStr_Txt.text = $"{msg}, Retry!";
        Retry_Btn.gameObject.SetActive(true);
    }

    /// <summary>
    /// 開始遊戲
    /// </summary>
    /// <param name="ass"></param>
    private void StartGame()
    {
        // 啟動腳本
        Assembly ass = null;
#if UNITY_EDITOR
        ass = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotFix");
        StartLauncher(ass);
#else
        Addressables.LoadAssetAsync<TextAsset>("DLL/HotFix.dll.bytes").Completed += ((handle) =>
        {
            ass = Assembly.Load(handle.Result.bytes);
            StartLauncher(ass);
        });
#endif
    }

    /// <summary>
    /// 啟動遊戲
    /// </summary>
    private void StartLauncher(Assembly ass)
    {
        Type type = ass.GetType("LauncherManager");
        GameObject langcherMgrObj = new GameObject("LauncherManager");
        langcherMgrObj.AddComponent(type);
        type.GetMethod("GameLauncher").Invoke(langcherMgrObj.GetComponent(type), null);
    }
}
