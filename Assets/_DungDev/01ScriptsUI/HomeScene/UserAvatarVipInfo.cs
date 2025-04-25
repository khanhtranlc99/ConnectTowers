using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserAvatarVipInfo : MonoBehaviour
{
    [SerializeField] Image avatar;

    private void OnEnable()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        var rewardSystem = dataVip.LsRewardSystems[UseProfile.CurrentVip];
        this.UpdateUI(rewardSystem.IconVip);

        this.RegisterListener(EventID.UPDATE_AVATAR_VIP, UpdateUI);
    }

     void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_AVATAR_VIP, UpdateUI);
    }

    void UpdateUI(object iconParam)
    {
        if (!(iconParam is Sprite sprite)) return;

        this.avatar.sprite = sprite;
        this.avatar.SetNativeSize();
    }
}
