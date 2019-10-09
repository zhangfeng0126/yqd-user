using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Menu
{
    /// <summary>
    /// 菜单信息
    /// </summary>
    public class GetMenuByIdResult : IServiceResult
    {     /// <summary>
          /// 返回信息
          /// </summary>
        public MenuModel Data { get; set; }
    }
}
