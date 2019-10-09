using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Student
{
    /// <summary>
    /// 根据ID获取学生信息
    /// </summary>
   public class GetStudentByIdRequest : IServiceRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [NotNull]
        public int Id { get; set; }
    }
}
