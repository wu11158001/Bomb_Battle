using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeNicknameView : BasePopUpView
{
    [Space(30)]
    [SerializeField] TMP_InputField SetNickname_If;
    [SerializeField] Button Send_Btn;

    protected override void OnEnable()
    {
        base.OnEnable();

        SetNickname_If.text = "";
    }

    private void Start()
    {
        Send_Btn.onClick.AddListener(() =>
        {
            string newNickname = SetNickname_If.text;
            Dictionary<string, object> data = new()
            {
                { FirebaseManager.USER_NICKNAME, newNickname },
            };
            FirebaseManager.I.UpdateData(
                $"{FirebaseManager.I.GetUserInfoDataRoot()}",
                data);

            StartCoroutine(ICloseView());
        });
    }
}
