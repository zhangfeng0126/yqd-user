using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{  /// <summary>
   /// 教师与班级管理表
   /// </summary>
    [Table("teacher_and_grade")]
    public class TeacherAndGreade : IDataModel
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
        [Column("greade_id")]
        public int GreadeId { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        [Column("teacher_id")]
        public int TeacherId { get; set; }

        /// <summary>
        /// 关联表
        /// </summary>
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime Update_Time { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("add_time")]
        public DateTime Add_Time { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }


  
    }
}
