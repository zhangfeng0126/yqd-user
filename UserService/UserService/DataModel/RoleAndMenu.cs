using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{  /// <summary>
   /// 用户权限
   /// </summary>
    [Table("role_and_menu")]
    public class RoleAndMenu : IDataModel
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("menu_id")]
        public int MenuId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        [Column("role_id")]
        public int RoleId { get; set; }

        /// <summary>
        /// 关联表
        /// </summary>
        [ForeignKey("MenuId")]
        public virtual User User { get; set; }

        /// <summary>
        /// 关联表
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}
