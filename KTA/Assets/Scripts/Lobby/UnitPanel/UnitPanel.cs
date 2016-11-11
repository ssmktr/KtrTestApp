using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class UnitPanel : MonoBehaviour {

    public GameObject CreateListBtn;
    public UIScrollView ScrollView;
    public UIGrid Grid;

    ObjectPaging Paging;
    List<UnitData> UnitList = new List<UnitData>();

	void Start () {
        UIEventListener.Get(CreateListBtn).onClick = (sender) =>
        {
            StartCoroutine(_GetAllUnitData());
        };
	}

    void CreateList()
    {
        UnitList.Clear();
        for (int i = 0; i < GameManager.Instance.MyUnitList.Count; ++i)
        {
            UnitList.Add(GameManager.Instance.MyUnitList[i]);
        }

        if (Paging == null)
            Paging = ObjectPaging.CreatePagingPanel(ScrollView.gameObject, Grid.gameObject, Resources.Load("UI/UnitPanel/Content/UnitDataContent") as GameObject, 1, 7, UnitList.Count, 10, PagingCalLBack, ObjectPaging.eScrollType.Vertical);
        else
            Paging.NowCreate(UnitList.Count);

        ScrollView.ResetPosition();
    }

    void PagingCalLBack(int idx, GameObject obj)
    {
        UnitDataContent content = obj.GetComponent<UnitDataContent>();
        if (UnitList.Count > idx)
        {
            content.Init(UnitList[idx]);
        }
        else
            content.Init(null);
    }

    IEnumerator _GetAllUnitData()
    {
        GameManager.Instance.MyUnitList.Clear();

        WWWForm form = new WWWForm();
        form.AddField("account_gsn", GameManager.Instance.AccountInfo.account_gsn.ToString());

        WWW www = new WWW(Login.DB_URL + "Unit/GetAllUnitData.php", form);

        yield return www;

        
        if (www.isDone)
        {
            if (www.error == null)
            {
                string Content = www.text.Trim();
                Debug.Log(Content);

                Dictionary<string, object> DicData = Json.Deserialize(Content) as Dictionary<string, object>;
                if (DicData.ContainsKey("ecode"))
                {
                    if (JsonUtil.GetStringValue(DicData, "ecode") == "success")
                    {
                        if (DicData.ContainsKey("haveUnit"))
                        {
                            List<object> UnitData = JsonUtil.GetListValue(DicData, "haveUnit");
                            for (int i = 0; i < UnitData.Count; ++i)
                            {
                                Dictionary<string, object> DicUnit = UnitData[i] as Dictionary<string, object>;
                                UnitData newData = new UnitData();
                                if (DicUnit.ContainsKey("pc_gsn"))
                                    newData.pc_gsn = JsonUtil.GetUIntValue(DicUnit, "pc_gsn");

                                if (DicUnit.ContainsKey("pc_id"))
                                    newData.pc_id = JsonUtil.GetUIntValue(DicUnit, "pc_id");

                                if (DicUnit.ContainsKey("star_grade"))
                                    newData.star_grade = JsonUtil.GetIntValue(DicUnit, "star_grade");

                                if (DicUnit.ContainsKey("pc_position"))
                                    newData.pc_position = JsonUtil.GetUIntValue(DicUnit, "pc_position");

                                if (DicUnit.ContainsKey("pc_reg_dt"))
                                    newData.pc_reg_dt = JsonUtil.GetLongValue(DicUnit, "pc_reg_dt");

                                if (DicUnit.ContainsKey("star_grade"))
                                    newData.star_grade = JsonUtil.GetIntValue(DicUnit, "star_grade");

                                //if (DicUnit.ContainsKey("skill_level_arr"))
                                //    newData.skill_level_arr = JsonUtil.GetUIntValue(DicUnit, "skill_level_arr");

                                GameManager.Instance.MyUnitList.Add(newData);
                            }
                            CreateList();
                        }
                    }
                }
            }
            else
                Debug.Log(www.error);
        }
    }
}
