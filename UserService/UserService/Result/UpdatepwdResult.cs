using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result
{
    /// <summary>
    /// 修改密码
    /// </summary>
   public class UpdatePwdResult : IServiceResult
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public Boolean Result { get; set; }
    }
}
