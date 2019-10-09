using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result
{
   /// <summary>
   /// 导入
   /// </summary>
    public class ImportResult : IServiceResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public Boolean Result { get; set; }
    }
}
