using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public List<CharacterBase> allyList = new List<CharacterBase>();
    public Stack<CharacterBase>[,] unitGrid;
    public Dictionary<Collider, CharacterBase> componentDict = new Dictionary<Collider, CharacterBase>();
    
    
}
