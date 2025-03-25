using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using TMPro;
#endif
using UnityEngine;
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
    private SphereCollider cc;

    public void OnValidate()
    {
        if (gold == null)
        {
            GoldSpawn[] myScript = Object.FindObjectsOfType<GoldSpawn>();
            foreach (var item in myScript)
            {
                if (item == this)
                {

                    if (TryGetComponent(out SphereCollider cap))
                    {
                        cc = cap;
                    }
                    else
                    {
                        cc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
                    }

                    cc.center = new Vector3(0, 0f, 0);
                    cc.radius = 0.25f;
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
    //private void Awake()
    //{
    //    Messenger.AddListener(GameConstant.Event.CREATE_GAME, ResetTower);
    //    Messenger.AddListener(GameConstant.Event.CLEAR_MAP, ResetTower);
    //}

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
}