using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Student
{
    /// <summary>
    /// 学生信息
    /// </summary>
    public class GetStudentByIdResult : IServiceResult
    {     /// <summary>
          /// 返回信息
          /// </summary>
        public StudentModel Data { get; set; }
    }
}
