// **********************************************************************
// 
// 文件名(File Name)：             LevelSelectCtrl.cs
// 
// 作者(Author)：                  小新
// 
// 交流群号(QQ Group):             190184450
//
// **********************************************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilemapLayer {

	public string name, type;
	public List<int> data;
	public int width, height, x, y;
	public bool visible;
	public float opacity;

	public TilemapLayer(){
	}
}
