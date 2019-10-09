using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using UserService.DataModel;
using UserService.Models;
using UserService.Request;
using UserService.Request.Menu;
using UserService.Request.Student;
using UserService.Request.Teacher;
using UserService.Request.User;
using UserService.Result.User;
using Xunit;

namespace UserService.Unit
{
    public class UserTest
    {
        UserService userService;
        readonly IConfiguration Configuration;
        public Common.Common common = new Common.Common();
        public UserTest()
        {
            userService = new UserService();

          


            //ConfigurationBuilder buidler = new ConfigurationBuilder();
            //buidler.AddJsonFile("appsettings.json");
  

            //Configuration = buidler.Build();
            userService.Configuration = Configuration;
            //common.Configuration = Configuration;

            MicroService.Database.ModelFactory.AddModel(typeof(Role));
            MicroService.Database.ModelFactory.AddModel(typeof(Student));
            MicroService.Database.ModelFactory.AddModel(typeof(Subject));
            MicroService.Database.ModelFactory.AddModel(typeof(Teacher));
            MicroService.Database.ModelFactory.AddModel(typeof(TeacherAndClass));
            MicroService.Database.ModelFactory.AddModel(typeof(TeacherAndGreade));
            MicroService.Database.ModelFactory.AddModel(typeof(User));
            MicroService.Database.ModelFactory.AddModel(typeof(UserAndRole));
            MicroService.Database.ModelFactory.AddModel(typeof(UserAndStudent));
            MicroService.Database.ModelFactory.AddModel(typeof(UserAndTeacher));
            MicroService.Database.ModelFactory.AddModel(typeof(Menu));
            MicroService.Database.ModelFactory.AddModel(typeof(RoleAndMenu));
            userService.ServiceCall = new MicroService.Services.Call.HttpServiceCall((m) => null, "http://10.0.104.56:8082/");
            userService.DataAdapter = new MicroService.Database.Mysql.MySqlDataAdapter("server=10.0.104.56;port=3306;database=zxx_kwdw;uid=kwdw;password=Yk@12345678;character Set=utf8;", "../logger.txt");

        }


        [Fact(DisplayName = "添加用户")]
        public void AddUserTest()
        {
            try
            {

                var a = userService.AddUser(new AddUserRequest()
                {
                    LoginName = "admin",
                    LoginPassword = "admin",
                    Avatar = 0,
                    SchoolId = 1,
                    Gender = 1,
                    Phone = "18680754937",
                    UserName = "测试",
                    RoleId = 1


                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "修改用户")]
        public void EditUserTest()
        {
            try
            {

                var a = userService.EditUser(new EditUserRequest()
                {
                    Id = 2,
                    LoginName = "admin",
                
                    Avatar = 0,
                    SchoolId = 1,
                    Gender = 1,
                    Phone = "18680754937",
                    UserName = "测试",
               

                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "删除用户")]
        public void DeleteUserTest()
        {
            try
            {

                var a = userService.DelUser(new DeleteUserRequest()
                {
                    Id = 2,
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }


        [Fact(DisplayName = "导出学生")]
        public void ExportStudentTest()
        {
            try
            {
                List<int> idlist = new List<int>();
                idlist.Add(97);

                var a = userService.ExportTeacher(new ExportRequest()
                {
                    SchoolId=1,
                    IdList=idlist,
                });
                //var filePath = @"D:\12345"+a.ContentType;
                //var stream = a.File;
                //byte[] bytes = new byte[stream.Length];

                //stream.Read(bytes, 0, bytes.Length);

                //// 设置当前流的位置为流的开始 

                //stream.Seek(0, SeekOrigin.Begin);

                //// 把 byte[] 写入文件 

                //FileStream fs = new FileStream(filePath, FileMode.Create);

                //BinaryWriter bw = new BinaryWriter(fs);

                //bw.Write(bytes);

                //bw.Close();

                //fs.Close();


            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "测试导入")]
        public void InputStudentTest()
        {
            try
            {
                string filePath = @"D:\teacher201904111848.xlsx";
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                
              var a = userService.ImportTeacher(new ImportRequest()
              {
                    SchoolId = 1,
                    FileId= 1230


              });

         

            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }


        [Fact(DisplayName = "根据ID获取用户信息")]
        public void GetUserTest()
        {
            try
            {

                var idList = new List<int>();
                idList.Add(54);
                idList.Add(55);
                idList.Add(56);
                var a = userService.GetUserListById(new GetUserListByIdRequest()
                {
                    IdList = idList
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }



        [Fact(DisplayName = "获取用户列表")]
        public void GetUserListTest()
        {
            try
            {

                var a = userService.GetUserList(new GetUserListRequest()
                {
                   
                    Page = 1,
                    Rows = 10,
                    Seach = "",
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }


        }

        [Fact(DisplayName = "添加学生")]
        public void AddStudentTest()
        {
            try
            {

                var a = userService.AddStudent(new AddStudentRequest()
                {
                    GradeId = 1,
                    ClassId = 3,
                    StudentNo = "20190329",
                 
                    Avatar = 0,
                    SchoolId = 1,
                    Gender = 1,
                    Phone = "18680754937",

                    UserName = "测试",



                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "修改学生")]
        public void EditStudentTest()
        {
            try
            {

                var a = userService.EditStudent(new EditStudentRequest()
                {
                    Id=3,
                    GradeId = 1,
                    ClassId = 3,
                    StudentNo = "20190329X",
                
                    Avatar = 0,
                    Gender = 1,
                    Phone = "18680754937",

                    UserName = "测试",



                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "添加教师")]
        public void AddTeacherTest()
        {
            try
            {
                var gradeList = new List<int>();
                var classList = new List<int>();
                gradeList.Add(1);
                classList.Add(3);
                classList.Add(4);
                classList.Add(5);
                classList.Add(6);

                var a = userService.AddTeacher(new AddTeacherRequest()
                {
                    //SubjectId = 2,
                    //ClassIdList = classList,
                    //GreadeIdList = gradeList,
                    //LoginName = "shuxuejiaoshi",
                    //LoginPassword = "shuxuejiaoshi",
                    //Avatar = 0,
                    //SchoolId = 1,
                    //Gender = 1,
                    //Phone = "18680754937",

                    //UserName = "测试",



                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "修改教师")]
        public void EditTeacherTest()
        {
            try
            {
                var gradeList = new List<int>();
                var classList = new List<ClassModel>();
                gradeList.Add(21);
                gradeList.Add(22);
                classList.Add(new ClassModel { Id=12,GradeId=21});
                classList.Add(new ClassModel { Id = 18, GradeId = 22 });

                var a = userService.EditTeacher(new EditTeacherRequest()
                {
                    Id = 13,
                    SubjectId = 2,
                    ClassList = classList,
                    GreadeIdList = gradeList,
                    LoginName = "shuxuejiaoshiX",
                    LoginPassword = "shuxuejiaoshiX",
                    Avatar = 0,
                    Gender = 1,
                    Phone = "18680754937",

                    UserName = "测试",



                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "根据ID获取学生信息")]
        public void GetStudentTest()
        {
            try
            {

                var a = userService.GetUserById(new GetUserByIdRequest()
                {
                    Id = 2
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "根据ID获取教师信息")]
        public void GetTeacherTest()
        {
            try
            {

                var a = userService.GetTeacherById(new GetGetMenuTreeRequest()
                {
                    Id = 13
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        [Fact(DisplayName = "获取学生列表")]
        public void GetStudentListTest()
        {
            try
            {

                
                var a = userService.GetStudentList(new GetStudentListRequest()
                {
                    Page = 1,
                    Rows = 10,
                    SchoolId=1,
                    ClassId=55,
                  
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }


        }
        
        [Fact(DisplayName = "获取教师列表")]
        public void GetTeacherListTest()
        {
            try
            {
                var list = new List<int>();
               
                var a = userService.GetTeacherList(new GetTeacherListRequest()
                {
                    Page = 1,
                    Rows = 10,
                    Seach = "",
                    SchoolId=1,
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
            
        }

        [Fact(DisplayName = "登录")]
        public void Login()
        {
            
            var num = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            try
            {

                var a = userService.Login(new LoginRequest()
                {
                  LoginName= "admin",
                  LoginPwd="admin"

                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }


        }

        [Fact(DisplayName = "获取树")]
        public void GetTree()
        {
            try
            {

               var a = userService.GetMenuLeftTree(new GetMenuTreeByRoleRequest()
                {
                    RoleId = 1
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
        [Fact(DisplayName = "添加权限")]
        public void RoleEmpower()
        {
            try
            {
                var list = new List<int>();
                list.Add(86);
                list.Add(90);

                list.Add(45);
                list.Add(46);
                list.Add(47);
                list.Add(48);
                list.Add(49);
                list.Add(52);
                list.Add(51);
                list.Add(43);
                list.Add(53);
                list.Add(54);
                list.Add(50);
                list.Add(42);
                list.Add(39);
                list.Add(40);
                list.Add(55);
                list.Add(38);
                list.Add(37);
                list.Add(36);
                list.Add(35);
                list.Add(34);
                list.Add(33);
                list.Add(32);
                list.Add(30);
                list.Add(29);
                list.Add(41);
                list.Add(56);

                var a = userService.RoleEmpower(new RoleEmpowerRequest()
                {
                    RoleId=1,
                    MenuId=list

                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }


        [Fact(DisplayName = "修改排序")]
        public void UpdateMenuSort()
        {
            try
            {
                var list = new List<int>();
                list.Add(86);
                list.Add(90);

                list.Add(45);
                list.Add(46);
                list.Add(47);
                list.Add(48);
                list.Add(49);
                list.Add(52);
                list.Add(51);
                list.Add(43);
                list.Add(53);
                list.Add(54);
                list.Add(50);
                list.Add(42);
                list.Add(39);
                list.Add(40);
                list.Add(55);
                list.Add(38);
                list.Add(37);
                list.Add(36);
                list.Add(35);
                list.Add(34);
                list.Add(33);
                list.Add(32);
                list.Add(30);
                list.Add(29);
                list.Add(41);
                list.Add(56);

                var a = userService.UpdateMenuSort(new UpdateMenuSortRequest()
                {
                    Id=3,
                    ParentId=1,
                    Type=1

                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
        

    }
}
