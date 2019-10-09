using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    /// <summary>
    /// 权限
    /// </summary>
  public  class RoleModel
    {
        /// <summary>
        /// Id
        /// </summary>
     
        public int Id { get; set; }
        /// <summary>
        /// 名称
       
        public string Name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
      
        public int Status { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string RoleName { get; set; }

       
    }
}
