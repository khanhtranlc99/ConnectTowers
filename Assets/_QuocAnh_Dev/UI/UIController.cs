using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : Singleton<UIController>
{
    public Button startGame;

    [Header("Play Game Variable")]
    public WinBox_QA winPopupPrefab;
    private void Start()
    {
        startGame.onClick.AddListener(() =>
        {
            PlayCampainGame();
        });
    }
    private void PlayCampainGame()
    {
        startGame.gameObject.SetActive(false);
        GameManager.Instance.StartGame();

    }
    public void NextGameLevel()
    {
        ActiveMainUI();
        GameManager.Instance.CreateGame();
    }

    private void ActiveMainUI()
    {
        throw new NotImplementedException();
    }
}
