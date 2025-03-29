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
        var dataProfile = GameController.Instance.dataContain.dataUser.DataUserProfileGame;
        this.inputField.text = dataProfile.UserName;
    }
    private void Start()
    {
        this.inputField.onSubmit.AddListener(OnClick);
    }

    void OnClick(string playerName)
    {
        var dataProfile = GameController.Instance.dataContain.dataUser.DataUserProfileGame;
        if (string.IsNullOrEmpty(playerName) && string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "Player" + Random.Range(1000, 9999);
        }
        dataProfile.SetUserName(playerName);
        this.inputField.text = dataProfile.UserName;
    }
}
