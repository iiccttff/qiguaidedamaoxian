// **********************************************************************
// 
// 文件名(File Name)：             EventCore.cs
// 
// 作者(Author)：                  Circle
// 
// 创建时间(CreateTime):           2015/8/25 0:1:36
//
// **********************************************************************

using UnityEngine;
using System.Collections;

public class EventNodeCore : EventNode 
{
    private static EventNodeCore mInstance;
    public static EventNodeCore Instance
    {
        get
        {
            return mInstance;
        }
        
    }
    void Awake()
    {
        mInstance = this;
        Debug.Log("-------------");
    }

    void OnDestroy()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect (0,0,200,50),"EventNodecore.SenEvent"))
        {
            EventNodeCore.Instance.SendEvent(EventDef.EventTest1, "测试消息发送");
        }
        if (GUI.Button(new Rect(0, 60, 200, 50), "EventNode1.SenEvent"))
        {
            EventNode1.Instance.SendEvent(EventDef.EventTest1, "测试消息发送");
            EventNode1.Instance.SendEvent(EventDef.EventTest2, "测试消息发送");
        }
        if (GUI.Button(new Rect(0, 120, 200, 50), "EventNode2.SenEvent"))
        {
            EventNode2.Instance.SendEvent(EventDef.EventTest1, "测试消息发送");
        }
    }
}
