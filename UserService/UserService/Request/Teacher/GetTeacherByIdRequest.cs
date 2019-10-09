using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.Teacher
{
    /// <summary>
    /// 根据 ID获取教师信息
    /// </summary>
   public class GetGetMenuTreeRequest : IServiceRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }
    }
}
