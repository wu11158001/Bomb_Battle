using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherManager : UnitySingleton<LauncherManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// 遊戲啟動
    /// </summary>
    public void GameLauncher()
    {
        Debug.Log("遊戲啟動");
        GPGSManager.I.LoginGoogle();

        StartCoroutine(IProjectInit());
    }

    /// <summary>
    /// 專案初始準備
    /// </summary>
    /// <returns></returns>
    private IEnumerator IProjectInit()
    {
        yield return ViewManager.I.IPrepare();
        yield return LanguageManager.I.Init();

        SceneManager.I.ChangeScene(SceneEnum.Lobby);
    }
}
