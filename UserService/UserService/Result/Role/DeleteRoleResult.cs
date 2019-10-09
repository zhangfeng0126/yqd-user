using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Role
{
    /// <summary>
    /// 删除角色
    /// </summary>
    public class DeleteRoleResult : IServiceResult
    { /// <summary>
      /// 返回结果
      /// </summary>
        public Boolean Result { get; set; }
    }
}
