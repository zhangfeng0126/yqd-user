using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Role
{
    /// <summary>
    /// 获取角色列表
    /// </summary>
   public class GetRoleListRequest : IServiceRequest
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
        /// 学校ID，当角色非超级管理员时候必填
        /// </summary>
     
        public int SchoolId { get; set; }
    }
}
