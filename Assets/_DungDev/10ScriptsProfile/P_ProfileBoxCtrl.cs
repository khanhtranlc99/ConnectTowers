using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class P_ProfileBoxCtrl : MonoBehaviour
{
    [SerializeField] InputField inputField;
    private void OnEnable()
    {
        this.inputField.text = UseProfile.ProfileNameUser;
    }
    private void Start()
    {
        this.inputField.onSubmit.AddListener(OnClick);
    }

    void OnClick(string playerName)
    {
        if (string.IsNullOrEmpty(playerName) && string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "Player" + Random.Range(1000, 9999).ToString();
        }
        UseProfile.ProfileNameUser = playerName;
        this.inputField.text = playerName;
        this.PostEvent(EventID.UPDATE_NAME_PROFILE,playerName);
    }
}
