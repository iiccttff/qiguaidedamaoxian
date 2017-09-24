// **********************************************************************
// 
// 文件名(File Name)：             EventListener1.cs
// 
// 作者(Author)：                  Circle
// 
// 创建时间(CreateTime):           2015/8/25 0:2:26
//
// **********************************************************************

using UnityEngine;
using System.Collections;

public class EventListener2 : MonoBehaviour,IEventListener 
{
	// Awake is called when the script instance is being loaded.
	void Awake() 
	{
        if (EventNode2.Instance)
        {
            EventNode2.Instance.AttachEventListener(EventDef.EventTest1, this);
        }
	}

    void OnDestroy()
    {
        if (EventNode2.Instance)
        {
            EventNode2.Instance.DetachEventListener(EventDef.EventTest1, this);
        }
    }

    public bool HandleEvent(int id, object param1, object param2)
    {
        Debug.Log("EventListener2.HandleEvent => id =" + id + " param1=" + param1);
        return false;
    }

    public int EventPriority()
    {
        return 999;
    }
}
