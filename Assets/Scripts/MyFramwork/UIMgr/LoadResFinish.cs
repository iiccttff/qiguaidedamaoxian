using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.MyFramwork.UIMgr
{
    /// <summary>
    /// UI资源加载完成的回调
    /// </summary>
    public class LoadResFinish : EventNode, IResLoadListener
    {
        /// <summary>
        /// 命令
        /// </summary>
        public Command cmd;
        public LoadResFinish(Command _cmd)
        {
            cmd = _cmd;
        }

        public void Finish(object asset)
        {

            if (cmd == null)
            {
                return;
            }
            GameObject go = Instantiate<GameObject>(asset as GameObject);
            go.SetActive(false);
            BaseUI ui = go.AddComponent(cmd.type) as BaseUI;
            ui.UIName = cmd.uiName;
            go.gameObject.name = ui.UIName;
            ui.CacheTransform.SetParent(UIMgr.Instance.UIROOT, false);
            UIMgr.Instance.AddUI(ui);
            if (cmd.cmdType == CmdType.CreateAndShow)
            {
                UIMgr.Instance.ShowUI(cmd.uiName, cmd.type, cmd.listener, cmd.param, cmd.createCanCall);
            }
            else if (cmd.createCanCall && cmd.listener != null)
            {
                cmd.listener.FiniSh(ui);
            }

            ui.UIInit();
        }

        public void Failure()
        {
            if (cmd.createCanCall && cmd.listener != null)
            {
                cmd.listener.Failure();
            }
        }
    }
}