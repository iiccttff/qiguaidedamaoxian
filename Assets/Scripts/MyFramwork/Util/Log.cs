// **********************************************************************
// 
// 文件名(File Name)：             Log.cs
// 
// 作者(Author)：                  Circle
// 
// 创建时间(CreateTime):           2016/9/21 22:45:14
//
// **********************************************************************

using UnityEngine;
using System.Collections;

public class Log
{ 
    public delegate void LogFunc(object obj);

#if UNITY_EDITOR
    public static LogFunc Debug = UnityEngine.Debug.Log;
    public static LogFunc Warning = UnityEngine.Debug.LogWarning;
    public static LogFunc Error = UnityEngine.Debug.LogError;
#else
    public static void Debug(object obj)
    {

    }

    public static void Warning(object obj)
    {

    }

    public static void Error(object obj)
    {

    }
#endif



}
