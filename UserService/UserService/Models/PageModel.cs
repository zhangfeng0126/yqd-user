using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 分页
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPage { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Seach { get; set; }

        /// <summary>
        /// 学校ID
        /// </summary>
        public int  SchoolId {get;set;}
    }
}
