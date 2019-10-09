using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Teacher
{
    /// <summary>
    /// 删除教师
    /// </summary>
   public class DeleteTeacherRequest : IServiceRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [NotNull]
        public int Id { get; set; }
    }
}
