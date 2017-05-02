using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace Assets.Scripts.MyFramwork.UIMgr
{
    /// <summary>
    /// 界面加载回调
    /// </summary>
    public interface ILoadUIListener
    {
        void FiniSh(BaseUI ui);
        void Failure();
    }
}