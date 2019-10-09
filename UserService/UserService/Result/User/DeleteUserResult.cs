using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.User
{
    /// <summary>
    /// 删除用户
    /// </summary>
    public class DeleteUserResult : IServiceResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Boolean Result { get; set; }
    }
}
