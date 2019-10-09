using MicroService.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UserService.Common.Models
{
    /// <summary>
    /// 文件类
    /// </summary>
   public class FileModel : IServiceResult
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public int FileId { get; set; }

        
    }
}
