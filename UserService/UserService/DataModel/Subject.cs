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
    [Table("subject")]
    public class Subject : IDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        [Column("update_time")]
        public DateTime Update_Time { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        [Column("add_time")]
        public DateTime Add_Time { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

    }
}
