using UnityEngine;
using UnityEngine.UI;

public class UserInfoView : BasePopUpView
{
    [Space(30)]
    [Header("語言")]
    [SerializeField] Toggle English_Tog;
    [SerializeField] Toggle Chinese_Tog;

    private void Start()
    {
        EventListener();

        switch (LanguageManager.I.CurrLanguage)
        {
            // 英文
            case 0:
                English_Tog.isOn = true;
                break;

            // 繁體中文
            case 1:
                Chinese_Tog.isOn = true;
                break;

            // 預設(英文)
            default:
                English_Tog.isOn = true;
                break;
        }
    }

    /// <summary>
    /// 事件聆聽
    /// </summary>
    private void EventListener()
    {
        #region 語言

        // 英文
        English_Tog.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                LanguageManager.I.ChangeLanguage(0);
            }
        });

        // 繁體中文
        Chinese_Tog.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                LanguageManager.I.ChangeLanguage(1);
            }
        });

        #endregion
    }
}
