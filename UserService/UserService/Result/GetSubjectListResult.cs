using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result
{
    /// <summary>
    /// 学科列表
    /// </summary>
   public class GetSubjectListResult : DataListModel<SubjectModel>, IServiceResult
    {
    }
}
