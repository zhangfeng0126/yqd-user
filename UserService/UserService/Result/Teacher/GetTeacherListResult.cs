using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Teacher
{
    /// <summary>
    /// 返回教师列表
    /// </summary>
    public class GetTeacherListResult : DataListModel<TeacherModel>, IServiceResult
    {
    }
}
