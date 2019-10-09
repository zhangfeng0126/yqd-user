using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Teacher
{
    /// <summary>
    /// 根据学生ID获取教师集合
    /// </summary>
    public class GetTeacherListByStuIdListRequest : IServiceRequest
    {
        
        /// <summary>
        /// 学生ID集合
        /// </summary>
        public List<int>  StudentIdList{ get; set; }

    }
}
