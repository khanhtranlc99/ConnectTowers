using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "USER/DataSettingBoxGame")]

public class DataSettingBoxGame : ScriptableObject
{
    [SerializeField] bool isMusicOn = true;
    public bool IsMusicOn => isMusicOn;
    [SerializeField] bool isSoundOn = true;
    public bool IsSoundOn => isSoundOn;
    [SerializeField] bool isVibrationOn = true;
    public bool IsVibrationOn => isVibrationOn;


    public void SetMusicState(bool param)
    {
        this.isMusicOn = param;
    }
    public void SetSoundState(bool param)
    {
        this.isSoundOn = param;
    }
    public void SetVibState(bool param)
    {
        this.isVibrationOn = param;
    }



}
