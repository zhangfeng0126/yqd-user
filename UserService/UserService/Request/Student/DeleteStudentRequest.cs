using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Student
{
    /// <summary>
    /// 删除学生
    /// </summary>
   public class DeleteStudentRequest : IServiceRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [NotNull]
        public int Id { get; set; }
    }
}
