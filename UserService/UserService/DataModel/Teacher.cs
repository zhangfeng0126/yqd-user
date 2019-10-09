using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{
    /// <summary>
    /// 教师表
    /// </summary>
     [Table("teacher")]
    public class Teacher : IDataModel
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
        [Column("subject_id")]
        public int SubjectId { get; set; }

        /// <summary>
        /// 教师与用户关联表
        /// </summary>
        public IList<UserAndTeacher> UserAndTeacher { get; set; }

        /// <summary>
        /// 教师与年级关联表
        /// </summary>
        public IList<TeacherAndGreade> TeacherAndGrade { get; set; }

        /// <summary>
        /// 教师与班级关联表
        /// </summary>
       public IList<TeacherAndClass> TeacherAndClass { get; set; }

    }
}
