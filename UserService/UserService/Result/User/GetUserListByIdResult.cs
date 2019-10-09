using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.User
{
    /// <summary>
    /// 根据ID获取用户信息
    /// </summary>
    public class GetUserListByIdResult : DataListModel<UserModel>, IServiceResult
    {
       
    }
}
