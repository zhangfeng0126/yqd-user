using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Menu
{
    /// <summary>
    /// 获取菜单列表
    /// </summary>
  public  class GetMenuTreeRequest : IServiceRequest
    {
       

        /// <summary>
        /// 关键字
        /// </summary>
        public string Seach { get; set; }

        /// <summary>
        /// 等于-1 时查询全部
        /// </summary>
        [NotNull]
        public int ParentId { get; set; }
        /// <summary>
        /// 0 全部  1 菜单   2 按钮
        /// </summary>
        [NotNull]
        public int IsMenu { get; set; }
    }
}
