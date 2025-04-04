using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SettingPanelCtrl : MonoBehaviour
{
    [SerializeField] List<S_SettingSlot> lsSettingSlots = new();
    public List<S_SettingSlot> LsSettingSlots => lsSettingSlots;

    private void OnEnable()
    {
        foreach (var child in this.lsSettingSlots)
        {
            switch (child.SettingType)
            {
                case SettingType.Music:
                    child.SetBtnState(GameController.Instance.useProfile.OnMusic);
                    break;
                case SettingType.Sound:
                    child.SetBtnState(GameController.Instance.useProfile.OnSound);
                    break;
                case SettingType.Vib:
                    child.SetBtnState(GameController.Instance.useProfile.OnVibration);
                    break;
            }
        }
    }
}
