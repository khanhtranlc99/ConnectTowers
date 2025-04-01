using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : Singleton<UIController>
{
    public Button startGame;
    public bool isStartGameClick=false;

    [Header("Play Game Variable")]
    public WinBox_QA winPopupPrefab;
    public BattleUiManager battleUiManager;
    private void Start()
    {
        startGame.onClick.AddListener(() =>
        {
            PlayCampainGame();
        });
    }
    private void PlayCampainGame()
    {
        isStartGameClick = true;
        startGame.gameObject.SetActive(false);
        battleUiManager.enabled = true;
        GameManager.Instance.StartGame();

    }

    private void ActiveMainUI()
    {
        throw new NotImplementedException();
    }
}
