using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Result.Menu
{
    /// <summary>
    /// 返回菜单列表
    /// </summary>
    public class GetMenuTreeResult : DataListModel<MenuTreeModel>, IServiceResult
    {
    }
}
