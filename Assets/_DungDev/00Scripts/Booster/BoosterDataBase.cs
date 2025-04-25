using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterDataBase : MonoBehaviour
{
    public IEnumerator SpeedBoost()
    {
        PropertiesUnitsBase solider  = GameController.Instance.dataContain.dataUser.CurrentCardSoldier;
        PropertiesUnitsBase beast  = GameController.Instance.dataContain.dataUser.CurrentCardBeast;
        PropertiesUnitsBase mage  = GameController.Instance.dataContain.dataUser.CurrentCardMage;

        float s_speed = solider.speed;
        float b_speed = beast.speed;
        float m_speed = mage.speed;

        solider.speed *= 1.6f;
        beast.speed *= 1.6f;
        mage.speed *= 1.6f;
        yield return new WaitForSeconds(60f);

        solider.speed = s_speed;
        beast.speed = b_speed;
        mage.speed = m_speed;
    }

}
