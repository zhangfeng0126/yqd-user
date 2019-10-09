using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Menu
{
    /// <summary>
    ///修改菜单状态
    /// </summary>
    public class UpdateMenuSortRequest : IServiceRequest
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [NotNull]
        public int Id { get; set; }

        /// <summary>
        /// 类型   0 下移 1 上移
        /// </summary>
        [NotNull]
        public int Type { get; set; }

        /// <summary>
        /// 父节ID
        /// </summary>
        [NotNull]
        public int ParentId { get; set; }
    }
}
