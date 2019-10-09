using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 导出学生
    /// </summary>
    public  class ExportStudentModel
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
        /// 学生号
        /// </summary>
      
        public string StudentNo { get; set; }
        /// <summary>
        /// 年纪ID
        /// </summary>
        /// 
        public string GradeName { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassName { get; set; }
    }
}
