using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Teacher
{
    /// <summary>
    /// 添加教师
    /// </summary>
    public class AddTeacherResult : IServiceResult
    {   /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
