using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPGSManager : UnitySingleton<GPGSManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 登入Google
    /// </summary>
    public void LoginGoogle()
    {
        PlayGamesPlatform.Activate().Authenticate((status) =>
        {
            if (status == SignInStatus.Success)
            {
                string userId = PlayGamesPlatform.Instance.GetUserId();
                string nickName = PlayGamesPlatform.Instance.GetUserDisplayName();
                string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

                Debug.Log($"用戶登入:{userId}/ID:{nickName}");
            }
            else
            {

                Debug.LogError("Google 登入失敗!!!");
            }
        });
    }
}
