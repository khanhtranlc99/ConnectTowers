using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SettingType
{
    Music,
    Sound,
    Vib
}


public class S_SettingSlot : LoadAutoComponents
{
    [SerializeField] SettingType settingType;
    public SettingType SettingType => settingType;
    [SerializeField] Button btnOn;
    public Button BtnOn => btnOn;
    [SerializeField] Button btnOff;
    public Button BtnOff => btnOff;

    bool isState = false;

    private void Start()
    {
        this.btnOn.onClick.AddListener(HandleButtonOn);
        this.btnOff.onClick.AddListener(HandleButtonOff);
    }
    void HandleButtonOn()
    {
        this.btnOff.gameObject.SetActive(true);
        this.btnOn.gameObject.SetActive(false);

        this.isState = false;
        this.HandleSettingData(this.isState);
    }
    void HandleButtonOff()
    {
        this.btnOff.gameObject.SetActive(false);
        this.btnOn.gameObject.SetActive(true);
        

        this.isState = true;
        this.HandleSettingData(this.isState);
    }

    void HandleSettingData(bool isBool)
    {
        var dataSetting = GameController.Instance.dataContain.dataUser.DataSettingBoxGame;

        switch (settingType)
        {
            case SettingType.Music:
                dataSetting.SetMusicState(isBool);
                GameController.Instance.useProfile.OnMusic = dataSetting.IsMusicOn;
                break;
            case SettingType.Sound:
                dataSetting.SetSoundState(isBool);
                GameController.Instance.useProfile.OnSound = dataSetting.IsSoundOn;
                break;
            case SettingType.Vib:
                dataSetting.SetVibState(isBool);
                break;
        }
    }

    public void SetBtnOnState(bool param)
    {
        this.btnOn.gameObject.SetActive(param);
    }

    public void SetBtnOffState(bool param)
    {
        this.btnOff.gameObject.SetActive(!param);
    }


    public override void LoadComponent()
    {
        base.LoadComponent();
        this.btnOn = transform.Find("btn").Find("btnOn").GetComponent<Button>();
        this.btnOff = transform.Find("btn").Find("btnOff").GetComponent<Button>();
    }

}
