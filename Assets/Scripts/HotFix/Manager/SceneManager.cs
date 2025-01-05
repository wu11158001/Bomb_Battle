using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

/// <summary>
/// 場景列表
/// </summary>
public enum SceneEnum
{ 
    Lobby,                  // 大廳
}

public class SceneManager : UnitySingleton<SceneManager>
{
    private RectTransform _sceneLoadView;

    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 轉換場景
    /// </summary>
    /// <param name="sceneEnum"></param>
    public void ChangeScene(SceneEnum sceneEnum)
    {
        Addressables.LoadSceneAsync($"Scenes/{sceneEnum}.unity", LoadSceneMode.Single).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"進入場景:{sceneEnum} !");

                ViewManager.I.ResetViewData();
                _sceneLoadView = ViewManager.I.OpenSceneLoadView();

                // 產生場景初始介面
                switch (sceneEnum)
                {
                    // 大廳
                    case SceneEnum.Lobby:
                        ViewManager.I.OpenView<RectTransform>(ViewEnum.LobbyView, CloseSceneLoadView);
                        break;
                }
            }
            else
            {
                Debug.LogError($"{sceneEnum} 場景載入失敗 ！");
            }
        };
    }

    /// <summary>
    /// 關閉場景轉換介面
    /// </summary>
    /// <param name="openView"></param>
    public void CloseSceneLoadView(RectTransform openView)
    {
        if (_sceneLoadView != null)
        {
            Destroy(_sceneLoadView.gameObject);
            _sceneLoadView = null;
        }
    }
}
