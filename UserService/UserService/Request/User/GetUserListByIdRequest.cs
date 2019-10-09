using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.User
{
    /// <summary>
    /// 根据ID获取用户信息
    /// </summary>
   public class GetUserListByIdRequest : IServiceRequest
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [NotNull]
        public List<int>  IdList { get; set; }
    }
}
