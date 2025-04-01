using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelDesign : LocalSingleton<LevelDesign>
{
#if UNITY_EDITOR
    public GameObject text;
    [HideInInspector]
    public Transform level;
    [HideInInspector]
    public List<SetupTower> towerSetupList;
    [HideInInspector]
    public LayerMask _obstacle;

    [PropertyOrder(2)]
    [PropertySpace(10)]
    [TableList]
    public List<SpecialObjInfo> listObj = new List<SpecialObjInfo>();

    [PropertyOrder(0)]
    [Button(ButtonSizes.Gigantic), GUIColor(1, 0.2f, 0)]
    public void ClearLevel()
    {
        if(level != null)
        {
            DestroyImmediate(level.gameObject);
        }
        GameObject newObject = new GameObject("Level");
        newObject.transform.position = Vector3.zero;
        level = newObject.transform;
        towerSetupList.Clear();
    }

    public void OnValidate()
    {
        towerSetupList.Clear();
        Object[] _type = FindObjectsOfType(typeof(SetupTower));
        foreach(var item in _type)
        {
            towerSetupList.Add((SetupTower)item);
        }
    }

    [PropertySpace(10)]
    [PropertyOrder(3)]
    [LabelText("Level")]
    [OnValueChanged("changLevel")]
    public int levelNum;
    [HideInInspector]
    private bool alreadyHave = false;
    [HideInInspector]
    private bool notFound = false;
    [PropertyOrder(6)]
    [Button, GUIColor(1, 0.2f,0)]
    [InfoBox(
        "Level has alreadly existed. Load and Change it ot change Level Number",
        InfoMessageType.Error,
        VisibleIf = "alrealdyHave"
        )]
    [HorizontalGroup("SAVE")]
    public void SaveLevel()
    {
        notFound = false;
        string localPath = "Assets/Resources/Levels/Level_" + levelNum + ".prefab";
        List<GameObject> objWithScript = new List<GameObject>();
        SearchForScriptInChildren(this.level.transform, nameof(SetupTower), objWithScript);

        foreach(GameObject obj in objWithScript)
        {
            Collider _co = obj.GetComponent<Collider>();
            if(obj.transform.childCount != 0)
            {
                DestroyImmediate(obj.transform.GetChild(0).gameObject);
            }
            DestroyImmediate(_co);
        }

        List<GameObject> objectsWithScript2 = new List<GameObject>();
        SearchForScriptInChildren(this.level.transform, nameof(GoldPack), objectsWithScript2);

        // Now you have a list of GameObjects with the specified script
        foreach (GameObject obj in objectsWithScript2)
        {
            if (obj.transform.childCount != 0)
            {
                DestroyImmediate(obj.transform.GetChild(0).gameObject);
            }

            Collider _co = obj.GetComponent<Collider>();
            DestroyImmediate(_co);
        }

        if(AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
        {
            AssetDatabase.DeleteAsset(localPath);
        }

        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        bool prefabSuccess;
        PrefabUtility.SaveAsPrefabAsset(this.level.gameObject, localPath, out prefabSuccess);
        if (prefabSuccess == true)
        {
            Debug.Log("Prefab was saved successfully");
        }
        else
        {
            Debug.Log("Prefab failed to save" + prefabSuccess);
        }
        foreach (GameObject obj in objWithScript)
        {
            SetupTower _co = obj.GetComponent<SetupTower>();
            if (obj.transform.childCount == 0)
            {
                GameObject _go = Instantiate(text, obj.transform);
                _go.transform.localPosition = new Vector3(0, 1.3f, 0);

            }
            _co.OnValidate();
        }
        foreach (GameObject obj in objectsWithScript2)
        {
            if (obj.transform.childCount == 0)
            {
                GameObject _go = Instantiate(text, obj.transform);
                _go.transform.localPosition = new Vector3(0, 1.3f, 0);
            }
            GoldSpawn _co = obj.GetComponent<GoldSpawn>();
            _co.OnValidate();
        }
    }
    [PropertyOrder(6)]
    [Button, GUIColor(0.4f, 0.8f, 1)]
    [HorizontalGroup("SAVE")]
    [InfoBox(
    "Level not found. Change Level Number",
    InfoMessageType.Error,
    VisibleIf = "notFound"
    )]

    public void LoadLevel()
    {
        string localPath = "Assets/Resources/Levels/Level_" + levelNum + ".prefab";
        alreadyHave = false;
        if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
        {
            GameObject existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(localPath);

            if (this.level != null)
            {
                DestroyImmediate(this.level.gameObject);
            }

            GameObject _Lv = (GameObject)PrefabUtility.InstantiatePrefab(existingPrefab);
            PrefabUtility.UnpackPrefabInstance(_Lv, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
            this.level = _Lv.transform;
            this.level.position = Vector3.zero;
            _Lv.name = "Level";
            notFound = false;
            OnValidate();

            List<GameObject> objectsWithScript = new List<GameObject>();
            SearchForScriptInChildren(this.level.transform, nameof(SetupTower), objectsWithScript);

            List<GameObject> objectsWithScript2 = new List<GameObject>();
            SearchForScriptInChildren(this.level.transform, nameof(GoldSpawn), objectsWithScript2);

            foreach (GameObject obj in objectsWithScript)
            {
                SetupTower _co = obj.GetComponent<SetupTower>();
                GameObject _go = Instantiate(text, obj.transform);
                _go.transform.localPosition = new Vector3(0, 1.3f, 0);
                _co.OnValidate();
            }

            // Now you have a list of GameObjects with the specified script
            foreach (GameObject obj in objectsWithScript2)
            {
                GameObject _go = Instantiate(text, obj.transform);
                _go.transform.localPosition = new Vector3(0, 1.3f, 0);
                GoldSpawn _co = obj.GetComponent<GoldSpawn>();
                _co.OnValidate();
            }
            return;
        }
        notFound = true;
    }
    private void changeLevel()
    {
        alreadyHave = false;
        notFound = false;
    }
    [PropertySpace(10)]
    [PropertyOrder(7)]
    [Button, GUIColor(1, 0.2f, 0)]
    public void DeleteLevel()
    {
        string localPath = "Assets/Resources/Levels/Level_" + levelNum + ".prefab";
        alreadyHave = false;
        if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
        {
            AssetDatabase.DeleteAsset(localPath);
            return;
        }
    }
    public void Check()
    {
        List<GameObject> objectsWithScript = new List<GameObject>();
        SearchForScriptInChildren(this.level.transform, nameof(SetupTower), objectsWithScript);

        List<GameObject> objectsWithScript2 = new List<GameObject>();
        SearchForScriptInChildren(this.level.transform, nameof(GoldSpawn), objectsWithScript2);

        foreach (GameObject obj in objectsWithScript)
        {
            if (obj.transform.childCount == 0)
            {
                GameObject _go = Instantiate(text, obj.transform);
                SetupTower _co = obj.GetComponent<SetupTower>();
                _go.transform.localPosition = new Vector3(0, 1.3f, 0);
                _co.OnValidate();
            }
        }

        // Now you have a list of GameObjects with the specified script
        foreach (GameObject obj in objectsWithScript2)
        {
            if (obj.transform.childCount == 0)
            {
                GameObject _go = Instantiate(text, obj.transform);
                _go.transform.localPosition = new Vector3(0, 1.3f, 0);
                GoldSpawn _co = obj.GetComponent<GoldSpawn>();
                _co.OnValidate();
            }
        }
    }

    [PropertySpace(10)]
    [PropertyOrder(7)]
    [Button, GUIColor(0, 1, 0)]
    public void PlayLevel()
    {
        string localPath = "Assets/Resources/Levels/Level_" + levelNum + ".prefab";
        alreadyHave = false;
        if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
        {
            GameSave.PlayerLevel = levelNum;
            GameManager.Instance.Save();
            //EditorSceneManager.OpenScene("Assets/GameplayCore/Scenes/Gameplay.unity");
            EditorApplication.ExecuteMenuItem("Edit/Play");
            return;
        }
    }

    private void SearchForScriptInChildren(Transform transform, string v, List<GameObject> objWithScript)
    {
        throw new System.NotImplementedException();
    }
#endif

}

[System.Serializable]
public class SpecialObjInfo
{

}
