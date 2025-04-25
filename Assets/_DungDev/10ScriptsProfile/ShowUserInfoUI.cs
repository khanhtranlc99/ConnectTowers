using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowUserInfoUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nameUser;
    private void OnEnable()
    {
        this.SetUpName(UseProfile.ProfileNameUser);
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
