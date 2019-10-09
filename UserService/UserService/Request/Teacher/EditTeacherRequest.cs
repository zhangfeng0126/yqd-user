﻿using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Models;

namespace UserService.Request.Teacher
{
    /// <summary>
    /// 编辑教师信息
    /// </summary>
   public class EditTeacherRequest: IServiceRequest
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
        /// 登录密码
        /// </summary>
    
        public string LoginPassword { get; set; }
        /// <summary>
        /// 头像
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
        /// 学科老师
        /// </summary>
        [NotNull]
        public int SubjectId { get; set; }

        /// <summary>
        /// 班级列表
        /// </summary>
        [NotNull]
        public List<ClassModel> ClassList { get; set; }

        /// <summary>
        /// 年级Id列表
        /// </summary>
        [NotNull]
        public List<int> GreadeIdList { get; set; }
    }
}
