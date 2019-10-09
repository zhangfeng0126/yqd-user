using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request
{
    /// <summary>
    /// 导出
    /// </summary>
   public class ExportRequest:IServiceRequest
    {

       
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
        /// 用户IDList
        /// </summary>
        public List<int> IdList { get; set; }

        /// <summary>
        /// 年级Id
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public int ClassId { get; set; }


        /// <summary>
        /// 年级IdList
        /// </summary>
        public List<int> GradeIdList { get; set; }

        /// <summary>
        /// 班级IDList
        /// </summary>
        public List<int> ClassIdList { get; set; }

    }
}
