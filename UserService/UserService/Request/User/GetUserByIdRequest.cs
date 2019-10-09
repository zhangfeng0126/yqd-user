using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.User
{
    /// <summary>
    /// 根据ID获取用户信息
    /// </summary>
   public class GetUserByIdRequest : IServiceRequest
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [NotNull]
        public int Id { get; set; }
    }
}
