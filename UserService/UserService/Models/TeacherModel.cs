using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 教师
    /// </summary>
   public class TeacherModel
    {


        
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 教师ID关联使用
        /// </summary>
        public int TeacherId { get; set; }
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
        public int Avatar { get; set; }
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
        /// 学科老师
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// 班级列表
        /// </summary>
        public List<ClassModel> ClassList { get; set; }

        /// <summary>
        /// 班级Id列表
        /// </summary>
        public List<int> ClassIdList { get; set; }
        /// <summary>
        /// 年级Id列表
        /// </summary>
        public List<int> GreadeIdList { get; set; }

       
    }
}
