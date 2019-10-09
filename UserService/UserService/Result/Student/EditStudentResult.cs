using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Student
{
   /// <summary>
   /// 编辑学生
   /// </summary>
    public class EditStudentResult : IServiceResult
    {
        /// <summary>
        /// 返回修改结果
        /// </summary>
        public Boolean Result { get; set; }
    }
}
