using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request
{
    /// <summary>
    /// 用户登录
    /// </summary>
   public class LoginRequest : IServiceRequest
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }
        
    }
}
