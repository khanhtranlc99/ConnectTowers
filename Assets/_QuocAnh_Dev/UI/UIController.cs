using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Button startGame;
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
}
