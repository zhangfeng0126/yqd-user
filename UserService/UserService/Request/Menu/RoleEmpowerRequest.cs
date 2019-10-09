using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Menu
{
    /// <summary>
    ///角色授权
    /// </summary>
    public class RoleEmpowerRequest : IServiceRequest
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [NotNull]
        public int RoleId { get; set; }

     

        /// <summary>
        /// 菜单（权限ID）
        /// </summary>
        [NotNull]
        public List<int> MenuId { get; set; }
    }
}
