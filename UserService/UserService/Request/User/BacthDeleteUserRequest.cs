﻿using MicroService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Request.User
{
   /// <summary>
   /// 批量删除用户
   /// </summary>
   public class BacthDeleteUserRequest : IServiceRequest
    {
        /// <summary>
        /// 用户IDList
        /// </summary>
        [NotNull]
        public List<int> IdList { get; set; }
    }
}