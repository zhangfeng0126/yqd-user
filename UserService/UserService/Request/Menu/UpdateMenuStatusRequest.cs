using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Menu
{
    /// <summary>
    ///修改菜单状态
    /// </summary>
    public class UpdateMenuStatusRequest : IServiceRequest
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [NotNull]
        public List<int> Id { get; set; }

        /// <summary>
        /// 状态类型 -1 删除  0 启动 1 禁用
        /// </summary>
        [NotNull]
        public int Status { get; set; }
    }
}
