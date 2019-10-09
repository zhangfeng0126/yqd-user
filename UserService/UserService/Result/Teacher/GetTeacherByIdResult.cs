using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Teacher
{
    /// <summary>
    /// 教师信息
    /// </summary>
    public class GetTeacherByIdResult : IServiceResult
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public TeacherModel Data { get; set; }
    }
}
