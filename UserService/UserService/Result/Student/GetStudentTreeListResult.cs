using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Student
{
    /// <summary>
    /// 返回学生列表
    /// </summary>
    public class GetStudentTreeListResult : IServiceResult
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public IEnumerable<StudentTree> Data { get; set; }

    }

    /// <summary>
    /// 学生信息
    /// </summary>
    public class StudentTree
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型<user>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 学校ID
        /// </summary>
        public int SchoolId { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public int ClassId { get; set; }

    }
}
