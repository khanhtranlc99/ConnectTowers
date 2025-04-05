using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using TMPro;
#endif
using UnityEngine;
using EventDispatcher;

[ExecuteInEditMode]
public class GoldSpawn : MonoBehaviour
{
    public int amount;
    public float maxScaleGold;
#if UNITY_EDITOR
    private TextMeshPro TextRoad;
    private void OnDrawGizmos()
    {
        if (gold == null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, .25f);
            if (LevelDesign.Instance != null)
            {
                foreach (var item in LevelDesign.Instance.towerSetupList)
                {

                    RaycastHit[] hits = new RaycastHit[5];
                    if (Physics.RaycastNonAlloc(transform.position, item.transform.position - transform.position, hits, Vector3.Distance(transform.position, item.transform.position), LevelDesign.Instance._obstacle) == 1)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(transform.position, item.transform.position);
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(transform.position, item.transform.position);
                    }
                }
            }
            if (TextRoad == null)
            {
                TextRoad = GetComponentInChildren<TextMeshPro>();
            }
            if (TextRoad != null)
            {
                TextRoad.text = "GOLD: " + amount.ToString();
            }
        }
        else
        {
            if (TextRoad == null)
            {
                TextRoad = GetComponentInChildren<TextMeshPro>();
            }
            if (TextRoad != null)
            {
                TextRoad.text = "";
            }
        }
    }

    public void Delete()
    {
        if (LevelDesign.Instance != null)
        {
            LevelDesign.Instance.OnValidate();
        }
    }

    public void OnValidate()
    {
        if (gold == null)
        {
            GoldSpawn[] myScript = Object.FindObjectsOfType<GoldSpawn>();
            foreach (var item in myScript)
            {
                if (item == this)
                {

                    
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    return;
                }
            }
        }
    }

#endif

    private GoldPack gold;
    [SerializeField] private GoldPack goldPrefab;
    public int Priority = 0;
    private System.Action<object> onClearMap;
    private void Awake()
    {
        onClearMap = _ => ResetTower();
        this.RegisterListener(EventID.CREATE_GAME, onClearMap);
        this.RegisterListener(EventID.CLEAR_MAP, onClearMap);
    }

    public void ResetTower()
    {
        if (gold == null)
        {
            gold = Instantiate(goldPrefab);
            GamePlayController.Instance.playerContain.buildingCtrl.towerList.Add(gold);
        }
        gold.maxHp = amount;
        gold.Hp = amount;
        gold.transform.position = transform.position;
        gold.gameObject.SetActive(true);
        gold.priority = Priority;
        gold.maxScaleGold = maxScaleGold;
        gold.currentScaleGold = maxScaleGold;
        gold.SetupGold();
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        Delete();
#endif
        this.RemoveListener(EventID.CREATE_GAME, onClearMap);
        this.RemoveListener(EventID.CLEAR_MAP, onClearMap);

        if(gold != null)
        {
            Destroy(gold.gameObject);
        }
    }
}