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
    [Table("teacher_and_class")]
    public class TeacherAndClass : IDataModel
    {
        /// <summary>
        /// 管理ID
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        [Column("class_id")]
        public int ClassId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        [Column("grade_id")]
        public int GradeId { get; set; }


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
