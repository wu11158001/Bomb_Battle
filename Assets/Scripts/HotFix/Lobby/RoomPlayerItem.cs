using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class RoomPlayerItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Character_Txt;
    [SerializeField] TextMeshProUGUI Nickname_Txt;
    [SerializeField] TextMeshProUGUI PrepareStatus_Txt;
    [SerializeField] Button Kick_Btn;
    [SerializeField] Button TransferHost_Btn;
    [SerializeField] GameObject Lock_Obj;

    /// <summary>
    /// 設置房間玩家項目
    /// </summary>
    /// <param name="player"></param>
    /// <param name="isPlayerHost"></param>
    /// <param name="isSelfHost"></param>
    public void SetRoomPlayerItem(Player player, bool isPlayerHost, bool isSelfHost)
    {
        Character_Txt.text = player.Data[$"{LobbyPlayerDataKeyEnum.Character}"].Value;
        Nickname_Txt.text = player.Data[$"{LobbyPlayerDataKeyEnum.PlayerName}"].Value;
        Lock_Obj.SetActive(false);

        TransferHost_Btn.gameObject.SetActive(!isPlayerHost && isSelfHost);
        TransferHost_Btn.onClick.RemoveAllListeners();
        TransferHost_Btn.onClick.AddListener(() =>
        {
            RoomManager.I.TransferRoomHost(player.Id);
        });

        Kick_Btn.gameObject.SetActive(!isPlayerHost && isSelfHost);
        Kick_Btn.onClick.RemoveAllListeners();
        Kick_Btn.onClick.AddListener(() =>
        {
            RoomManager.I.KickPlayer(player.Id);
        });

        if (isPlayerHost)
        {
            /*房主*/

            PrepareStatus_Txt.text = $"<color=#0C31F0>{LanguageManager.I.GetString(LocalizationTableEnum.Room_Table, "Head")}</color>";
        }
        else
        {
            /*一般玩家*/
            bool isPrepare = player.Data[$"{LobbyPlayerDataKeyEnum.IsPrepare}"].Value == "True";
            PrepareStatus_Txt.text = isPrepare ?
                $"<color=#FFF700>{LanguageManager.I.GetString(LocalizationTableEnum.Room_Table, "Prepare")}</color>" :
                $"<color=#B8B673>{LanguageManager.I.GetString(LocalizationTableEnum.Room_Table, "Prepare")}</color>";
        }
    }

    /// <summary>
    /// 設置空的房間玩家項目
    /// </summary>
    /// <param name="isLock"></param>
    public void SetEmptyRoomPlayerItem(bool isLock)
    {
        Character_Txt.text = "";
        Nickname_Txt.text = "";
        PrepareStatus_Txt.text = $"<color=#B8B673>{LanguageManager.I.GetString(LocalizationTableEnum.Room_Table, "Prepare")}</color>";
        Kick_Btn.gameObject.SetActive(false);
        TransferHost_Btn.gameObject.SetActive(false);
        Lock_Obj.SetActive(isLock);
    }
}
