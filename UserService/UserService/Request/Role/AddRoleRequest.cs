using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Role
{
    /// <summary>
    /// 新增角色
    /// 
    /// </summary>
   public class AddRoleRequest : IServiceRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        /// 
        [NotNull]
        public string Name { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        [NotNull]
        public string RoleName { get; set; }
    }
}
