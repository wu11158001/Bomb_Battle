using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class LanguageManager : UnitySingleton<LanguageManager>
{
    /*
     * 0 = 英文
     * 1 = 繁體中文
     */
    private const string SWALLOW_LANGUAGE = "BombBattle_Language";          // 本地紀錄

    public int CurrLanguage { get; private set; }                           // 當前語言

    /// <summary>
    /// 語言配置表
    /// </summary>
    private string[] LocalizationTable = new string[]
    {
        "Lobby_Table",
    };

    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public IEnumerator Init()
    {
        foreach (var table in LocalizationTable)
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(table);
            yield return loadingOperation;

            if (loadingOperation.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"載入語言配置表錯誤: {table}");
            }
        }

        Debug.Log("語言腳本準備完成。");

        int localLanguage = PlayerPrefs.GetInt(SWALLOW_LANGUAGE);
        ChangeLanguage(localLanguage);
    }

    /// <summary>
    /// 更換語言
    /// </summary>
    /// <param name="index"></param>
    public void ChangeLanguage(int index)
    {
        AsyncOperationHandle handle = LocalizationSettings.SelectedLocaleAsync;
        if (handle.IsDone)
        {
            SetLanguage(index);
        }
        else
        {
            handle.Completed += (OperationHandle) =>
            {
                SetLanguage(index);
            };
        }
    }

    /// <summary>
    /// 設置語言
    /// </summary>
    /// <param name="index"></param>
    private void SetLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        PlayerPrefs.SetInt(SWALLOW_LANGUAGE, index);
        CurrLanguage = index;

        Debug.Log($"當前語言: {index}");
    }
}
