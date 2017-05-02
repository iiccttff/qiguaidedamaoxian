// **********************************************************************
// 
// 文件名(File Name)：             Initializer
// 
// 作者(Author)：                  #AuthorName#
// 
// 创建时间(CreateTime):           #CreateTime#
//
// 网址：                          http://blog.csdn.net/u013108312
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using Assets.Scripts.MyFramwork.UIMgr;

public class Initializer :MonoBehaviour
{
	void Start()
	{
        if (this.transform.FindChild("UIRoot") == null)
        {
            GameObject go = new GameObject("UIRoot");
            go.transform.parent = this.transform;
        }
        
        DontDestroyOnLoad(this.gameObject);

        StartGame();

        
	}
	
    void OnDestroy()
    {
    }

	
    void StartGame()
    {
        this.gameObject.AddComponent<ResMgr>();
        this.gameObject.AddComponent<UIMgr>();
        this.gameObject.AddComponent<AppMgr>();
        this.gameObject.AddComponent<TableDataMgr>();
    }

   
}
