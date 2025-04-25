using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineContain : MonoBehaviour
{
    public LineRenderer line;
    public Transform Plane;
    public Stack<LineRenderer> linesList = new Stack<LineRenderer>();

    public LayerMask _mouseDown;
    public LayerMask _obstacle;
    public LayerMask _plane;

    protected Vector3 previousPos;
    protected Vector3 currentPos;
    [SerializeField] protected bool draw;
    [SerializeField] protected bool cut;

    [SerializeField] protected Color _color;
    public Material material;

    protected Vector3 littleUp = new Vector3(0, 0.1f, 0);
    private ArmyTower from;
    private BuildingContain hitTow;

    protected void Awake()
    {
        draw = false;
        _color = ConfigData.Instance.colors[0];
    }

    public void DrawPath()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100, _mouseDown))
            {
                if(hit.transform == Plane)
                {
                    previousPos = hit.point;
                    currentPos = hit.point;
                    cut=true;
                }
                else
                {
                    if(hit.transform.TryGetComponent(out from))
                    {
                        if (from.teamId==0 && from.gate.Count < from.level + 1)
                        {
                            line.positionCount = 2;
                            line.SetPosition(0, from.transform.position + littleUp);
                            line.SetPosition(1, hit.point + littleUp);
                            draw= true;
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
                if(Physics.Raycast(ray, out hit, 100, _mouseDown))
                {
                    if (hit.transform == Plane)
                    {
                        
                        hitTow = null;
                        
                        line.SetPosition(1, hit.point + littleUp);
                        if (Physics.Raycast(from.transform.position, hit.point - from.transform.position, Vector3.Distance(hit.point, from.transform.position), _obstacle))
                        {
                            line.material.color = Color.gray;
                        }
                        else
                        {
                            line.material.color = _color;
                        }
                    }
                    else
                    {
                        if (from.transform != hit.transform && hit.transform.TryGetComponent(out BuildingContain tower))
                        {
                            
                            line.SetPosition(1, tower.transform.position + littleUp);
                            if (from.listCanGo.Contains(tower.id))
                            {
                                hitTow = tower;
                                line.material.color = _color;
                            }
                            else
                            {
                                hitTow = null;
                                line.material.color = Color.gray;
                            }
                        }
                    }
                }
            }
            if (cut)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 100, _plane))
                {
                    currentPos  = hit.point;
                    CheckDrawCut();
                    previousPos = currentPos;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hitTow != null)
            {
                LinkTower(from, hitTow);
            }
            line.positionCount = 1;
            hitTow = null;
            cut = false;
            draw=false;
        }
    }

    public void LinkTower(ArmyTower from, BuildingContain to)
    {
        if (from.gate.Count >= from.level + 1 || from.gate.Contains(to.id)) return;
        Vector3 endPos = to.transform.position + littleUp / 2f;
        
        if(to is ArmyTower _to)
        {
            if (_to.gate.Contains(from.id))
            {
                int idx = _to.gate.IndexOf(from.id);
                if(to.teamId == from.teamId)
                {
                    _to.gate.RemoveAt(idx);
                    linesList.Push(_to.road[idx]);
                    _to.road[idx].positionCount = 1;
                    _to.road.RemoveAt(idx);
                    _to.timeNow.RemoveAt(idx);
                    _to.CreatePath();
                }
                else
                {
                    Vector3 mid = (from.transform.position + to.transform.position) / 2f + littleUp / 2f;
                    _to.road[idx].SetPosition(1, mid);
                    endPos = mid;
                }
            }
        }
        LineRenderer _line;
        if(linesList.Count > 0)
        {
            _line = linesList.Pop();
        }
        else
        {
            _line = Instantiate(line);
        }
        _line.positionCount = 2;
        _line.SetPosition(0, from.transform.position + littleUp / 2f);
        _line.SetPosition(1, endPos);
        _line.material.color = ConfigData.Instance.colors[from.teamId];
        from.gate.Add(to.id);
        from.timeNow.Add(from.timeSpawnLevel[from.level]);
        from.road.Add(_line);
        from.CreatePath();
        // sound effect
    }

    public void CheckDrawCut()
    {
        foreach(var tower in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (tower.teamId == 0)
            {
                if(tower is ArmyTower tow)
                {
                    for(int i=tow.road.Count-1; i>=0; i--)
                    {
                        Vector3[] linePos = new Vector3[2];
                        linePos[0] = tow.transform.position;
                        linePos[1] = GamePlayController.Instance.playerContain.buildingCtrl.towerList[tow.gate[i]].transform.position;
                        if(Helper.CheckIntersection(previousPos, currentPos, linePos))
                        {
                            CutRoad(tow, GamePlayController.Instance.playerContain.buildingCtrl.towerList[tow.gate[i]]);
                        }
                    }
                }
            }
        }
    }

    public void CutRoad(ArmyTower from, BuildingContain to)
    {
        int x = from.gate.IndexOf(to.id);
        LineRenderer _line = from.road[x];
        _line.positionCount = 1;
        from.gate.RemoveAt(x);
        linesList.Push(_line);
        from.road.RemoveAt(x);
        from.timeNow.RemoveAt(x);
        from.CreatePath();
        if(to is ArmyTower tow)
        {
            if (tow.gate.Contains(from.id))
            {
                int y = tow.gate.IndexOf(from.id);
                LineRenderer _lineTo = tow.road[y];
                _lineTo.SetPosition(1, from.transform.position + littleUp);
            }
        }
        // sound effect
    }
}
