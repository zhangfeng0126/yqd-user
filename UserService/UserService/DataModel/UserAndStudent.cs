using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{  /// <summary>
   /// 学生中间表
   /// </summary>
    [Table("user_and_student")]
    public class UserAndStudent : IDataModel
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
        [Column("user_id")]
        public int UserId{ get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        [Column("student_id")]
        public int StudentId { get; set; }
        
        /// <summary>
        /// 关联表
        /// </summary>
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        /// <summary>
        /// 关联表
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
