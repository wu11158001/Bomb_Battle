using System.Collections;
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
    Game,                   // 遊戲
}

public class SceneChangeManager : UnitySingleton<SceneChangeManager>
{
    private RectTransform _sceneLoadView;

    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 轉換場景
    /// </summary>
    /// <param name="scene"></param>
    public void ChangeScene(SceneEnum scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }
    private IEnumerator LoadSceneAsync(SceneEnum scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync($"{scene}");

        // 等待場景加載完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log($"進入場景:{scene} !");

        ViewManager.I.ResetViewData();
        _sceneLoadView = ViewManager.I.OpenSceneLoadView();

        // 產生場景初始介面
        switch (scene)
        {
            // 大廳
            case SceneEnum.Lobby:
                ViewManager.I.OpenView<RectTransform>(ViewEnum.LobbyView);
                break;

            // 遊戲
            case SceneEnum.Game:
                ViewManager.I.OpenView<RectTransform>(ViewEnum.GameView);
                break;
        }
    }

    /// <summary>
    /// 關閉場景轉換介面
    /// </summary>
    public void CloseSceneLoadView()
    {
        if (_sceneLoadView != null)
        {
            Destroy(_sceneLoadView.gameObject);
            _sceneLoadView = null;
        }
    }
}
