using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BtnPlayOne : BtnUpgradeBase
{
    public override void OnClick()
    {
        var name = SceneName.GAME_PLAY;
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }

}
