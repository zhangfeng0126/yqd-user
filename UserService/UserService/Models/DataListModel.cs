using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// ListModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class DataListModel<T>
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public IEnumerable<T> Data { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { get; set; }
    }
}
