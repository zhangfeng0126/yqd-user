using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
   public class MenuTreeModel
    {


        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// path
        /// </summary>

        public string path { get; set; }

        /// <summary>
        ///component: 
        /// </summary>

        public string component { get; set; }

        /// <summary>
        /// redirect
        /// </summary>

        public string redirect { get; set; }

        /// <summary>
        /// icon
        /// </summary>

        public string name { get; set; }

        /// <summary>
        /// 是否内页
        /// </summary>

        public bool hidden { get; set; }

        /// <summary>
        /// 文件夹
        /// </summary>
        public string folder { get; set; }
        /// <summary>
        /// 路由 Path
        /// </summary>

        public MataModel meta { get; set; }


        /// <summary>
        /// 自己节点
        /// </summary>
        [NotWord]
        public List<MenuTreeModel> children { get; set; }

       
    }
}
