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
        form.AddField("account_gsn", 2);

        WWW www = new WWW(Login.DB_URL + "UnitData/GetAllUnitData.php", form);

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
                            List<object> UnitData = DicData["haveUnit"] as List<object>;
                            for (int i = 0; i < UnitData.Count; ++i)
                            {
                                UnitData data = UnitData[i] as UnitData;
                                UnitData newData = new UnitData();
                                //newData.pc_id = UnitData[i]

                                GameManager.Instance.MyUnitList.Add(data);
                                Debug.Log(data.pc_id);
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
