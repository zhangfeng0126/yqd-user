using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models.Result
{
    /// <summary>
    /// 班级列表
    /// </summary>
    public class ClassList : IServiceResult
    {


        /// <summary>
        /// 集合
        /// </summary>
        public List<ClassInfo> Data { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

    }
    /// <summary>
    /// 班级信息
    /// </summary>
    public class ClassInfo : IServiceResult
    { 
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 年级 ID
        /// </summary>
        public int GradeId { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
