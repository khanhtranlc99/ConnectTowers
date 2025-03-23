using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PanelCardCtrl : MonoBehaviour
{
    [SerializeField] List<S_CardSlot> lsCardSlots = new();
    public List<S_CardSlot> LsCardSlots => lsCardSlots;
}
