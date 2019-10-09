using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Student
{
    /// <summary>
    /// 添加学生
    /// </summary>
   public class AddStudentResult : IServiceResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
