using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.User
{
   /// <summary>
   /// 删除用户
   /// </summary>
   public class DeleteUserRequest : IServiceRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [NotNull]
        public int Id { get; set; }
    }
}
