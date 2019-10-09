using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Role
{
    /// <summary>
    /// 角色
    /// </summary>
   public class GetRoleByIdRequest : IServiceRequest
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [NotNull]
        public int Id { get; set; }
    }
}
