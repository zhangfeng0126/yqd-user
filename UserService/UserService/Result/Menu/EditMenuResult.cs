using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Result.Menu
{
    /// <summary>
    /// 编辑菜单
    /// </summary>
    public class EditMenuResult : IServiceResult
    {
        /// <summary>
        /// 返回修改结果
        /// </summary>
        public Boolean Result { get; set; }
    }
}
