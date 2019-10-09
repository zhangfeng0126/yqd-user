using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Menu
{
    /// <summary>
    /// 获取菜单列表
    /// </summary>
  public  class GetMenuListRequest : IServiceRequest
    {
        /// <summary>
        /// 当前页
        /// </summary>
        [NotNull]
        public int Page { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        [NotNull]
        public int Rows { get; set; }


        /// <summary>
        /// 是否分页 0 分页 1 不分页
        /// </summary>
        [NotNull]
        public int IsPage { get; set; }

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
