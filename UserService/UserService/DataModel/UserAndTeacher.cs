using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{  /// <summary>
   /// 教师表
   /// </summary>
    [Table("user_and_teacher")]
    public class UserAndTeacher : IDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 用户 ID
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }
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
        /// 关联表
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
