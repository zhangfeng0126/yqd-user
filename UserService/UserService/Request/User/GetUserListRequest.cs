using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Request.User
{
    /// <summary>
    /// 获取用户列表
    /// </summary>
   public class GetUserListRequest : IServiceRequest
    {
        /// <summary>
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
        /// 权限
        /// </summary>
         public int RoleId { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Seach { get; set; }
        /// <summary>
        /// 学校ID
        /// </summary>
        [NotNull]
        public int SchoolId { get; set; }
    }
}
