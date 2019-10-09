using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Teacher
{
   /// <summary>
   /// 编辑教师
   /// </summary>
    public class EditTeacherResult : IServiceResult
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public Boolean Result { get; set; }
    }
}
