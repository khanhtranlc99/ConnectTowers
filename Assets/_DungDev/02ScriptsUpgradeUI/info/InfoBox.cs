using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    [SerializeField] List<InfoBoxSpecialSkill> lsInfoBoxSpecialSkill = new();
    [SerializeField] List<InfoBoxStarAttribute> lsInfoBoxStarAttribute = new();


    public void SetInfoBox(UnitsType unitsType)
    {
        PropertiesUnitsBase unitData = GameController.Instance.dataContain.dataUnits.GetPropertiesBases(unitsType);


        List<UnitSpecialSkill> lsUnitSpecialSkill = unitData.lsUnitSpecialSkills;
        List<StarLevelBonus> lsStarLevelBonus = unitData.lsStartLevelBonus;
        // cap nhat danh sach ki nang dac biet left
        for (int i = 0; i < this.lsInfoBoxSpecialSkill.Count; i++)
        {
            if(i < lsUnitSpecialSkill.Count)
            {
                lsInfoBoxSpecialSkill[i].skillText.text = lsUnitSpecialSkill[i].skillName;
                lsInfoBoxSpecialSkill[i].skillValueText.text = unitData.GetSkillValue(lsUnitSpecialSkill[i].skillName).ToString();

                lsInfoBoxSpecialSkill[i].imgSpecial.sprite = lsUnitSpecialSkill[i].skillIcon;
                lsInfoBoxSpecialSkill[i].imgSpecial.gameObject.SetActive(true);
                lsInfoBoxSpecialSkill[i].skillText.gameObject.SetActive(true);
            }
            else
            {
                lsInfoBoxSpecialSkill[i].skillText.gameObject.SetActive(false);
                lsInfoBoxSpecialSkill[i].imgSpecial.gameObject.SetActive(false);
            }
        }

        // cap nhat danh sach ki nang theo star - right

        for (int i = 0; i < this.lsInfoBoxStarAttribute.Count; i++)
        {
            if (i < lsStarLevelBonus.Count)
            {
                lsInfoBoxStarAttribute[i].attributeText.text = lsStarLevelBonus[i].bonusName;
                lsInfoBoxStarAttribute[i].attributeBonusText.text = lsStarLevelBonus[i].bonusValue.ToString();
            }
            else
            {
                //note:
            }
        }
    }
}


[System.Serializable]
public class InfoBoxSpecialSkill  
{
    public TextMeshProUGUI skillText;
    public Image imgSpecial;
    public TextMeshProUGUI skillValueText;

}

[System.Serializable]
public class InfoBoxStarAttribute  
{
    public TextMeshProUGUI attributeText;
    public Image imgStarOn;
    public TextMeshProUGUI attributeBonusText;

}