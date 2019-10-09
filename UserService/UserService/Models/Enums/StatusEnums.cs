using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models.Enums
{
    /// <summary>
    /// 状态
    /// </summary>
   public enum StatusEnums
    { /// <summary>
      /// 正常
      /// </summary>
        Normal = 0,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = -1,
        /// <summary>
        /// 异常
        /// </summary>
        Abnormal = 2,
    }
}
