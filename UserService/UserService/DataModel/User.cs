using MicroService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserService.DataModel
{  /// <summary>
   /// 用户 表
   /// </summary>
    [Table("user")]
    public class User : IDataModel
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [Column("login_name")]
        public string Login_Name { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Column("login_password")]
        public string Login_Password { get; set; }
        /// <summary>
        /// 头像(FileId)
        /// </summary>
        [Column("avatar")]
        public int Avatar { get; set; }
        /// <summary>
        /// 学校 ID
        /// </summary>
        [Column("school_id")]
        public int School_Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column("user_name")]
        public string User_Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column("gender")]
        public int Gender { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Column("phone")]
        public string Phone { get; set; }
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
        /// 客户端 ID
        /// </summary>
        [Column("client_id")]
        public string Client_Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 关联权限表
        /// </summary>
        
        public IList<UserAndRole> UserRole { get; set; }

        /// <summary>
        /// 关联学生表
        /// </summary>
        public IList<UserAndStudent> UserAndStudent { get; set; }

        /// <summary>
        /// 关联教师表
        /// </summary>
        public IList<UserAndTeacher> UserAndTeacher { get; set; }

    }
}
