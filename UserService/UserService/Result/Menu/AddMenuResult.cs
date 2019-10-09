using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Menu
{
    /// <summary>
    /// 添加菜单
    /// </summary>
    public class AddMenuResult : IServiceResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
