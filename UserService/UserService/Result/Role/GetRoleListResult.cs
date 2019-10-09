using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Role
{
    /// <summary>
    /// 返回角色列表
    /// </summary>
    public class GetRoleListResult : DataListModel<RoleModel>, IServiceResult
    {
    }
}
