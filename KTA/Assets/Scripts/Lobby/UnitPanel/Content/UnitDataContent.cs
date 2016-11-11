using UnityEngine;
using System.Collections;

public class UnitDataContent : MonoBehaviour {
    public GameObject Center;
    public UILabel IdLbl, StarGradeLbl, PositionLbl, RegDtLbl, StateLbl, SkillArrLbl;

    UnitData UnitData;

    public void Init(UnitData data)
    {
        UnitData = data;
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
            IdLbl.text = UnitData.pc_id.ToString();
            StarGradeLbl.text = UnitData.star_grade.ToString();
            PositionLbl.text = UnitData.pc_position.ToString();
            RegDtLbl.text = UnitData.pc_reg_dt.ToString();
            StateLbl.text = UnitData.pc_state.ToString(); ;
            //SkillArrLbl.text = UnitData.skill_level_arr;
        }
    }
}
