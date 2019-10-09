using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Menu
{
    /// <summary>
    /// 删除菜单
    /// </summary>
    public class DeleteMenuRequest : IServiceRequest
    {   /// <summary>
        /// ID
        /// </summary>
        [NotNull]
        public List<int> Id { get; set; }
    }
}
