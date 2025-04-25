using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EventDispatcher;

public class Fence : MonoBehaviour
{
    public int dmg;
    public int hp;
    [SerializeField] private int _hp;
    [SerializeField] private bool showText;
    private TextMeshPro textRoad;

    [SerializeField] private TypeFence fenceType;

    #region multiple
    [ShowIf("fenceType", Value = TypeFence.multiple)]
    [SerializeField] private GameObject fenceParent;
    [ShowIf("fenceType", Value = TypeFence.multiple)]
    [SerializeField] private List<Transform> fenceList = new List<Transform>();
    [ShowIf("fenceType", Value = TypeFence.multiple)]
    [SerializeField] private float forceDown = 20;
    [ShowIf("fenceType", Value = TypeFence.multiple)]
    [SerializeField] private List<GameObject> objFenceList = new List<GameObject>();
    private Vector3 posTarget;
    private int cntObj;
    #endregion
    private System.Action<object> onReset;

    private void OnTriggerEnter(Collider other)
    {
        if (GamePlayController.Instance.playerContain.unitCtrl.componentDict.TryGetValue(other, out CharacterBase _unit))
        {
            if (_unit.isDead) return;
            // sound eff
            _unit.Hp -= dmg;
            if (fenceType == TypeFence.multiple)
            {
                for (int i = 0; i < _unit.dame; i++)
                {
                    if (_hp <= 0)
                    {
                        continue;
                    }
                    _hp -= 1;
                    GameObject g = CreateObjFencePool(fenceList[1].gameObject, transform);
                    g.transform.position = fenceList[_hp].transform.position;
                    g.transform.rotation = fenceList[_hp].transform.rotation;
                    g.transform.localScale = fenceList[0].transform.localScale;

                    float rPosY = Random.Range(0.6f, 0.1f), rPosZ = Random.Range(0.3f, 0.5f);

                    g.SetActive(true);
                    Rigidbody r = g.AddComponent<Rigidbody>();
                    r.AddForce((fenceList[_hp]).transform.position + new Vector3(0, rPosY, rPosZ) * forceDown);
                    fenceList[_hp].gameObject.SetActive(false);
                }
            }
            else
            {
                _hp -= 1;
            }
            if (showText)
            {
                textRoad.text = hp.ToString();
            }
            if (_hp <= 0)
            {
                _hp = -1;
                if (fenceType == TypeFence.multiple)
                {
                    foreach (var item in objFenceList)
                    {
                        Destroy(item);
                    }
                    objFenceList.Clear();
                }
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTower()
    {
        _hp = hp;
        gameObject.SetActive(true);
        if (fenceList.Count <= 0 && fenceType != TypeFence.multiple)
        {
            return;
        }
        for (int i = _hp; i < fenceList.Count; i++)
        {
            fenceList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < _hp; i++)
        {
            fenceList[i].gameObject.SetActive(true);
        }
    }

    private GameObject CreateObjFencePool(GameObject prefab, Transform parent = null)
    {
        foreach (var item in objFenceList)
        {
            if (item.activeSelf)
            {
                continue;
            }
            return item;
        }
        GameObject g = Instantiate(prefab, parent);
        objFenceList.Add(g);
        return g;
    }
    private void Awake()
    {
        onReset = _ => ResetTower();
        if (showText)
        {
            textRoad = GetComponent<TextMeshPro>();
            textRoad.text = hp.ToString();
        }
        if (fenceType == TypeFence.multiple)
        {
            fenceList = new List<Transform>(fenceParent.GetComponentsInChildren<Transform>());
        }
        ResetTower();
        //this.RegisterListener(EventID.CREATE_GAME, onReset);
        //this.RegisterListener(EventID.CLEAR_MAP, onReset);
    }
    private void OnDestroy()
    {
        //this.RemoveListener(EventID.CREATE_GAME, onReset);
        //this.RemoveListener(EventID.CLEAR_MAP, onReset);
    }
}

public enum TypeFence
{
    single,
    multiple
}
