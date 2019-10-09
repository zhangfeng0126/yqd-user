using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result
{
    /// <summary>
    /// 用户登录
    /// </summary>
   public class LoginResult: IServiceResult
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public UserModel Data { get; set; }


        /// <summary>
        /// 是否成功
        /// </summary>
        public Boolean Success { get; set; }
    }
}
