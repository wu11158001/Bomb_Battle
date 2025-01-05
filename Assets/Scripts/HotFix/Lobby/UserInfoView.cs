using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInfoView : BasePopUpView
{
    [Space(30)]
    [Header("基本訊息")]
    [SerializeField] TextMeshProUGUI Nickname_Txt;
    [SerializeField] Button SetNickname_Btn;

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

        UpdateView();
    }

    /// <summary>
    /// 事件聆聽
    /// </summary>
    private void EventListener()
    {
        #region 基本訊息
        // 設置暱稱按鈕
        SetNickname_Btn.onClick.AddListener(() =>
        {
            ViewManager.I.OpenView<RectTransform>(ViewEnum.ChangeNicknameView);
        });
        #endregion

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

    /// <summary>
    /// 更新介面
    /// </summary>
    public void UpdateView()
    {
        Nickname_Txt.text = DataManager.UserInfoData.Nickname;
    }
}
