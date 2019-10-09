using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{ /// <summary>
  /// 权限表
  /// </summary>
    [Table("menu")]
    public class Menu : IDataModel
    {
        /// <summary>
        /// 主键 ID
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        ///名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// key
        /// </summary>
        [Column("menu_key")]
        public string MenuKey { get; set; }

        /// <summary>
        /// icon
        /// </summary>
        [Column("icon")]
        public string  Icon { get; set; }

        /// <summary>
        /// 路由 Path
        /// </summary>
        [Column("router")]
        public string Router { get; set; }

        /// <summary>
        /// 页面地址 component
        /// </summary>
        [Column("view_url")]
        public string ViewUrl { get; set; }


        /// <summary>
        /// 文件夹
        /// </summary>
        [Column("folder")]
        public string Folder { get; set; }
        /// <summary>
        ///页面名称 Name
        /// </summary>
        [Column("view_name")]
        public string ViewName { get; set; }

        
        /// <summary>
        ///父级别ID
        /// </summary>
        [Column("parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        [Column("is_menu")]
        public int IsMenu { get; set; }
        /// <summary>
        /// 是否内页
        /// </summary>
        [Column("is_insidepages")]
        public bool IsInsidePages { get; set; }
        

        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort_id")]
        public int SortId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("add_time")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
    }
}
