using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models.Enums
{
    /// <summary>
    /// 角色
    /// </summary>
   public enum RoleEnums
    {
        /// <summary>
       /// 超级管理员
       /// </summary>
        SysAdmin = 1,
        /// <summary>
        /// 学校管理员
        /// </summary>
        SchoolAdmin = 2,
        /// <summary>
        /// 教师
        /// </summary>
        Teacher = 3,

        /// <summary>
        /// 学生
        /// </summary>
        Student = 4,
    }
}
