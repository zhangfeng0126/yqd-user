using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Request.Student
{
    /// <summary>
    /// 添加学生
    /// </summary>
    public class AddStudentRequest: IServiceRequest
    {
    
        /// <summary>
        /// 头像FileId
        /// </summary>
        public int  Avatar { get; set; }
        /// <summary>
        /// 学校ID 
        /// </summary>
        [NotNull]
        public int SchoolId { get; set; }
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
        ///学籍号
        /// </summary>
        [NotNull]
        public string StudentNo { get; set; }
        /// <summary>
        /// 年级ID
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
