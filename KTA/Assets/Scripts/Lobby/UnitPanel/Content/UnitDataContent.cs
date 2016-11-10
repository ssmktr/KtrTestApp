using UnityEngine;
using System.Collections;

public class UnitDataContent : MonoBehaviour {
    public GameObject Center;
    public UILabel IdLbl, StarGradeLbl, PositionLbl, RegDtLbl, StateLbl, SkillArrLbl;

    UnitData UnitData;

    public void Init(UnitData data)
    {
        UnitData = null;
        if (UnitData == null)
        {
            Center.SetActive(false);
        }
        else
        {
            Center.SetActive(true);
            DataUpdate();
        }
    }

    public void DataUpdate()
    {
        if (UnitData != null)
        {

        }
    }
}
