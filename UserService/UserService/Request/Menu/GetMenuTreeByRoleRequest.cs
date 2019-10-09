using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Menu
{
    /// <summary>
    /// 获取菜单列表
    /// </summary>
  public  class GetMenuTreeByRoleRequest : IServiceRequest
    {
        /// <summary>
        ///角色ID
        /// </summary>


        [NotNull]
        public int RoleId { get; set; }
       
    }
}
