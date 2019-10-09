using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{
    /// <summary>
    /// 权限表
    /// </summary>
    [Table("role")]
    public class Role : IDataModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Column("code")]

        public string RoleName {get;set;}
    }
}
