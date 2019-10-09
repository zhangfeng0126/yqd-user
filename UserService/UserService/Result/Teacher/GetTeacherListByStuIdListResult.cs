using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Teacher
{
    /// <summary>
    /// 返回教师ID集合
    /// </summary>
    public class GetTeacherListByStuIdListResult : IServiceResult
    { 
        /// <summary>
      　/// 教师IDList
     　 /// </summary>
        public List<int> TeacherIdList { get; set; }
    }
}
