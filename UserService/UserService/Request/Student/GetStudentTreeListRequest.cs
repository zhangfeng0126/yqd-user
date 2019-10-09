using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Request.Student
{
    /// <summary>
    ///获取学生列表Tree
    /// </summary>
    public class GetStudentTreeListRequest : IServiceRequest
    {
      
        /// <summary>
        /// 班级IDList
        /// </summary>
        public List<int> ClassIdList { get; set; }
    }
}
