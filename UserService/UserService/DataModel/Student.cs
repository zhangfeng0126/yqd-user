using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{
    /// <summary>
    /// 学生表
    /// </summary>
    [Table("student")]
    public class Student : IDataModel
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        [Column("student_no")]
        public string StudentNo { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        [Column("grade_id")]
        public int GradeId { get; set; }
        /// <summary>
        /// 班级 ID
        /// </summary>
        [Column("class_id")]
        public int ClassId { get; set; }


        /// <summary>
        /// 学生与用户关联表
        /// </summary>
        public IList<UserAndStudent> UserAndStudent{ get; set; }

    }
}
