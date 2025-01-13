using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 語言配置表列表
/// </summary>
public enum LocalizationTableEnum
{
    Universal_Table,                      // 共用
    Lobby_Table,                          // 大聽
    Room_Table,                           // 房間
}

public class LanguageManager : UnitySingleton<LanguageManager>
{
    /*
     * 0 = 英文
     * 1 = 繁體中文
     */
    private const string SWALLOW_LANGUAGE = "BombBattle_Language";                          // 本地紀錄

    private Dictionary<LocalizationTableEnum, StringTable> _localizationTableDic;    // 語言配置表
    public int CurrLanguage { get; private set; }                                           // 當前語言


    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public IEnumerator Init()
    {
        _localizationTableDic = new();

        foreach (var tableName in Enum.GetValues(typeof(LocalizationTableEnum)))
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync($"{tableName}");
            yield return loadingOperation;

            if (loadingOperation.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"載入語言配置表錯誤: {tableName}");
                yield break;
            }

            _localizationTableDic.Add((LocalizationTableEnum)tableName, loadingOperation.Result);
        }

        Debug.Log("語言腳本準備完成。");

        int localLanguage = PlayerPrefs.GetInt(SWALLOW_LANGUAGE);
        ChangeLanguage(localLanguage);
    }

    /// <summary>
    /// 獲取文字內容
    /// </summary>
    /// <param name="table"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetString(LocalizationTableEnum table, string key)
    {
        if (_localizationTableDic.ContainsKey(table))
        {
            return _localizationTableDic[table].GetEntry(key).GetLocalizedString();
        }
        else
        {
            Debug.LogError($"獲取文字內容錯誤: table:{table}, key{key}");
            return "";
        }
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
