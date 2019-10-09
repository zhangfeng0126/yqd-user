using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 教师
    /// </summary>
    public  class ExportTeacherModel
    {

       
       
        /// <summary>
        /// 姓名
        /// </summary>
     
        public string UserName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string  Gender { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
      
        public string LoginName { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        /// 
        public string GradeName { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 学科
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// 年级ID
        /// </summary>

        public List<int> GradeIdList { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>

        public List<int> ClassIdList { get; set; }
    }
}
