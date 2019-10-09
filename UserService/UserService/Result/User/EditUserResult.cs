using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.User
{
    /// <summary>
    /// 修改用户
    /// </summary>
    public class EditUserResult : IServiceResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Boolean Result { get; set; }
    }
}
