using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Role
{
    /// <summary>
    /// 删除角色
    /// </summary>
    public class UpdateRoleStatusRequest : IServiceRequest
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [NotNull]
        public List<int> Id { get; set; }

        /// <summary>
        /// 状态类型 -1 删除  0 启动 1 禁用
        /// </summary>
        [NotNull]
        public int Status { get; set; }
    }
}
