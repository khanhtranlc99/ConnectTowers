using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
using EventDispatcher;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif


public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameController");
                    _instance = obj.AddComponent<GameController>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }
       

    public MoneyEffectController moneyEffectController;
    public UseProfile useProfile;
    public DataContain dataContain;
    public MusicManagerGameBase musicManager;
    public AdmobAds admobAds;

    public AnalyticsController AnalyticsController;
    public IapController iapController;
    public HeartGame heartGame;
    public SceneType currentScene;
 
    public StartLoading startLoading;

    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        Init();

        DontDestroyOnLoad(this);

        //GameController.Instance.useProfile.IsRemoveAds = true;
        //Application.targetFrameRate = 120;


#if UNITY_IOS

    if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == 
    ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
    {

        ATTrackingStatusBinding.RequestAuthorizationTracking();

    }

#endif

    }

    private void Start()
    {
           musicManager.PlayBGMusic();
    }

    public void Init()
    {
        Application.targetFrameRate = 120;
        SetUp();
    }

    public void SetUp()
    {
        admobAds.Init();
        musicManager.Init();
        iapController.Init();
        dataContain.InitData();
        MMVibrationManager.SetHapticsActive(useProfile.OnVibration);
        startLoading.Init();
        //heartGame.Init();

    }

    public void LoadScene(string sceneName)
    {
        Initiate.Fade(sceneName.ToString(), Color.black, 2f);
    }


}
public enum SceneType
{
    StartLoading = 0,
    MainHome = 1,
    GamePlay = 2
}