using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "USER/DataUserProfileGame")]

public class DataUserProfileGame : ScriptableObject
{
    [SerializeField] string userName;
    public string UserName => userName;
    public void SetUserName(string name)
    {
        this.userName = name;
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.UPDATE_NAME_PROFILE, userName);
    }
}
