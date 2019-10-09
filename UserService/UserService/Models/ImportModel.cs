using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 导入
    /// </summary>
   public class ImportModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public DataTable TableList { get; set; }
    }
}
