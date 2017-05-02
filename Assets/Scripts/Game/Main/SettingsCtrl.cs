// **********************************************************************
// 
// 文件名(File Name)：             SettingsCtrl
// 
// 作者(Author)：                  Circle
// 
// 创建时间(CreateTime):           2016/11/18 18:52:40
//
// 网址：                          http://blog.csdn.net/u013108312
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.MyFramwork.UIMgr;

public class SettingsCtrl : BaseUI , ILoadUIListener
{
    private List<string> mFindNames = new List<string>()
    {
        "BtnSound",
        "BtnMusic",
        "BtnBack",
    };

    private GameObject mBtnSound = null;
    private GameObject mBtnMusic = null;

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

                if (findTrans[i].name.Equals(mFindNames[0]))
                {
                    mBtnMusic = findTrans[i].transform.GetChild(0).gameObject;
                }
                else if (findTrans[i].name.Equals(mFindNames[1]))
                {
                    mBtnSound = findTrans[i].transform.GetChild(0).gameObject;
                }
            }
        }


    }

    private void OnBtnClick(GameObject arg0)
    {
        
        if (arg0.name.Equals(mFindNames[0]))
        {
            Transform child = arg0.transform.GetChild(0);
            child.gameObject.SetActive(!child.gameObject.activeSelf);
            AppMgr.Instance.MusicValue = child.gameObject.activeSelf;
        }
        else if (arg0.name.Equals(mFindNames[1]))
        {
            Transform child = arg0.transform.GetChild(0);
            child.gameObject.SetActive(!child.gameObject.activeSelf);
            AppMgr.Instance.SoundValue = child.gameObject.activeSelf;
        }
        else
        {
            UIMgr.Instance.ShowUI(UIDef.StartUI, typeof(StartCtrl), this);
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
        mBtnSound.SetActive(AppMgr.Instance.SoundValue);
        mBtnMusic.SetActive(AppMgr.Instance.MusicValue);
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
        UIMgr.Instance.HideUI(base.UIName);
    }

    public void Failure()
    {
    }
}
