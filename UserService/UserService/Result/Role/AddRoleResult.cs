using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Role
{
    /// <summary>
    /// 添加角色
    /// </summary>
    public class AddRoleResult : IServiceResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
