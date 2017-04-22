using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 [Serializable]
public class EventDef
{
    #region 系统消息 1-1000
    
    public const int ResLoadFinish = 1;

    #endregion

#region 关卡时间 2000 -3000
   

    [Serializable]
    public enum LevelEvent : int
    {
        None = 2000,
        PlayerDie,
        SaveGame,
        GameOver,
    }
#endregion


}
