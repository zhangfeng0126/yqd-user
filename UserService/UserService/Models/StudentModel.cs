using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 学生
    /// </summary>
  public  class StudentModel
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword { get; set; }
        /// <summary>
        /// 头像(FileId)
        /// </summary>
        public int  Avatar { get; set; }
        /// <summary>
        /// 学校ID 
        /// </summary>
        public int SchoolId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 接收ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 学生号
        /// </summary>
        public string StudentNo { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        public int GradeId { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// 年级Name
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// 班级Name
        /// </summary>
        public string ClassName { get; set; }
    }
}
