using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileUserInfoUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nameUser;
    private void OnEnable()
    {
        var dataProfile = GameController.Instance.dataContain.dataUser.DataUserProfileGame;
        this.SetUpName(dataProfile.UserName);
        this.RegisterListener(EventID.UPDATE_NAME_PROFILE, SetUpName);
    }
    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_NAME_PROFILE, SetUpName);
    }

    public void SetUpName(object obj)
    {
        if (!(obj is string name)) return;
        this.nameUser.text = name;
    }

}
