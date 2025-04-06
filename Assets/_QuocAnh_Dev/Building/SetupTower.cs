using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using TMPro;
#endif
using UnityEngine;
using EventDispatcher;




public class SetupTower : MonoBehaviour
{
    public int id;

#if UNITY_EDITOR
    [ValueDropdown("GetListTow")]
    [OnValueChanged("GetTowId")]
    public string towerName;
    [ReadOnly]
    [InlineEditor]
    public BuildingContain towerCtrl;
    private TextMeshPro textRoad;
    public List<string> GetListTow()
    {
        List<string> list = new List<string>();
        foreach (var item in TowerData.Instance.towers)
        {
            list.Add(item.towerName);
        }
        return list;
    }
    public void GetTowId()
    {
        foreach (var item in TowerData.Instance.towers)
        {
            if (item.towerName == towerName)
            {
                id = item.id;
                towerCtrl = item.prefab;
                return;
            }
        }
        towerCtrl = null;
        id = 0;
    }
    [UnityEditor.DrawGizmo(UnityEditor.GizmoType.Selected | UnityEditor.GizmoType.Active)]
    private void OnDrawGizmos()
    {
        if (tow == null)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, .63f);
            if (LevelDesign.Instance != null)
            {
                foreach (var item in LevelDesign.Instance.towerSetupList)
                {
                    if (item != this)
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
            }
        }
    }


#endif
#if UNITY_EDITOR
    [ProgressBar(-1, 4, ColorGetter = "GetColor", Segmented = true, Height = 20)]
    [OnValueChanged("OnChangeTeam")]
#endif
    public int teamId;
    [SerializeField] private BuildingContain tow;
#if UNITY_EDITOR
    [HideInInspector]
    public Color color;
    public Color GetColor() { return color; }
    public void OnChangeTeam()
    {
        if (this.teamId == -1)
        {
            color = Color.gray;
        }
        else
        {
            color = ConfigData.Instance.colors[this.teamId];
        }
    }
    private CapsuleCollider col;
    public void OnValidate()
    {
        if (tow == null)
        {
            SetupTower[] myScript = Object.FindObjectsOfType<SetupTower>();
            foreach (var item in myScript)
            {
                if (item == this)
                {
                    OnChangeTeam();
                    if (TryGetComponent(out CapsuleCollider cap))
                    {
                        col = cap;
                    }
                    else
                    {
                        col = gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
                    }
                    col.center = new Vector3(0, 0.5f, 0);
                    col.radius = 0.63f;
                    col.height = 2;
                    this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                    break;
                }
            }
            if (textRoad == null)
            {
                textRoad = GetComponentInChildren<TextMeshPro>();
            }
            if (textRoad != null)
            {
                textRoad.text = towerName + " " + this.hp.ToString();
            }
        }
        else
        {
            if (textRoad == null)
            {
                textRoad = GetComponentInChildren<TextMeshPro>();
            }
            if (textRoad != null)
            {
                textRoad.text = towerName + " " + this.hp.ToString();
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


#endif


    [ProgressBar(5, 65)]
    public int hp = 5;

    public int priority = 0;

    private System.Action<object> onCreateGame;
    private System.Action<object> onStartGame;
    private System.Action<object> onClearMap;

    private void Awake()
    {
        onCreateGame = _ => ResetTower();
        onStartGame = _ => StartGame();
        onClearMap = _ => ResetTower();

        this.RegisterListener(EventID.CREATE_GAME, onCreateGame);
        this.RegisterListener(EventID.START_GAME, onStartGame);
        this.RegisterListener(EventID.CLEAR_MAP, onClearMap);

    }

    public void StartGame()
    {
        tow.enabled = true;
    }
    public void CreateTower()
    {


        Tower tower = TowerData.Instance.GetTower(id);


        tow = Instantiate(tower.prefab);


        tow.teamId = this.teamId;
        tow.Hp = this.hp;
        tow.priority = this.priority;
        tow.transform.position = this.transform.position;
        tow.enabled = false;

    }
    public void ResetTower()
    {

        if (tow == null)
        {
            CreateTower();
            GamePlayController.Instance.playerContain.buildingCtrl.towerList.Add(tow);
        }
        tow.teamId = this.teamId;
        tow.Hp = this.hp;
        tow.enabled = false;
    }
    private void OnDestroy()
    {
#if UNITY_EDITOR
        Delete();
#endif
        if (this != null)
        {
            this.RemoveListener(EventID.CREATE_GAME, onCreateGame);
            this.RemoveListener(EventID.START_GAME, onStartGame);
            this.RemoveListener(EventID.CLEAR_MAP, onClearMap);
        }
        if (tow != null)
        {
            Destroy(tow.gameObject);
        }
    }
}
