using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Request.User
{
   /// <summary>
   /// 编辑用户
   /// </summary>
   public class EditUserRequest: IServiceRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        [NotNull]
        public int Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [NotNull]
        public string LoginName { get; set; }

        /// <summary>
        /// 头像
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
        /// 角色ID
        /// </summary>
        [NotNull]
        public int RoleId { get; set; }



    }
}
