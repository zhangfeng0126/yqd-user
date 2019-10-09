using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.User
{
    /// <summary>
    /// 添加用户
    /// </summary>

    public class AddUserResult : IServiceResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
