using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Student
{
    /// <summary>
    /// 返回学生列表
    /// </summary>
    public class GetStudentListResult : DataListModel<StudentModel>, IServiceResult
    {
    }
}
