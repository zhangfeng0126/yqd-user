using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Request.Teacher
{
    /// <summary>
    /// 获取教师列表
    /// </summary>
   public class GetTeacherListRequest : IServiceRequest
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
        /// 是否分页 0 分页 1 不分页
        /// </summary>
        [NotNull]
        public int IsPage { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>

        public string Seach { get; set; }

        /// <summary>
        /// 学校ID
        /// </summary>
        [NotNull]
        public int SchoolId { get; set; }

        /// <summary>
        /// 教师IDList
        /// </summary>
        public List<int> IdList { get; set; }
    }
}
