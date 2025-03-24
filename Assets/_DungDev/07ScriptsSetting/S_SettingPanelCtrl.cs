using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SettingPanelCtrl : MonoBehaviour
{
    [SerializeField] List<S_SettingSlot> lsSettingSlots = new();
    public List<S_SettingSlot> LsSettingSlots => lsSettingSlots;

    private void OnEnable()
    {
        var dataSetting = GameController.Instance.dataContain.dataUser.DataSettingBoxGame;

        foreach (var child in this.lsSettingSlots)
        {
            switch (child.SettingType)
            {
                case SettingType.Music:
                    child.SetBtnOnState(dataSetting.IsMusicOn);
                    child.SetBtnOffState(dataSetting.IsMusicOn);
                    break;
                case SettingType.Sound:
                    child.SetBtnOnState(dataSetting.IsSoundOn);
                    child.SetBtnOffState(dataSetting.IsSoundOn);
                    break;
                case SettingType.Vib:
                    child.SetBtnOnState(dataSetting.IsVibrationOn);
                    child.SetBtnOffState(dataSetting.IsVibrationOn);
                    break;
            }
        }
    }
}
