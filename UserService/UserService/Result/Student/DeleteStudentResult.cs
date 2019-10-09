using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Student
{
    /// <summary>
    /// 删除
    /// </summary>
    public class DeleteStudentResult : IServiceResult
    { /// <summary>
      /// 返回结果
      /// </summary>
        public Boolean Result { get; set; }
    }
}
