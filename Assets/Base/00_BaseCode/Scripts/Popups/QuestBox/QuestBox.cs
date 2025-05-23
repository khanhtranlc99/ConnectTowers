using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBox : BaseBox
{
    private static QuestBox instance;
    public static QuestBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<QuestBox>(PathPrefabs.QUEST_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    private void Init()
    {
        btnClose.onClick.AddListener(Close);
    }
    private void InitState()
    {
        questBoxCtrl.Init();
    }

    public Q_QuestBoxCtrl questBoxCtrl;


}
