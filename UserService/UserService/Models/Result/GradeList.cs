using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models.Result
{
    /// <summary>
    /// 年级列表
    /// </summary>
   public class GradeList : IServiceResult
    {

        /// <summary>
        /// 集合
        /// </summary>
        public List<GradeInfo> Data { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        
    }
    /// <summary>
    /// 年级
    /// </summary>

    public class GradeInfo : IServiceResult
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学校 ID
        /// </summary>
        public int SchoolId { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// 年段
        /// </summary>
        public string YearSegment { get; set; }
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
