using MicroService.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UserService.Models;

namespace UserService.Result
{
    /// <summary>
    /// 导出
    /// </summary>
   public class ExportResult : IServiceResult
    {
        /// <summary>
        ///文件Id
        /// </summary>
        public int  FileId { get; set; }

        ///// <summary>
        ///// 类型
        ///// </summary>
        //public string ContentType { get; set; }
        ///// <summary>
        ///// 文件名称
        ///// 
        ///// </summary>
        //public string FileName { get; set; }
    }
}
