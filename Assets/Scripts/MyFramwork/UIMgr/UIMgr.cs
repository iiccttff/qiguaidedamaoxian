// **********************************************************************
// 
// 文件名(File Name)：             UIMgr.cs
// 
// 作者(Author)：                  Circle
// 
// 创建时间(CreateTime):           2016/9/14 12:31:20
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.MyFramwork.UIMgr
{
    public class UIMgr : EventNode
    {
        private static UIMgr mInstance;
        public static UIMgr Instance
        {
            get
            {
                return mInstance;
            }
        }

        /// <summary>
        /// 所有UI
        /// </summary>
        private Dictionary<string, BaseUI> mDicUI = new Dictionary<string, BaseUI>();

        /// <summary>
        /// 添加一个UI
        /// </summary>
        /// <param name="ui"></param>
        public void AddUI(BaseUI ui)
        {
            if (ui != null)
            {
                mDicUI[ui.UIName] = ui;

            }

        }

        /// <summary>
        /// 移除一个UI
        /// </summary>
        /// <param name="ui"></param>
        public void RemoveUI(BaseUI ui)
        {
            if (ui != null && mDicUI.ContainsKey(ui.UIName))
            {
                mDicUI.Remove(ui.UIName);
            }
        }

        /// <summary>
        /// 所有命令集合
        /// </summary>
        public List<Command> cmdList = new List<Command>();

        internal Transform UIROOT = null;
        void Awake()
        {
            UIROOT = this.transform.FindChild("UIRoot");
            mInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public List<string> GetCurrentOpenUI()
        {
            List<string> list = new List<string>();
            foreach (BaseUI ui in mDicUI.Values)
            {
                if (ui.CacheGameObject.activeInHierarchy)
                {
                    list.Add(ui.UIName);
                }
            }
            return list;
        }

        #region 创建UI

        /// <summary>
        /// 创建UI
        /// </summary>
        /// <param name="uiName">UI名称</param>
        /// <param name="type">要绑定的脚本</param>
        /// <param name="listener">创建完成的回调</param>
        public void CreateUI(string uiName, Type type, ILoadUIListener listener)
        {
            cmdList.Add(Command.CreateCmd(type, uiName, listener));
        }

        /// <summary>
        /// 创建UI的实体部分
        /// </summary>
        /// <param name="cmd">命令</param>
        private void _Create(Command cmd)
        {

            BaseUI ui = null;
            mDicUI.TryGetValue(cmd.uiName, out ui);
            if (ui != null)
            {
                if (cmd.listener != null) cmd.listener.FiniSh(ui);
            }
            else
            {

                ResMgr.Instance.Load(cmd.uiName, new LoadResFinish(cmd), typeof(GameObject), true);
            }
        }

        #endregion

        #region 显示UI

        /// <summary>
        /// 显示一个UI界面  如果不存在就创建
        /// </summary>
        /// <param name="uiName">ui名称</param>
        /// <param name="type">要绑定的脚本</param>
        /// <param name="listener">如果界面不存在则会有界面加载完成后的回调</param>
        /// <param name="param">要传入的参数</param>
        public void ShowUI(string uiName, Type type, ILoadUIListener listener, object param = null, bool createCanCall = false)
        {
            Command cmd = Command.ShowCmd(uiName, listener, param, createCanCall);
            cmd.type = type;
            cmdList.Add(cmd);
        }

        /// <summary>
        /// 显示一个界面
        /// </summary>
        /// <param name="cmd"></param>
        private void _ShowUI(Command cmd)
        {

            BaseUI ui = null;
            mDicUI.TryGetValue(cmd.uiName, out ui);
            if (ui != null)
            {
                if (cmd.listener != null)
                {
                    cmd.listener.FiniSh(ui);
                }
                ui.Show(cmd.param);

            }
        }


        #endregion

        #region  隐藏UI

        /// <summary>
        /// 隐藏这个UI
        /// </summary>
        /// <param name="uiName"></param>
        public void HideUI(string uiName)
        {

            cmdList.Add(Command.HideCmd(uiName));
        }


        private void _HideUI(Command cmd)
        {
            BaseUI ui = null;
            mDicUI.TryGetValue(cmd.uiName, out ui);
            if (ui != null)
            {
                ui.Hide();
            }
        }
        #endregion

        #region  删除UI

        /// <summary>
        /// 删除UI
        /// </summary>
        /// <param name="uiName">UI名称</param>
        public void DestroyUI(string uiName)
        {
            cmdList.Add(Command.DestroyCmd(uiName));
        }

        private void _DestroyUI(Command cmd)
        {
            BaseUI ui = null;
            mDicUI.TryGetValue(cmd.uiName, out ui);
            if (ui != null)
            {
                mDicUI.Remove(ui.UIName);
                Destroy(ui.CacheGameObject);
            }
        }

        #endregion

        // Update is called every frame, if the MonoBehaviour is enabled.
        void Update()
        {

            if (cmdList.Count > 0)
            {
                Command tempCmd = null;
                tempCmd = cmdList[0];
                if (tempCmd == null)
                {
                    cmdList.RemoveAt(0);
                }
                else
                {
                    switch (tempCmd.cmdType)
                    {
                        case CmdType.CreateAndShow:
                            _Create(tempCmd);
                            break;
                        case CmdType.Create:

                            _Create(tempCmd);
                            break;
                        case CmdType.Destroy:
                            _DestroyUI(tempCmd);
                            break;
                        case CmdType.Hide:

                            _HideUI(tempCmd);
                            break;
                        case CmdType.Show:
                            BaseUI ui = null;
                            mDicUI.TryGetValue(tempCmd.uiName, out ui);
                            if (ui == null)
                            {
                                cmdList.RemoveAt(0);
                                cmdList.Insert(0, Command.CreateAndShowCmd(tempCmd.uiName, tempCmd.type, tempCmd.listener, tempCmd.param, tempCmd.createCanCall));

                                return;
                            }
                            _ShowUI(tempCmd);
                            break;
                    }
                    cmdList.RemoveAt(0);
                }
            }
        }

    }
}