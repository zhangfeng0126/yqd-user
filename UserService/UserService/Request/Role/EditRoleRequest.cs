using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Role
{

    /// <summary>
    /// 编辑角色
    /// </summary>
    public class EditRoleRequest : IServiceRequest
    {

        /// <summary>
        /// 角色 ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// 
        [NotNull]
        public string Name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [NotNull]
        public int Status { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        [NotNull]
        public string RoleName { get; set; }
    }
}
