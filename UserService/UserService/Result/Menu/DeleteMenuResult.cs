using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Menu
{
    /// <summary>
    /// 删除菜单
    /// </summary>
    public class DeleteMenuResult : IServiceResult
    { /// <summary>
      /// 返回结果
      /// </summary>
        public Boolean Result { get; set; }
    }
}
