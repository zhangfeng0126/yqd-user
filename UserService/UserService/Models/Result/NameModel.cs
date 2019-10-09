using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models.Result
{
    /// <summary>
    /// 获取名称
    /// </summary>
  public  class NameModel : IServiceResult
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Names { get; set; }
    }
}
