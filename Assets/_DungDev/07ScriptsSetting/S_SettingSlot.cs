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

    private void Start()
    {
        this.btnOn.onClick.AddListener(HandleButtonOn);
        this.btnOff.onClick.AddListener(HandleButtonOff);
    }
    void HandleButtonOn()
    {
        this.SetBtnState(false);

    }
    void HandleButtonOff()
    {
        this.SetBtnState(true);
    }

    void HandleSettingData(bool isState)
    {
        GameController.Instance.musicManager.PlayClickSound();
        switch (settingType)
        {
            case SettingType.Music:
                GameController.Instance.useProfile.OnMusic = isState;
                break;
            case SettingType.Sound:
                GameController.Instance.useProfile.OnSound = isState;
                break;
            case SettingType.Vib:
                GameController.Instance.useProfile.OnVibration = isState;
                break;
        }
    }

    public void SetBtnState(bool state)
    {
        this.btnOff.gameObject.SetActive(!state);
        this.btnOn.gameObject.SetActive(state);
        this.HandleSettingData(state);
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.btnOn = transform.Find("btn").Find("btnOn").GetComponent<Button>();
        this.btnOff = transform.Find("btn").Find("btnOff").GetComponent<Button>();
    }

}
