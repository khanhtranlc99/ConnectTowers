using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventDispatcher;
using Org.BouncyCastle.Math.Raw;

public class TutorialManager : BaseBox
{
    [SerializeField] private Image[] imgAvatar;
    [SerializeField] private GameObject[] textBox;
    [SerializeField] private Text[] textList;
    [SerializeField] private RectTransform hand;


    private Vector3 previousPos;
    private Vector3 currentPos;
    private Color color;
    private bool draw;
    private bool cut;
    private Vector3 littleUp = new Vector3(0, 0.1f, 0);
    private ArmyTower from;
    private BuildingContain hitTow;

    private bool end = false;
    private bool endHand = false;
    private int now = 0;


    private static TutorialManager _instance;
    public static TutorialManager Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<TutorialManager>(PathPrefabs.TUTORIAL_MANAGER));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }

    private void InitState()
    {
        Debug.LogError("Finish Tutorial");
    }

    public void Init()
    {
        color = ConfigData.Instance.colors[0];
        this.gameObject.SetActive(false);
        this.RegisterListener(EventID.CLEAR_MAP, delegate { Clear(); });
        this.RegisterListener(EventID.END_GAME, delegate { Clear(); });
    }
    public void Clear()
    {
        this.gameObject.SetActive(false);
        end = true;
        endHand = true;
    }
    public void SetupTutorial()
    {
        if (GamePlayController.Instance == null)
        {
            Init();
        }
        foreach (var item in imgAvatar)
        {
            item.enabled = false;
        }
        foreach (var item in textBox)
        {
            item.SetActive(false);
        }
        hand.gameObject.SetActive(false);
    }
    public void StartTutorial(int x)
    {
        SetupTutorial();
        this.gameObject.SetActive(true);
        switch (x)
        {
            case 1:
                StartCoroutine(Tutorial1());
                break;
            case 2:
                StartCoroutine(Tutorial2());
                break;
            case 3:
                StartCoroutine(Tutorial3());
                break;
            case 5:
                StartCoroutine(Tutorial5());
                break;
            case 7:
                StartCoroutine(Tutorial8());
                break;
            case 11:
                StartCoroutine(Tutorial11());
                break;
            case 14:
                StartCoroutine(Tutorial14());
                break;
        }
    }


    private IEnumerator Tutorial1()
    {
        imgAvatar[0].enabled = true;
        textBox[0].SetActive(true);
        StartCoroutine(ShowText(textList[0], "Lord. Draw a Line to capture this tower!"));
        UseProfile.Tut = 11;
        ArmyTower firstTow = null;
        BuildingContain tow = null;
        Vector3 pos1 = Vector3.zero;
        Vector3 pos2 = Vector3.zero;
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId == 0)
            {
                pos1 = item.transform.position;
                firstTow = (ArmyTower)item;
            }
            else
            {
                if (item is AttackTower)
                {
                    pos2 = item.transform.position;
                    tow = item;
                }
            }
        }
        hand.gameObject.SetActive(true);
        pos1 = Camera.main.WorldToScreenPoint(pos1);
        pos2 = Camera.main.WorldToScreenPoint(pos2);
        end = false;
        endHand = false;
        now++;
        HandMove(pos1, pos2, now);
        while (!end)
        {
            DrawPhase(1);
            yield return null;
        }
        UseProfile.Tut = 12;
        StartCoroutine(ShowText(textList[0], "Good. Now your troops will assault the enemy stronghold."));
        while (tow.teamId != 0)
        {
            yield return null;
        }
        UseProfile.Tut = 13;
        SetupTutorial();
        imgAvatar[1].enabled = true;
        textBox[1].SetActive(true);
        StartCoroutine(ShowText(textList[1], "Lord! Now try to cut the path by swiping!"));

        Vector3 vec1 = (pos1 + pos2) / 2 + new Vector3((pos2.y - pos1.y) / 4, 0, 0);
        Vector3 vec2 = (pos1 + pos2) / 2 - new Vector3((pos2.y - pos1.y) / 4, 0, 0);
        end = false;
        endHand = false;
        hand.gameObject.SetActive(true);
        now++;
        HandMove(vec1, vec2, now);
        while (!end)
        {
            DrawPhase(2);
            if (firstTow.gate.Count == 0)
            {
                endHand = true;
                end = true;
            }

            yield return null;
        }
        UseProfile.Tut = 14;
        GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.positionCount = 1;
        hitTow = null;
        cut = false;
        draw = false;
        SetupTutorial();
        float t = 0;
        while (t < 1.5f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        UseProfile.Tut = 15;
        imgAvatar[3].enabled = true;
        textBox[0].SetActive(true);

        StartCoroutine(ShowText(textList[0], "Finally! Conquer every enemy stronghold to claim the win!!"));
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item is ArmyTower)
            {
                pos2 = item.transform.position;
                tow = item;
            }
        }
        pos2 = Camera.main.WorldToScreenPoint(pos2);
        end = false;
        endHand = false;
        hand.gameObject.SetActive(true);
        hand.transform.position = pos2;
        now++;
        HandMove(pos1, pos2, now);
        while (!end)
        {
            DrawPhase(3);
            if (tow.teamId == 0)
            {
                end = true;
            }
            yield return null;
        }
        UseProfile.Tut = 16;
        GamePlayController.Instance.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }



    private IEnumerator Tutorial2() // tutorial update tow
    {
        UseProfile.Tut = 21;
        SetupTutorial();
        imgAvatar[0].enabled = true;
        textBox[0].SetActive(true);
        StartCoroutine(ShowText(textList[0], "Lord. Please send reinforce to this tower!"));
        ArmyTower firstTow = null;
        BuildingContain tow = null;
        Vector3 pos1 = Vector3.zero;
        Vector3 pos2 = Vector3.zero;
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId == 0 && item.priority == 0)
            {
                pos1 = item.transform.position;
                firstTow = (ArmyTower)item;
            }
            else if (item.teamId == 0 && item.priority == 1)
            {
                pos2 = item.transform.position;
                tow = item;
            }
        }
        hand.gameObject.SetActive(true);
        pos1 = Camera.main.WorldToScreenPoint(pos1);
        pos2 = Camera.main.WorldToScreenPoint(pos2);
        end = false;
        endHand = false;
        now++;
        HandMove(pos1, pos2, now);
        while (!end)
        {
            foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
            {
                item.Hp = 5;
            }
            DrawPhase(11);
            yield return null;
        }
        UseProfile.Tut = 22;
        SetupTutorial();
        imgAvatar[1].enabled = true;
        textBox[1].SetActive(true);
        StartCoroutine(ShowText(textList[1], "With more solider, Tower will be upgraded and gain more path!"));
        end = false;
        endHand = false;
        while (!end)
        {
            if (tow.Hp >= 10)
            {
                end = true;
                endHand = true;
            }
            yield return null;
        }
        UseProfile.Tut = 23;
        float t = 0;
        end = false;
        endHand = false;
        while (!end)
        {
            if (t >= 2)
            {
                end = true;
                endHand = true;
            }
            t += Time.deltaTime;
            yield return null;
        }
        UseProfile.Tut = 24;
        SetupTutorial();
        imgAvatar[2].enabled = true;
        textBox[0].SetActive(true);
        StartCoroutine(ShowText(textList[0], "Conquer all enemies to win. LORD!"));
        t = 0;
        end = false;
        endHand = false;
        while (!end)
        {
            DrawPhase();
            if (t > 10)
            {
                SetupTutorial();
                end = true;
                endHand = true;
            }
            t += Time.deltaTime;
            yield return null;
        }
        UseProfile.Tut = 25;
        GamePlayController.Instance.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private IEnumerator Tutorial3() // tutorial max tower
    {
        UseProfile.Tut = 31;
        SetupTutorial();
        ArmyTower firstTow = null;
        Vector3 pos1 = Vector3.zero;
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId == 0 && item.priority == 1)
            {
                firstTow = (ArmyTower)item;
                pos1 = firstTow.transform.position;
            }
        }
        pos1 = Camera.main.WorldToScreenPoint(pos1);
        end = false;
        endHand = false;
        now++;
        hand.gameObject.SetActive(true);
        hand.transform.position = pos1;
        HandClick(pos1, now);
        imgAvatar[0].enabled = true;
        textBox[0].SetActive(true);
        StartCoroutine(ShowText(textList[0], "Lord. Let's send our troops to this tower!"));
        while (!end)
        {
            DrawPhase(21);
            if (firstTow.Hp >= 65)
            {
                end = true;
                endHand = true;
            }
            yield return null;
        }
        UseProfile.Tut = 32;
        hand.gameObject.SetActive(false);
        SetupTutorial();
        imgAvatar[5].enabled = true;
        textBox[2].SetActive(true);
        StartCoroutine(ShowText(textList[2], "Lord. Maximum tower will allow troops to pass through it, creating overwhelming army!"));

        float t = 0;
        end = false;
        endHand = false;
        while (!end)
        {
            DrawPhase();
            if (t > 10)
            {
                SetupTutorial();
                end = true;
                endHand = true;
            }
            t += Time.deltaTime;
            yield return null;
        }
        UseProfile.Tut = 33;
        GamePlayController.Instance.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private IEnumerator Tutorial5()
    {
        UseProfile.Tut = 51;
        SetupTutorial();
        Vector3 pos1 = Vector3.zero;
        BuildingContain tow = null;
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId != 0 && item.Hp > 55)
            {
                tow = item;
                break;
            }
        }
        hand.gameObject.SetActive(true);
        hand.transform.position = pos1;
        imgAvatar[2].enabled = true;
        textBox[1].SetActive(true);
        StartCoroutine(ShowText(textList[1], "Lord. Red army is too strong! Please use your special skill!"));
        end = false;
        endHand = false;
        now++;
        HandClick(pos1, now);
        while (!end)
        {
            DrawPhase();
            if (tow.teamId == 0)
            {
                end = true;
            }
            yield return null;
        }
        UseProfile.Tut = 52;
        float t = 0;
        end = false;
        while (!end)
        {
            DrawPhase();
            if (t > 3)
            {
                SetupTutorial();
                end = true;
                endHand = true;
            }
            t += Time.deltaTime;
            yield return null;
        }
        UseProfile.Tut = 53;
        GamePlayController.Instance.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private IEnumerator Tutorial8()
    {
        UseProfile.Tut = 81;
        SetupTutorial();
        BuildingContain tow = null;
        Vector3 pos1 = Vector3.zero;
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.priority == 10)
            {
                tow = item;
                pos1 = tow.transform.position;
                break;
            }
        }
        pos1 = Camera.main.WorldToScreenPoint(pos1);

        hand.gameObject.SetActive(true);
        hand.transform.position = pos1;
        imgAvatar[0].enabled = true;
        textBox[0].SetActive(true);
        StartCoroutine(ShowText(textList[0], "Lord. Let's capture this new building!"));
        end = false;
        endHand = false;
        now++;
        HandClick(pos1, now);
        while (!end)
        {
            DrawPhase(81);
            if (tow.teamId == 0)
            {
                end = true;
                endHand = true;
            }
            yield return null;
        }
        UseProfile.Tut = 82;
        hand.gameObject.SetActive(false);
        SetupTutorial();
        imgAvatar[5].enabled = true;
        textBox[2].SetActive(true);
        StartCoroutine(ShowText(textList[2], "Exelent. Heavy troop is more powerful than normal troop! Let's deploy them now!"));

        float t = 0;
        end = false;
        endHand = false;
        while (!end)
        {
            DrawPhase();
            if (t > 10)
            {
                SetupTutorial();
                end = true;
                endHand = true;
            }
            t += Time.deltaTime;
            yield return null;
        }
        UseProfile.Tut = 83;
        GamePlayController.Instance.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

    }
    private IEnumerator Tutorial11()
    {
        UseProfile.Tut = 111;
        SetupTutorial();
        Vector3 pos1 = Vector3.zero;
        Vector3 pos2 = Vector3.zero;
        Vector3 pos3 = Vector3.zero;
        int i = 0;
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item is GoldPack)
            {
                i++;
                if (i == 1)
                {
                    pos1 = item.transform.position;
                }
                else if (i == 2)
                {
                    pos2 = item.transform.position;
                }
                else if (i == 3)
                {
                    pos3 = item.transform.position;
                }
            }
        }
        pos1 = Camera.main.WorldToScreenPoint(pos1);
        pos2 = Camera.main.WorldToScreenPoint(pos2);
        pos3 = Camera.main.WorldToScreenPoint(pos3);
        hand.gameObject.SetActive(true);
        hand.transform.position = pos1;
        now++;
        HandClickThree(pos1, pos2, pos3, now);
        imgAvatar[5].enabled = true;
        textBox[2].SetActive(true);
        StartCoroutine(ShowText(textList[2], "Lord. Those gold coins are truly valuable. Try sending troops to collect thems!"));
        while (!end)
        {
            DrawPhase(111);
            yield return null;
        }
        UseProfile.Tut = 112;
        float t = 0;
        end = false;
        while (!end)
        {
            DrawPhase();
            if (t > 10)
            {
                SetupTutorial();
                end = true;
                endHand = true;
            }
            t += Time.deltaTime;
            yield return null;
        }
        UseProfile.Tut = 153;
        GamePlayController.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    private IEnumerator Tutorial14() // fence
    {
        SetupTutorial();

        UseProfile.Tut = 191;
        imgAvatar[3].enabled = true;
        textBox[1].SetActive(true);
        StartCoroutine(ShowText(textList[1], "Lord. Please careful with there trap. Our troop will automatic destroy them!"));


        while (!end)
        {
            DrawPhase(3);
            yield return null;
        }
        UseProfile.Tut = 192;
        float t = 0;
        end = false;
        while (!end)
        {
            DrawPhase();
            if (t > 10)
            {
                SetupTutorial();
                end = true;
                endHand = true;
            }
            t += Time.deltaTime;
            yield return null;
        }
        UseProfile.Tut = 193;

        GamePlayController.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    private IEnumerator ShowText(Text text, string v)
    {
        string final = "";
        char[] charArray = v.ToCharArray();
        for (int i = 0; i < charArray.Length; i++)
        {
            final += charArray[i];
            text.text = final;
            yield return Helper.GetWait(0.02f);
        }
    }
    private void HandMove(Vector3 _from, Vector3 _to, int _now)
    {
        if (now != _now)
        {
            return;
        }
        if (endHand)
        {
            hand.gameObject.SetActive(false);
            return;
        }
        hand.transform.DOMove(_to, 0.8f).SetEase(Ease.OutQuad).From(_from).OnComplete(() => HandMove(_from, _to, _now));
    }

    public void HandClick(Vector3 _point, int _now)
    {
        if (now != _now)
        {
            return;
        }
        if (endHand)
        {
            hand.gameObject.SetActive(false);
            return;
        }
        hand.transform.DOScale(.8f, 1f).From(1.2f).OnComplete(() => HandClick(_point, _now));
    }
    public void HandClickThree(Vector3 _p1, Vector3 _p2, Vector3 _p3, int _now)
    {
        if (now != _now)
        {
            return;
        }
        if (endHand)
        {
            hand.gameObject.SetActive(false);
            return;
        }
        hand.transform.position = _p1;
        hand.transform.DOScale(.8f, 1f).From(1.2f).OnComplete(() => HandClickThree(_p2, _p3, _p1, _now));
    }

    

    private void DrawPhase(int tutCount = -1)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // 
            if (Physics.Raycast(ray, out hit, 100, GamePlayController.Instance.playerContain.inputCtrl.lineContain._mouseDown))
            {
                if (hit.transform == GamePlayController.Instance.playerContain.inputCtrl.lineContain.Plane)
                {
                    previousPos = hit.point;
                    currentPos = hit.point;
                    //bat.MouseFollower.position = hit.point + littleUp;
                    //bat.MouseFollower.gameObject.SetActive(true);
                    cut = true;
                }
                else
                {
                    if (tutCount == 2)
                    {
                        return;
                    }
                    if (hit.transform.TryGetComponent(out from))
                    {
                        if (from.teamId == 0 && from.gate.Count < from.level + 1)
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.positionCount = 2;
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.SetPosition(0, from.transform.position + littleUp);
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.SetPosition(1, hit.point + littleUp);
                            draw = true;
                        }
                    }
                }
            }
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (draw)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, GamePlayController.Instance.playerContain.inputCtrl.lineContain._mouseDown))
                {
                    // move line with finger
                    if (hit.transform == GamePlayController.Instance.playerContain.inputCtrl.lineContain.Plane)
                    {
                        hitTow = null;
                        GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.SetPosition(1, hit.point + littleUp);

                        // check if obstacle
                        if (Physics.Raycast(from.transform.position, hit.point - from.transform.position, Vector3.Distance(hit.point, from.transform.position), GamePlayController.Instance.playerContain.inputCtrl.lineContain._obstacle))
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.material.color = Color.gray;
                        }
                        else
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.material.color = color;
                        }
                    }
                    else
                    {
                        if (from.transform != hit.transform && hit.transform.TryGetComponent(out BuildingContain tow))
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.SetPosition(1, tow.transform.position + littleUp);

                            if (from.listCanGo.Contains(tow.id))
                            {
                                hitTow = tow;
                                GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.material.color = color;
                            }
                            else
                            {
                                hitTow = null;
                                GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.material.color = Color.gray;
                            }
                        }
                    }
                }
            }
            if (cut) // draw cut
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, GamePlayController.Instance.playerContain.inputCtrl.lineContain._plane))
                {
                    currentPos = hit.point;

                    CheckDrawCut();
                    //bat.MouseFollower.position = Vector3.Lerp(bat.MouseFollower.position, hit.point + littleUp, 9 * Time.deltaTime);
                    previousPos = currentPos;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (hitTow != null)
            {
                if (tutCount == 1) // connect with attack tower
                {
                    if (hitTow is AttackTower)
                    {
                        end = true;
                        endHand = true;
                        GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(from, hitTow);
                    }
                }
                else if (tutCount == 3) // connect with enemy tower
                {
                    if (hitTow.teamId != 0)
                    {
                        end = true;
                        endHand = true;
                    }
                    GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(from, hitTow);
                }
                else if (tutCount == 11) // connect with my priority tow
                {
                    if (hitTow.teamId == 0 && hitTow.priority == 1)
                    {
                        end = true;
                        endHand = true;
                        GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(from, hitTow);
                    }
                }
                else if (tutCount == 21) // connect with my priority tow
                {
                    if (hitTow.teamId == 0 && hitTow.priority == 1)
                    {
                        endHand = true;
                    }
                    GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(from, hitTow);
                }
                else if (tutCount == 51) // connect with my priority tow
                {
                    if (hitTow.teamId != 0 && hitTow.priority == 10)
                    {
                        endHand = true;
                    }
                    GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(from, hitTow);
                }
                else if (tutCount == 111) // connect with my gold pack
                {
                    if (hitTow is GoldPack)
                    {
                        endHand = true;
                        end = true;
                    }
                    GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(from, hitTow);
                }
                else
                {
                    GamePlayController.Instance.playerContain.inputCtrl.lineContain.LinkTower(from, hitTow);
                }
            }
            GamePlayController.Instance.playerContain.inputCtrl.lineContain.line.positionCount = 1;
            hitTow = null;
            cut = false;
            draw = false;
            //bat.MouseFollower.gameObject.SetActive(false);
        }
    }

    private void CheckDrawCut()
    {
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId == 0)
            {
                if (item is ArmyTower tow)
                {
                    for (int i = tow.road.Count - 1; i >= 0; i--)
                    {
                        Vector3[] linePositions = new Vector3[2];
                        linePositions[0] = tow.transform.position;
                        linePositions[1] = GamePlayController.Instance.playerContain.buildingCtrl.towerList[tow.gate[i]].transform.position;

                        if (Helper.CheckIntersection(previousPos, currentPos, linePositions))
                        {
                            GamePlayController.Instance.playerContain.inputCtrl.lineContain.CutRoad(tow, GamePlayController.Instance.playerContain.buildingCtrl.towerList[tow.gate[i]]);
                        }
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.CLEAR_MAP, delegate { Clear(); });
        this.RemoveListener(EventID.END_GAME, delegate { Clear(); });
    }
}