using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using TMPro;
using System;

/// <summary>
/// 一般介面列表
/// </summary>
public enum ViewEnum
{
    LobbyView,                      // 大廳
    UserInfoView,                   // 用戶訊息
}

/// <summary>
/// 常駐介面列表
/// </summary>
public enum PermanentViewEnum
{
    SceneLoadView,                  // 場景轉換介面
}

public class ViewManager : UnitySingleton<ViewManager>
{
    private Queue<RectTransform> _openedView = new();                                   // 已開啟介面

    private Dictionary<ViewEnum, RectTransform> _normalView = new();                    // 一般介面
    private Dictionary<PermanentViewEnum, RectTransform> _permanentView = new();        // 常駐介面

    private TMP_FontAsset _font;

    private RectTransform _canvasRt;

    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 介面腳本準備
    /// </summary>
    /// <returns></returns>
    public IEnumerator IPrepare()
    {
        // 常駐介面
        _permanentView.Clear();
        foreach (var permanentEnum in Enum.GetValues(typeof(PermanentViewEnum)))
        {
            var permanentHandle = Addressables.LoadAssetAsync<GameObject>($"Prefab/View/{permanentEnum}.prefab");

            yield return permanentHandle;

            if (permanentHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                RectTransform rt = permanentHandle.Result.GetComponent<RectTransform>();
                _permanentView.Add((PermanentViewEnum)permanentEnum, rt);
            }
            else
            {
                Debug.LogError($"加載 {permanentEnum} 失敗 !");
            }
        }

        // 字體
        var fontHandle = Addressables.LoadAssetAsync<TMP_FontAsset>("TmpFont/思源宋體-Medium.asset");
        yield return fontHandle;
        _font = fontHandle.Result;

        Debug.Log("介面腳本準備完成。");
    }

    /// <summary>
    /// 設置當前場景Canvas
    /// </summary>
    private void SetCanvas()
    {
        _canvasRt = FindAnyObjectByType<Canvas>().GetComponent<RectTransform>();
    }

    /// <summary>
    /// 重製介面資料
    /// </summary>
    public void ResetViewData()
    {
        SetCanvas();
        _openedView.Clear();
        _normalView.Clear();
    }

    /// <summary>
    /// 關閉介面
    /// </summary>
    public void CloseView()
    {
        _openedView.Peek().gameObject.SetActive(false);
        _openedView.Dequeue();
    }

    /// <summary>
    /// 開啟介面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="viewEnum"></param>
    /// <param name="callback"></param>
    public void OpenView<T>(ViewEnum viewEnum, UnityAction<T> callback = null) where T : Component
    {
        if (_normalView.ContainsKey(viewEnum))
        {
            RectTransform rt = Instantiate(_normalView[viewEnum], _canvasRt).GetComponent<RectTransform>();
            CreateViewHandle(rt, callback);

            _openedView.Enqueue(rt);
        }
        else
        {
            Addressables.LoadAssetAsync<GameObject>($"Prefab/View/{viewEnum}.prefab").Completed += (handle) =>
            {
                if (handle.Result != null)
                {
                    RectTransform rt = Instantiate(handle.Result, _canvasRt).GetComponent<RectTransform>();
                    CreateViewHandle(rt, callback);

                    _openedView.Enqueue(rt);
                    _normalView.Add(viewEnum, rt);
                    Addressables.Release(handle);
                }
                else
                {
                    Debug.LogError($"無法加載介面:{viewEnum}");
                }
            };
        }
    }

    /// <summary>
    /// 開啟場景轉換介面
    /// </summary>
    public RectTransform OpenSceneLoadView()
    {
        RectTransform sceneLoadView = _permanentView[PermanentViewEnum.SceneLoadView];
        RectTransform rt = Instantiate(sceneLoadView, _canvasRt).GetComponent<RectTransform>();
        CreateViewHandle<RectTransform>(rt);

        return rt;
    }

    /// <summary>
    /// 產生介面處理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rt"></param>
    /// <param name="callBack"></param>
    public void CreateViewHandle<T>(RectTransform rt, UnityAction<T> callBack = null) where T : Component
    {
        rt.gameObject.SetActive(true);
        rt.offsetMax = Vector2.zero;
        rt.offsetMin = Vector2.zero;
        rt.anchoredPosition = Vector2.zero;
        rt.eulerAngles = Vector3.zero;
        rt.localScale = Vector3.one;
        rt.name = rt.name.Replace("(Clone)", "");
        rt.SetSiblingIndex(_canvasRt.childCount + 1);

        // 字體
        TextMeshProUGUI[] texts = rt.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var text in texts)
        {
            text.font = _font;
        }

        // 獲取Component
        if (callBack != null)
        {
            T component = rt.GetComponent<T>();
            if (component != null)
            {
                callBack?.Invoke(component);
            }
            else
            {
                Debug.LogError($"{rt.name}: 介面不存在 Component");
            }
        }
    }
}
