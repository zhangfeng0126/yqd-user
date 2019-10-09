using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Role
{
    /// <summary>
    /// 编辑角色
    /// </summary>
    public class EditRoleResult : IServiceResult
    {
        /// <summary>
        /// 返回修改结果
        /// </summary>
        public Boolean Result { get; set; }
    }
}
