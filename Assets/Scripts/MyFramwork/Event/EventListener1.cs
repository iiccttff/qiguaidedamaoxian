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

public class EventListener1 : MonoBehaviour , IEventListener
{
	// Awake is called when the script instance is being loaded.
	void Awake() 
	{
        if (EventNode1.Instance)
        {
            EventNode1.Instance.AttachEventListener(EventDef.EventTest1, this);
            EventNode1.Instance.AttachEventListener(EventDef.EventTest2, this);
        }
	}
    void OnDestroy()
    {
        if (EventNode1.Instance)
        {
            EventNode1.Instance.DetachEventListener(EventDef.EventTest1, this);
            EventNode1.Instance.DetachEventListener(EventDef.EventTest2, this);
        }
    }
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	void Start()
	{
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	void Update() 
	{
	}

    public bool HandleEvent(int id, object param1, object param2)
    {
        Debug.Log("EventListener1." + "HandleEvent => id =" + id + " param1=" + param1);
        return false;
    }

    public int EventPriority()
    {
        return 0;
    }
}
