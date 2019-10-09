using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Role
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class GetRoleByIdResult : IServiceResult
    {     /// <summary>
          /// 返回信息
          /// </summary>
        public RoleModel Data { get; set; }
    }
}
