using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeBase/MammothUpgrade")]
public class MammothUpgrade : UpgradeBase
{
    public List<MammothUpgradeData> lsMammothUpgradeDatas;

    public MammothUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsMammothUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class MammothUpgradeData
{
    public int idStar;
    public PropertiesMammothUpgradeData propertiesMammothUpgradeData;
}

[System.Serializable]
public class PropertiesMammothUpgradeData
{
    public int bonus_Move_Speed_0;
    public int bonus_Move_Speed_1;
    public int bonus_Speed_To_All;
    public int bonus_Gold_To_All_0;
    public int bonus_Gold_To_All_1;
}
