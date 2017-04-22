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

public class Tilemap
{
    public int width, height, tileheight, tilewidth, version;
	public List<TilemapLayer> layers;
    public string orientation;
    //public TilemapTileset[] tilesets;
}
