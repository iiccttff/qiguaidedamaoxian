using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MyFramwork.UIMgr
{
    /// <summary>
    /// 命令类型
    /// </summary>
    public enum CmdType
    {
        /// <summary>
        /// 创建
        /// </summary>
        CreateAndShow,
        /// <summary>
        /// 创建
        /// </summary>
        Create,
        /// <summary>
        /// 显示或者刷新
        /// </summary>
        Show,
        /// <summary>
        /// 隐藏
        /// </summary>
        Hide,
        /// <summary>
        /// 删除
        /// </summary>
        Destroy,
    }
}