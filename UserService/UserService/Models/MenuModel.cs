using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
   public class MenuModel
    {
        /// <summary>
        /// 主键 ID
        /// </summary>

        public int Id { get; set; }

        /// <summary>
        ///名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// key
        /// </summary>

        public string MenuKey { get; set; }

        /// <summary>
        /// icon
        /// </summary>

        public string Icon { get; set; }

        /// <summary>
        /// 路由 Path
        /// </summary>

        public string Router { get; set; }

        /// <summary>
        /// 是否内页
        /// </summary>
     
        public bool IsInsidePages { get; set; }
        /// <summary>
        /// 文件夹
        /// </summary>
        public string Folder { get; set; }
        /// <summary>
        /// 页面地址 component
        /// </summary>

        public string ViewUrl { get; set; }

        /// <summary>
        ///页面名称 Name
        /// </summary>

        public string ViewName { get; set; }

        /// <summary>
        ///父级别ID
        /// </summary>

        public int ParentId { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>

        public int IsMenu { get; set; }

        /// <summary>
        /// 排序
        /// </summary>

        public int SortId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>

        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>

        public DateTime AddTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>

        public int Status { get; set; }



        /// <summary>
        /// 自己节点
        /// </summary>
        [NotWord]
        public List<MenuModel> Children { get; set; }
    }
}
