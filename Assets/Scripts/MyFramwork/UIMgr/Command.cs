using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace Assets.Scripts.MyFramwork.UIMgr
{
    /// <summary>
    /// 操作UI命令集
    /// </summary>
    public class Command
    {   
        /// <summary>
        /// UI名称
        /// </summary>
        public string uiName;

        /// <summary>
        /// 要绑定的脚本
        /// </summary>
        public Type type;

        /// <summary>
        /// 加载完成之后的回调
        /// </summary>
        public ILoadUIListener listener;

        /// <summary>
        /// 要传入的数据
        /// </summary>
        public object param;

        /// <summary>
        /// 命令类型
        /// </summary>
        public CmdType cmdType;

        /// <summary>
        /// 创建时候需要回调
        /// </summary>
        public bool createCanCall = true;

        /// <summary>
        /// 获取一个显示的命令
        /// </summary>
        /// <param name="_uiName">UI名称</param>
        /// <param name="_param">要传入的参数</param>
        public static Command CreateAndShowCmd(string uiName, Type type, ILoadUIListener listener, object param, bool createCanCall)
        {
            Command cmd = new Command(CmdType.CreateAndShow, uiName, type);
            cmd.createCanCall = createCanCall;
            cmd.listener = listener;
            cmd.type = type;
            cmd.param = param;
            return cmd;
        }
        /// <summary>
        /// 获取一个显示的命令
        /// </summary>
        /// <param name="_uiName">UI名称</param>
        /// <param name="_param">要传入的参数</param>
        public static Command ShowCmd(string _uiName, ILoadUIListener listener, object _param, bool _createCanCall)
        {
            Command cmd = new Command(CmdType.Show, _uiName, _param);
            cmd.createCanCall = _createCanCall;
            cmd.listener = listener;
            return cmd;
        }


        /// <summary>
        /// 获取一个创建的命令
        /// </summary>
        /// <param name="_type">要绑定的脚本</param>
        /// <param name="_listener">加载完成之后的回调</param>
        public static Command CreateCmd(Type _type, string _uiName, ILoadUIListener _listener)
        {
            return new Command(CmdType.Create, _uiName, _type, _listener);
        }

        /// <summary>
        /// 获取一个隐藏命令
        /// </summary>
        /// <param name="_uiName">要隐藏的UI名称</param>
        /// <returns></returns>
        public static Command HideCmd(string _uiName)
        {
            return new Command(CmdType.Hide, _uiName, null);
        }

        /// <summary>
        /// 获取一个删除的命令
        /// </summary>
        /// <param name="_uiName">UI名称</param>
        /// <returns></returns>
        public static Command DestroyCmd(string _uiName)
        {
            return new Command(CmdType.Destroy, _uiName, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_cmdType">命令类型</param>
        /// <param name="_uiName">UI名称</param>
        /// <param name="_param">要传入的参数</param>
        public Command(CmdType _cmdType, string _uiName, object _param)
        {
            uiName = _uiName;
            cmdType = _cmdType;
            param = _param;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_cmdType">命令类型</param>
        /// <param name="_type">要绑定的脚本</param>
        /// <param name="_listener">加载完成之后的回调</param>
        public Command(CmdType _cmdType, string _uiName, Type _type, ILoadUIListener _listener)
        {
            cmdType = _cmdType;
            type = _type;
            listener = _listener;
            uiName = _uiName;
        }
    }
}