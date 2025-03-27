using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUserCtrl : MonoBehaviour
{
    [SerializeField] InputField inputField;
    private void OnEnable()
    {
        var dataProfile = GameController.Instance.dataContain.dataUser.DataUserProfileGame;
        this.inputField.text = dataProfile.UserName;
    }
    private void Start()
    {
        this.inputField.onSubmit.AddListener(OnClick);
    }

    void OnClick(string playerName)
    {
        if (string.IsNullOrEmpty(playerName) && string.IsNullOrWhiteSpace(playerName)) return;
        if (playerName.Length <= 3) return;

        var dataProfile = GameController.Instance.dataContain.dataUser.DataUserProfileGame;
        dataProfile.SetUserName(playerName);
    }
}
