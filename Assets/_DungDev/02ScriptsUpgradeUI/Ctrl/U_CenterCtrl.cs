using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_CenterCtrl : LoadAutoComponents
{
    [SerializeField] protected SoldiersCardCtrl soldiersCardCtrl;
    public SoldiersCardCtrl SoldiersCardCtrl => soldiersCardCtrl;

    [SerializeField] protected BeastCardCtrl beastCardCtrl;
    public BeastCardCtrl BeastCardCtrl => beastCardCtrl;

    [SerializeField] protected MageCardCtrl mageCardCtrl;
    public MageCardCtrl MageCardCtrl => mageCardCtrl;

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.soldiersCardCtrl = GetComponentInChildren<SoldiersCardCtrl>();
        this.beastCardCtrl = GetComponentInChildren<BeastCardCtrl>();
        this.mageCardCtrl = GetComponentInChildren<MageCardCtrl>();
    }

}
