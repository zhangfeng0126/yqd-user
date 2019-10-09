using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Menu
{
    /// <summary>
    /// 根据ID获取菜单信息
    /// </summary>
   public class GetMenuByIdRequest : IServiceRequest
    {   /// <summary>
        /// 用户ID
        /// </summary>
        [NotNull]
        public int Id { get; set; }
    }
}
