using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_BottomCtrl : LoadAutoComponents
{
    [SerializeField] InfoBox infoBox;
    public InfoBox InfoBox => infoBox;

    [SerializeField] EvolutionInfoBox evolutionInfoBox;
    public EvolutionInfoBox EvolutionInfoBox => evolutionInfoBox;

    [SerializeField] BtnUpgradeByCoin btnUpgradeByCoin;
    public BtnUpgradeByCoin BtnUpgradeByCoin => btnUpgradeByCoin;

    [SerializeField] BtnEvolveByGem btnUpgradeByGem;
    public BtnEvolveByGem BtnUpgradeByGem => btnUpgradeByGem;
    public override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadChild();
    }

    public void LoadChild()
    {
        this.infoBox = GetComponentInChildren<InfoBox>();
        this.evolutionInfoBox = GetComponentInChildren<EvolutionInfoBox>();
        this.btnUpgradeByCoin = GetComponentInChildren<BtnUpgradeByCoin>();
        this.btnUpgradeByGem = GetComponentInChildren<BtnEvolveByGem>();
    }
}
