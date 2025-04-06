using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Quoc_Dev/TowerData", order = 1)]
public class TowerData : SingletonScriptableObject<TowerData>
{
    [TableList]
    public List<Tower> towers = new List<Tower>();
    public Tower GetTower(int id)
    {
        foreach(var item in towers)
        {
            if(item.id == id) return item;
        }
        return null;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        for(int i = 0; i < towers.Count; i++)
        {
            towers[i].id = i+1;
            towers[i].showErr=false;
            foreach(var item in towers)
            {
                if(item.towerName == towers[i].towerName && item != towers[i])
                {
                    towers[i].showErr = true;
                }
            }
        }
    }
#endif
}


[System.Serializable]
public class Tower
{
    [VerticalGroup("Base")]
    public int id;
    [VerticalGroup("Base")]
#if UNITY_EDITOR
    [InfoBox(
        "Name has already existed.",
        InfoMessageType.Error,
        VisibleIf = "showErr"
        )]
#endif
    public string towerName;
    [InlineEditor]
    public BuildingContain prefab;
#if UNITY_EDITOR
    [HideInInspector]
    public bool showErr = false;
#endif
}
