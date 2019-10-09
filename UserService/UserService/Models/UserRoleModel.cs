using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class UserRoleModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int RoleId { get; set; }
    }
}
