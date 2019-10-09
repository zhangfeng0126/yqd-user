using MicroService.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UserService.Models.Result
{
    /// <summary>
    ///文件
    /// </summary>
  public  class FileModel : IServiceResult
    {

        /// <summary>
        /// ID
        /// 
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>

        public Stream File { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
    }
}
