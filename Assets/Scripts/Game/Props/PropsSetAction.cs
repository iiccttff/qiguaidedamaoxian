// **********************************************************************
// 
// 文件名(File Name)：             PropsSetAction
// 
// 作者(Author)：                  Circle
// 
// 创建时间(CreateTime):           2016/12/8 18:14:27
//
// 网址：                          http://blog.csdn.net/u013108312
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class PropsSetAction : MonoBehaviour 
{

    /// <summary>
    /// 碰撞到之后要执行的事件
    /// </summary>
    [Header("碰撞到之后要执行的事件")]
    public Button.ButtonClickedEvent TriggerEvent;

    [Header("角色在下落的时候能不能触发")]
    public bool isFallTrigger = false;
    public void OnTriggerEnter2D(Collider2D coll)
    {

        if (!isFallTrigger)
        {
            HeroCtrl ctrl = coll.GetComponent<HeroCtrl>();
            if (ctrl != null && ctrl.playerRigidbody2D.velocity.y < 0)
            {
                return;
            }
        }
        TriggerEvent.Invoke();

        if (eventId != EventDef.LevelEvent.None)
        {
            SendEvent();
        }
    }

    public EventDef.LevelEvent eventId = EventDef.LevelEvent.None;
    private void SendEvent()
    {
        if (AppMgr.Instance)
        AppMgr.Instance.SendEvent((int)eventId,this.gameObject);
    }
	
}
