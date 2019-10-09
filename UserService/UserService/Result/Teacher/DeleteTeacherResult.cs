using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Teacher
{
    /// <summary>
    /// 删除教师
    /// </summary>
    public class DeleteTeacherResult : IServiceResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Boolean Result { get; set; }
    }
}
