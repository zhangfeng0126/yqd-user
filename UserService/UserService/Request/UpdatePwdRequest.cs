using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request
{
    /// <summary>
    /// 修改密码
    /// </summary>
   public class UpdatePwdRequest : IServiceRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }
        
    }
}
