// **********************************************************************
// 
// 文件名(File Name)：             LevelMgr
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/12/18 18:50:29
//
// 网址：                          www.youke.pro
//
// **********************************************************************

using UnityEngine;
using System.Collections;

public class LevelMgr : BaseUI , IEventListener , UIMgr.ILoadUIListener
{

    private int mCurrentLevel;

    /// <summary>
    /// 初始化当前界面
    /// </summary>
    protected override void OnInit()
    {

        if (AppMgr.Instance)
        {
            AppMgr.Instance.AttachEventListener((int)EventDef.LevelEvent.PlayerDie, this);
            AppMgr.Instance.AttachEventListener((int)EventDef.LevelEvent.GameOver, this);
            AppMgr.Instance.AttachEventListener((int)EventDef.LevelEvent.SaveGame, this);
        }
    }

    /// <summary>
    /// 显示当前界面
    /// </summary>
    /// <param name="param">附加参数</param>
    protected override void OnShow(object param) 
    {
        if (param != null)
        mCurrentLevel = (int)param;
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
        if (AppMgr.Instance)
        {
            AppMgr.Instance.DetachEventListener((int)EventDef.LevelEvent.PlayerDie, this);
            AppMgr.Instance.DetachEventListener((int)EventDef.LevelEvent.GameOver, this);
            AppMgr.Instance.DetachEventListener((int)EventDef.LevelEvent.SaveGame, this);
        }
    }


    public bool HandleEvent(int id, object param1, object param2)
    {
        EventDef.LevelEvent evid = (EventDef.LevelEvent)id;
        switch (evid)
        {
            case EventDef.LevelEvent.PlayerDie:
                UIMgr.Instance.ShowUI(UIDef.DieUI, typeof(DieCtrl), this,mCurrentLevel);
                return false;
            case EventDef.LevelEvent.GameOver:
                Log.Debug("---------游戏完成！");

                if (!AppMgr.Instance.OpenLevels.Contains(mCurrentLevel + 1))
                {
                    AppMgr.Instance.AddOpenLevel(mCurrentLevel + 1);
                }
                
                return false;
            case EventDef.LevelEvent.SaveGame:
                {
                    GameObject go = (GameObject)param1;
                    AppMgr.Instance.HeroPos = go.transform.position;
                }
                return false;
        }
        return false;
    }

    public int EventPriority()
    {
        return 0;
    }

    public void FiniSh(BaseUI ui)
    {
        //throw new System.NotImplementedException();
    }

    public void Failure()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnAwake()
    {
        
    }
}
