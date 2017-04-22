// **********************************************************************
// 
// 文件名(File Name)：             StartCtrl
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/11/18 18:52:40
//
// 网址：                          www.youke.pro
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartCtrl : BaseUI , UIMgr.ILoadUIListener
{
    private List<string> mFindNames = new List<string>()
    {
        "BtnStart",
        "BtnSettings"
    };

    /// <summary>
    /// 初始化当前界面
    /// </summary>
    protected override void OnInit()
    {
        List<Transform> findTrans = new List<Transform>();
        ComUtil.GetTransformInChild(mFindNames, CacheTransform, ref findTrans);

        for (int i = 0; i < findTrans.Count;i++ )
        {
            if (findTrans[i].name.StartsWith("Btn"))
            {
                EventTrigger btn = findTrans[i].GetComponent<EventTrigger>();
                EventTrigger.Entry ev = new EventTrigger.Entry();
                ev.callback.AddListener((BaseEventData arg0) => { OnBtnClick(btn.gameObject); });
                ev.eventID = EventTriggerType.PointerClick;

                
                btn.triggers.Add(ev);
            }
        }
    }

    private void OnBtnClick(GameObject arg0)
    {
        
        if (arg0.name.Equals(mFindNames[0]))
        {
            UIMgr.Instance.ShowUI(UIDef.SelectLevelUI, typeof(SelectLevelCtrl), this);
            //点击了开始按钮//
        }
        else if (arg0.name.Equals(mFindNames[1]))
        {
            UIMgr.Instance.ShowUI(UIDef.SettingsUI,typeof(SettingsCtrl),this);
            //点击了设置按钮//
        }
        //throw new System.NotImplementedException();
    }

    protected override void OnAwake() 
    {

    }

    /// <summary>
    /// 显示当前界面
    /// </summary>
    /// <param name="param">附加参数</param>
    protected override void OnShow(object param) 
    {

    }

    /// <summary>
    /// 隐藏当前界面
    /// </summary>
    protected override void OnHide() 
    {

    }

    /// <summary>
    /// 删除当前UI 
    /// </summary>
    protected override void OnDestroy()
    {

    }


    public void FiniSh(BaseUI ui)
    {
        if (ui.UIName == UIDef.SettingsUI
            || ui.UIName == UIDef.SelectLevelUI)
        {
            UIMgr.Instance.HideUI(this.UIName);
        }
    }

    public void Failure()
    {
    }
}
