using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.User
{
    /// <summary>
    /// 返回数据
    /// </summary>
    public class GetUserListResult : DataListModel<UserModel>, IServiceResult
    {
    }
}
