using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Request.Student
{
    /// <summary>
    ///获取学生列表
    /// </summary>
    public class GetStudentListRequest : IServiceRequest
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
        /// 学校ID
        /// </summary>
        [NotNull]
        public int SchoolId { get; set; }

        /// <summary>
        /// 根据学生ID集合获取学生信息
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
