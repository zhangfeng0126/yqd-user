using MicroService.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UserService.Request.Student
{
    /// <summary>
    /// 导入
    /// </summary>
    public class ImportRequest : IServiceRequest
    {
        /// <summary>
        ///文件ID
        /// </summary>
        [NotNull]
        public int  FileId { get; set; }

        /// <summary>
        /// 学校 ID
        /// </summary>
        [NotNull]
        public int SchoolId { get; set; }

        
    }
}
