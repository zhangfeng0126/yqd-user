using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Request.Student
{
    /// <summary>
    /// 编辑学生信息
    /// </summary>
   public class EditStudentRequest : IServiceRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        [NotNull]
        public int Id { get; set; }
        /// <summary>
        /// 头像(FileId)
        /// </summary>
        public int  Avatar { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [NotNull]
        public string UserName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [NotNull]
        public int Gender { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [NotNull]
        public string Phone { get; set; }
      
        /// <summary>
        /// 学生号
        /// </summary>
        [NotNull]
        public string StudentNo { get; set; }
        /// <summary>
        /// 年纪ID
        /// </summary>
        [NotNull]
        public int GradeId { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        [NotNull]
        public int ClassId { get; set; }
    }
}
