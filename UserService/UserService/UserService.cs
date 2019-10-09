using MicroService.Database.Interfaces;
using MicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.DataModel;
using UserService.Models.Enums;
using UserService.Request.User;
using UserService.Result.User;
using System.Linq;
using UserService.Models;
using UserService.Result.Student;
using UserService.Request.Student;
using UserService.Result.Teacher;
using UserService.Request.Teacher;
using UserService.Request;
using UserService.Result;
using UserService.Models.Result;
using UserService.Common.Models;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using UserService.Request.Role;
using UserService.Result.Role;
using UserService.Request.Menu;
using UserService.Result.Menu;

namespace UserService
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [Service(Router = "User")]
    public class UserService : BaseService
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public IDataAdapter DataAdapter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpRequest HttpRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpResponse HttpResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IServiceCall ServiceCall { get; set; }


        #region  调用接口

        /// <summary>
        /// 学校服务
        /// </summary>
        public const string SCHOOL = "School";

        /// <summary>
        /// 获取年级信息
        /// </summary>
        public const string GETGRADE = "GetGradeList";

        /// <summary>
        /// 获取年级信息
        /// </summary>
        public const string GETALLGRADE = "GetGradeAllList";

        /// <summary>
        /// 获取班级信息
        /// </summary>
        public const string GETCLASS = "GetClassList";
        /// <summary>
        /// 获取全部班级信息
        /// </summary>
        public const string GETALLCLASS = "GetClassAllList";
        /// <summary>
        /// 根据获取年级信息
        /// </summary>
        public const string GETCLASSINFO = "GetClassById";

        /// <summary>
        /// 根据ID获取班级信息
        /// </summary>
        public const string GETGRADEINFO = "GetGradeById";


        /// <summary>
        /// 根据获取班级列表
        /// </summary>
        public const string GETCLASSBYIDLIST = "GetClassByIdList";

        /// <summary>
        /// 根据ID获取年级列表
        /// </summary>
        public const string GETGRADEBYIDLIST = "GetGradeByIdList";

        /// <summary>
        /// 根据获取班级名称
        /// </summary>
        public const string GETCLASSNAME = "GetClassNameByIdList";

        /// <summary>
        /// 根据ID获取年级名称
        /// </summary>
        public const string GETGRADENAME = "GetGradeNameByIdList";

        /// <summary>
        /// 文件服务
        /// </summary>
        public const string FILE = "File";

        /// <summary>
        /// 获取文件
        /// </summary>
        public const string GETFILE = "GetFile";
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public Common.Common common = new Common.Common();
        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {
            try
            {
                DataAdapter?.Dispose();
                DataAdapter = null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string apiUrl
        {
            get
            {
                return Configuration.GetSection("Connection")["Api"];
            }
        }

        #region 用户信息管理

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("AddUser")]
        [ServiceRouter("AddUser")]
        [Request(RequestType.Body)]
        public AddUserResult AddUser(AddUserRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.LoginName))
                {
                    throw new ServiceException("登录名不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.LoginPassword))
                {
                    throw new ServiceException("登录密码");
                }

                var model = DataAdapter.Query<User>(s => s.Login_Name == request.LoginName && s.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model != null)
                {
                    throw new ServiceException("登录名已经存在，不允许重复添加");
                }


                User user = new User()
                {
                    Login_Name = request.LoginName,
                    Login_Password = request.LoginPassword,
                    Avatar = request.Avatar,
                    School_Id = request.SchoolId,
                    User_Name = request.UserName,
                    Gender = request.Gender,
                    Phone = request.Phone,
                    Update_Time = DateTime.Now,
                    Add_Time = DateTime.Now,
                    Client_Id = "",
                    Status = (int)StatusEnums.Normal

                };
                DataAdapter.Add(user);

                var role = new UserAndRole
                {
                    User = user,
                    RoleId = request.RoleId,
                };
                DataAdapter.Add(role);
                DataAdapter.SaveChange();
                return new AddUserResult { Id = user.Id };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }




        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("EditUser")]
        [ServiceRouter("EditUser")]
        [Request(RequestType.Body)]
        public EditUserResult EditUser(EditUserRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }

                var userModel = DataAdapter.Query<User>(q => q.Login_Name == request.LoginName && q.Status != (int)StatusEnums.Delete).FirstOrDefault();

                if (userModel != null && userModel.Id != model.Id)
                {

                    throw new ServiceException("登录名已经存在，不允许重复！");
                }

                model.Login_Name = request.LoginName;
                model.Avatar = request.Avatar;
                model.User_Name = request.UserName;
                model.Gender = request.Gender;
                model.Phone = request.Phone;
                model.Update_Time = DateTime.Now;

                var userRole = DataAdapter.Query<UserAndRole>(q => q.UserId == request.Id).FirstOrDefault();
                userRole.RoleId = request.RoleId;

                DataAdapter.SaveChange();
                return new EditUserResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 根据 ID获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetUserById")]
        [ServiceRouter("GetUserById")]
        [Request(RequestType.Body)]
        public GetUserByIdResult GetUserById(GetUserByIdRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                UserModel userInfo = new UserModel()
                {
                    LoginName = model.Login_Name,
                    LoginPassword = model.Login_Password,
                    Avatar = model.Avatar,
                    SchoolId = model.School_Id,
                    UserName = model.User_Name,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    UpdateTime = model.Update_Time,
                    AddTime = model.Add_Time,
                    ClientId = model.Client_Id,
                    Status = model.Status
                };
                var RoleId = DataAdapter.Query<UserAndRole>(q => q.UserId == request.Id).FirstOrDefault().RoleId;
                userInfo.RoleId = RoleId;
                return new GetUserByIdResult { Data = userInfo };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("DelUser")]
        [ServiceRouter("DelUser")]
        [Request(RequestType.Body)]
        public DeleteUserResult DelUser(DeleteUserRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                model.Status = (int)StatusEnums.Delete;
                DataAdapter.SaveChange();
                return new DeleteUserResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 批量删除用户、学生、教师
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("BacthDeleteUser")]
        [ServiceRouter("BacthDeleteUser")]
        [Request(RequestType.Body)]
        public BatchDeleteUserResult BacthDeleteUser(BacthDeleteUserRequest request)
        {
            try
            {
                if (request.IdList.Count > 0)
                {

                    foreach (var UserId in request.IdList)
                    {
                        var model = DataAdapter.Query<User>(q => q.Id == UserId && q.Status != (int)StatusEnums.Delete).FirstOrDefault();

                        model.Status = (int)StatusEnums.Delete;
                        DataAdapter.SaveChange();
                    }
                }

                return new BatchDeleteUserResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除用户、学生、教师
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("BacthEditUserStatus")]
        [ServiceRouter("BacthEditUserStatus")]
        [Request(RequestType.Body)]
        public BatchDeleteUserResult BacthEditUserStatus(BacthEditUserStatusRequest request)
        {
            try
            {
                if (request.IdList.Count > 0)
                {

                    foreach (var UserId in request.IdList)
                    {
                        var model = DataAdapter.Query<User>(q => q.Id == UserId && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                        model.Status = request.Status;
                        DataAdapter.SaveChange();
                    }
                }

                return new BatchDeleteUserResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("UpdatePwd")]
        [ServiceRouter("UpdatePwd")]
        [Request(RequestType.Body)]
        public UpdatePwdResult UpdatePwd(UpdatePwdRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                model.Login_Password = request.LoginPwd;
                model.Update_Time = DateTime.Now;
                DataAdapter.SaveChange();
                return new UpdatePwdResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetUserList")]
        [ServiceRouter("GetUserList")]
        [Request(RequestType.Body)]
        public GetUserListResult GetUserList(GetUserListRequest request)
        {
            try
            {
                int total = 0;
                var entity = DataAdapter.Query<User>(q => q.Status != (int)StatusEnums.Delete);

                if (!string.IsNullOrWhiteSpace(request.Seach))
                {
                    entity = entity.Where(i => i.User_Name.Contains(request.Seach) || i.Login_Name.Contains(request.Seach));
                }
                if (request.SchoolId > 0)
                {
                    entity = entity.Where(i => i.School_Id == request.SchoolId);
                }

                var userRole = DataAdapter.Query<UserAndRole>();
                var roleInfo = DataAdapter.Query<Role>();
                var userList = from u in entity
                               join ur in userRole
                               on u.Id equals ur.UserId
                               join r in roleInfo
                               on ur.RoleId equals r.Id
                               select new UserModel()
                               {
                                   Id = u.Id,
                                   LoginName = u.Login_Name,
                                   LoginPassword = u.Login_Password,
                                   Avatar = u.Avatar,
                                   SchoolId = u.School_Id,
                                   UserName = u.User_Name,
                                   Gender = u.Gender,
                                   Phone = u.Phone,
                                   UpdateTime = u.Update_Time,
                                   AddTime = u.Add_Time,
                                   ClientId = u.Client_Id,
                                   Status = u.Status,
                                   RoleId = ur.RoleId,
                                   RoleCode = r.RoleName,
                                   RoleName = r.Name

                               };

                userList = userList.Where(s => s.RoleId == (int)RoleEnums.SchoolAdmin || s.RoleId == (int)RoleEnums.SysAdmin);

                total = userList.Count();
                var Page = Convert.ToInt32(request.Page);
                var Rows = Convert.ToInt32(request.Rows);
                var temp = userList.OrderByDescending(i => i.Id).Skip((Page - 1) * Rows).Take(Rows).ToList();
                return new GetUserListResult { Data = temp, Total = total };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetUserListById")]
        [ServiceRouter("GetUserListById")]
        [Request(RequestType.Body)]
        public GetUserListByIdResult GetUserListById(GetUserListByIdRequest request)
        {
            try
            {
                int total = 0;
                var entity = DataAdapter.Query<User>(q => q.Status != (int)StatusEnums.Delete);

                if (request.IdList != null && request.IdList.Count > 0)
                {
                    entity = entity.Where(s => request.IdList.Contains(s.Id));
                }

                total = entity.Count();

                var temp = entity.OrderByDescending(i => i.Id).Select(s => new UserModel()
                {
                    Id = s.Id,
                    LoginName = s.Login_Name,
                    LoginPassword = s.Login_Password,
                    Avatar = s.Avatar,
                    SchoolId = s.School_Id,
                    UserName = s.User_Name,
                    Gender = s.Gender,
                    Phone = s.Phone,
                    UpdateTime = s.Update_Time,
                    AddTime = s.Add_Time,
                    ClientId = s.Client_Id,
                    Status = s.Status
                }).ToList();
                return new GetUserListByIdResult { Data = temp, Total = total };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        #endregion

        #region 学生信息管理

        /// <summary>
        /// 新增学生
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("AddStudent")]
        [ServiceRouter("AddStudent")]
        [Request(RequestType.Body)]
        public AddUserResult AddStudent(AddStudentRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.StudentNo))
                {
                    throw new ServiceException("学号不能为空");
                }


                var model = DataAdapter.Query<User>(s => s.Login_Name == request.StudentNo && s.Status != (int)StatusEnums.Delete).FirstOrDefault();


                if (model != null)
                {
                    throw new ServiceException("登录名已经存在，不允许重复添加");
                }


                User user = new User()
                {
                    Login_Name = request.StudentNo,
                    Login_Password = "123456",
                    Avatar = request.Avatar,
                    School_Id = request.SchoolId,
                    User_Name = request.UserName,
                    Gender = request.Gender,
                    Phone = request.Phone,
                    Update_Time = DateTime.Now,
                    Add_Time = DateTime.Now,
                    Client_Id = "",
                    Status = (int)StatusEnums.Normal

                };
                DataAdapter.Add(user);
                //添加权限
                var role = new UserAndRole
                {
                    User = user,
                    RoleId = (int)RoleEnums.Student,
                };
                DataAdapter.Add(role);

                //添加班级
                var student = new Student
                {
                    GradeId = request.GradeId,
                    ClassId = request.ClassId,
                    StudentNo = request.StudentNo,
                };
                DataAdapter.Add(student);

                var userStudent = new UserAndStudent
                {
                    Student = student,
                    User = user
                };

                DataAdapter.Add(userStudent);
                DataAdapter.SaveChange();
                return new AddUserResult { Id = user.Id };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }


        /// <summary>
        ///导出学生数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("ExportStudent")]
        [ServiceRouter("ExportStudent")]
        [Request(RequestType.Body)]
        public ExportResult ExportStudent(ExportRequest request)
        {
            try
            {

                var entity = DataAdapter.Query<User>(q => q.Status != (int)StatusEnums.Delete);

                if (!string.IsNullOrWhiteSpace(request.Seach))
                {
                    entity = entity.Where(i => i.User_Name.Contains(request.Seach) || i.Login_Name.Contains(request.Seach));
                }
                if (request.SchoolId > 0)
                {
                    entity = entity.Where(i => i.School_Id == request.SchoolId);
                }
                if (request.IdList != null && request.IdList.Count > 0)
                {
                    entity = entity.Where(s => request.IdList.Contains(s.Id));
                }


                //获取学生
                var studentModel = DataAdapter.Query<Student>();
                var userRole = DataAdapter.Query<UserAndRole>();
                var userSetudent = DataAdapter.Query<UserAndStudent>();

                var studentData = from u in entity
                                  join us in userSetudent
                                  on u.Id equals us.UserId
                                  join s in studentModel
                                  on us.StudentId equals s.Id
                                  join ur in userRole
                                  on u.Id equals ur.UserId
                                  where ur.RoleId == (int)RoleEnums.Student
                                  select new StudentModel
                                  {
                                      LoginName = u.Login_Name,
                                      LoginPassword = u.Login_Password,
                                      Avatar = u.Avatar,
                                      SchoolId = u.School_Id,
                                      UserName = u.User_Name,
                                      Gender = u.Gender,
                                      Phone = u.Phone,
                                      UpdateTime = u.Update_Time,
                                      AddTime = u.Add_Time,
                                      ClientId = u.Client_Id,
                                      Status = u.Status,
                                      GradeId = s.GradeId,
                                      ClassId = s.ClassId,
                                      StudentNo = s.StudentNo

                                  };


                if (request.GradeId > 0)
                {

                    studentData = studentData.Where(en => en.GradeId == request.GradeId);
                }

                if (request.ClassId > 0)
                {
                    studentData = studentData.Where(en => en.ClassId == request.ClassId);
                }

                if (request.GradeIdList != null && request.GradeIdList.Count > 0)
                {
                    studentData = studentData.Where(s => request.GradeIdList.Contains(s.GradeId));
                }
                if (request.ClassIdList != null && request.ClassIdList.Count > 0)
                {
                    studentData = studentData.Where(s => request.ClassIdList.Contains(s.ClassId));
                }

                var gradeIdList = studentData.Select(s => s.GradeId).ToList();
                var classIdList = studentData.Select(s => s.ClassId).ToList();
                var gradeList = new List<GradeInfo>();
                var classList = new List<ClassInfo>();
                var gradeData = ServiceCall.Call<GradeList>(SCHOOL, GETGRADEBYIDLIST, RequestType.Body, new { IdList = gradeIdList });
                if (gradeData.Code != 0)
                {
                    throw new ServiceException(gradeData.Message);
                }
                else
                {
                    if (gradeData.Data.Total > 0)
                    {
                        gradeList = gradeData.Data.Data;
                    }
                }

                var classData = ServiceCall.Call<ClassList>(SCHOOL, GETCLASSBYIDLIST, RequestType.Body, new { IdList = classIdList });
                if (classData.Code != 0)
                {
                    throw new ServiceException(classData.Message);
                }
                else
                {
                    if (classData.Data.Total > 0)
                    {
                        classList = classData.Data.Data;
                    }
                }
                var studentList = studentData.Select(s => new ExportStudentModel()
                {
                    StudentNo = s.StudentNo,
                    UserName = s.UserName,
                    Gender = s.Gender == 0 ? "女" : "男",
                    GradeName = gradeList.Where(en => en.Id == s.GradeId).FirstOrDefault().GradeName,
                    ClassName = classList.Where(en => en.Id == s.ClassId).FirstOrDefault().ClassName,

                }).ToList();


                var buffer = common.ExportStudent(studentList, "学生信息");

                var FileId = FileInfo(buffer, "学生信息.xlsx", ".xlsx");

                return new ExportResult { FileId = FileId };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 导入学生数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("ImportStudent")]
        [ServiceRouter("ImportStudent")]
        [Request(RequestType.Body)]
        public ImportResult ImportStudent(ImportRequest request)
        {
            try
            {

                var entity = DataAdapter.Query<User>(s => s.Status != (int)StatusEnums.Delete);
                var data = common.ReadExcel(request.FileId, apiUrl);
                var gradeList = new List<GradeInfo>();
                var classList = new List<ClassInfo>();

                var saveGradeName = "";

                var gradeData = ServiceCall.Call<GradeList>(SCHOOL, GETALLGRADE, RequestType.Body, new { request.SchoolId });
                if (gradeData.Code != 0)
                {
                    throw new ServiceException(gradeData.Message);
                }
                else
                {

                    gradeList = gradeData.Data.Data;

                }

                var index = 1;

                System.Data.DataView dv = new System.Data.DataView(data);
                if (dv.Count != dv.ToTable(true, "学籍号").Rows.Count)
                {
                    throw new ServiceException("学籍号有重复值，请修改后重试");
                }

                foreach (System.Data.DataRow dr in data.Rows)
                {

                    var studentNo = dr[0].ToString();
                    if (string.IsNullOrWhiteSpace(studentNo))
                    {
                        throw new ServiceException("第" + index + "行学籍号：" + studentNo + "为空");
                    }
                    if (ContainChinese(studentNo))
                    {
                        throw new ServiceException("第" + index + "行学籍号：" + studentNo + ";籍号中不允许出现中文");
                    }
                    var studentModel = entity.Where(i => i.Login_Name == studentNo).FirstOrDefault();
                    if (studentModel != null)
                    {

                        throw new ServiceException("第" + index + "行学籍号：" + studentNo + "已经存在");
                    }

                    var studentName = dr[1].ToString();
                    if (string.IsNullOrWhiteSpace(studentName))
                    {
                        throw new ServiceException("第" + index + "行姓名：" + studentName + "为空");
                    }

                    var studentSex = dr[2].ToString();
                    if (string.IsNullOrWhiteSpace(studentSex))
                    {
                        throw new ServiceException("第" + index + "行性别：" + studentSex + "为空");
                    }
                    var gradeName = dr[3].ToString();
                    if (string.IsNullOrWhiteSpace(gradeName))
                    {
                        throw new ServiceException("第" + index + "行年级：" + gradeName + "为空");
                    }
                    var gradeId = 0;
                    foreach (var item in gradeList)
                    {
                        if (item.GradeName == gradeName)
                        {
                            gradeId = item.Id;
                        }
                    }

                    if (gradeId == 0)
                    {
                        throw new ServiceException("第" + index + "行年级：" + gradeName + "不存在");
                    }


                    if (saveGradeName == "")
                    {
                        var result = ServiceCall.Call<ClassList>(SCHOOL, GETALLCLASS, RequestType.Body, new { gradeId });
                        if (result.Code != 0)
                        {
                            throw new ServiceException(result.Message);
                        }
                        else
                        {

                            classList = result.Data.Data;

                        }
                        saveGradeName = gradeName;
                    }
                    if (saveGradeName != gradeName)
                    {

                        var result = ServiceCall.Call<ClassList>(SCHOOL, GETALLCLASS, RequestType.Body, new { gradeId });
                        if (result.Code != 0)
                        {
                            throw new ServiceException(result.Message);
                        }
                        else
                        {

                            classList = result.Data.Data;

                        }
                        saveGradeName = gradeName;
                    }


                    var className = dr[4].ToString();
                    if (string.IsNullOrWhiteSpace(className))
                    {
                        throw new ServiceException("第" + index + "行班级：" + className + "为空");
                    }

                    var calssId = 0;
                    foreach (var item in classList)
                    {
                        if (item.ClassName == className)
                        {
                            calssId = item.Id;
                        }
                    }
                    if (calssId == 0)
                    {
                        throw new ServiceException("第" + index + "行班级：" + className + "不存在");
                    }
                    index += 1;

                }

                foreach (System.Data.DataRow dr in data.Rows)
                {

                    var studentNo = dr[0].ToString();
                    var studentName = dr[1].ToString();
                    var studentSex = dr[2].ToString();
                    var gradeName = dr[3].ToString();
                    var className = dr[4].ToString();
                    var Gender = 1;
                    if (studentSex == "女")
                    {

                        Gender = 0;
                    }


                    var user = new User()
                    {
                        Login_Name = studentNo,
                        Login_Password = "123456",
                        Avatar = 0,
                        School_Id = request.SchoolId,
                        User_Name = studentName,
                        Gender = Gender,
                        Phone = "",
                        Update_Time = DateTime.Now,
                        Add_Time = DateTime.Now,
                        Client_Id = "",
                        Status = (int)StatusEnums.Normal

                    };

                    DataAdapter.Add(user);
                    //添加权限
                    var role = new UserAndRole
                    {
                        User = user,
                        RoleId = (int)RoleEnums.Student,
                    };
                    DataAdapter.Add(role);
                    var gradeId = 0;
                    foreach (var item in gradeList)
                    {
                        if (item.GradeName == gradeName)
                        {
                            gradeId = item.Id;
                        }
                    }

                    #region 获取班级 ID

                    if (saveGradeName == "")
                    {
                        var result = ServiceCall.Call<ClassList>(SCHOOL, GETALLCLASS, RequestType.Body, new { gradeId });
                        if (result.Code != 0)
                        {
                            throw new ServiceException(result.Message);
                        }
                        else
                        {
                            if (result.Data.Total > 0)
                            {
                                classList = result.Data.Data;
                            }
                        }
                        saveGradeName = gradeName;
                    }
                    if (saveGradeName != gradeName)
                    {

                        var result = ServiceCall.Call<ClassList>(SCHOOL, GETALLCLASS, RequestType.Body, new { gradeId });
                        if (result.Code != 0)
                        {
                            throw new ServiceException(result.Message);
                        }
                        else
                        {
                            if (result.Data.Total > 0)
                            {
                                classList = result.Data.Data;
                            }
                        }
                        saveGradeName = gradeName;
                    }

                    var calssId = 0;
                    foreach (var item in classList)
                    {
                        if (item.ClassName == className)
                        {
                            calssId = item.Id;
                        }
                    }
                    #endregion
                    //添加班级
                    var student = new Student
                    {
                        GradeId = gradeId,
                        ClassId = calssId,
                        StudentNo = studentNo,
                    };
                    DataAdapter.Add(student);

                    var userStudent = new UserAndStudent
                    {
                        Student = student,
                        User = user
                    };

                    DataAdapter.Add(userStudent);

                    DataAdapter.SaveChange();


                }

                return new ImportResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 编辑学生信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("EditStudent")]
        [ServiceRouter("EditStudent")]
        [Request(RequestType.Body)]
        public EditStudentResult EditStudent(EditStudentRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }

                var userModel = DataAdapter.Query<User>(q => q.Login_Name == request.StudentNo && q.Status != (int)StatusEnums.Delete).FirstOrDefault();

                if (userModel != null && userModel.Id != model.Id)
                {

                    throw new ServiceException("学籍号已经存在，不允许重复！");
                }
                model.Login_Name = request.StudentNo;
                model.Avatar = request.Avatar;
                model.User_Name = request.UserName;
                model.Gender = request.Gender;
                model.Phone = request.Phone;
                model.Update_Time = DateTime.Now;

                var studentId = DataAdapter.Query<UserAndStudent>(q => q.UserId == request.Id).FirstOrDefault().StudentId;
                var studentModel = DataAdapter.Query<Student>(q => q.Id == studentId).FirstOrDefault();
                studentModel.StudentNo = request.StudentNo;
                studentModel.ClassId = request.ClassId;
                studentModel.GradeId = request.GradeId;

                DataAdapter.SaveChange();
                return new EditStudentResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 根据ID获取学生信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetStudentById")]
        [ServiceRouter("GetStudentById")]
        [Request(RequestType.Body)]
        public GetStudentByIdResult GetStudentById(GetStudentByIdRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                var studentInfo = new StudentModel()
                {
                    Id = model.Id,
                    LoginName = model.Login_Name,
                    LoginPassword = model.Login_Password,
                    Avatar = model.Avatar,
                    SchoolId = model.School_Id,
                    UserName = model.User_Name,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    UpdateTime = model.Update_Time,
                    AddTime = model.Add_Time,
                    ClientId = model.Client_Id,
                    Status = model.Status
                };
                var studentId = DataAdapter.Query<UserAndStudent>(q => q.UserId == request.Id).FirstOrDefault().StudentId;
                var studentModel = DataAdapter.Query<Student>(q => q.Id == studentId).FirstOrDefault();
                studentInfo.GradeId = studentModel.GradeId;
                studentInfo.ClassId = studentModel.ClassId;
                studentInfo.StudentNo = studentModel.StudentNo;
                return new GetStudentByIdResult { Data = studentInfo };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("DelStudent")]
        [ServiceRouter("DelStudent")]
        [Request(RequestType.Body)]
        public DeleteStudentResult DelStudent(DeleteStudentRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                model.Status = (int)StatusEnums.Delete;
                DataAdapter.SaveChange();
                return new DeleteStudentResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 查询学生列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetStudentList")]
        [ServiceRouter("GetStudentList")]
        [Request(RequestType.Body)]
        public GetStudentListResult GetStudentList(GetStudentListRequest request)
        {

            try
            {
                int total = 0;
                var entity = DataAdapter.Query<User>(q => q.Status != (int)StatusEnums.Delete);

                if (!string.IsNullOrWhiteSpace(request.Seach))
                {
                    entity = entity.Where(i => i.User_Name.Contains(request.Seach) || i.Login_Name.Contains(request.Seach));
                }
                if (request.SchoolId > 0)
                {
                    entity = entity.Where(i => i.School_Id == request.SchoolId);
                }
                if (request.IdList != null && request.IdList.Count > 0)
                {
                    entity = entity.Where(s => request.IdList.Contains(s.Id));
                }

                //获取学生
                var studentModel = DataAdapter.Query<Student>();
                var userRole = DataAdapter.Query<UserAndRole>();
                var userSetudent = DataAdapter.Query<UserAndStudent>();

                var studentList = from u in entity
                                  join us in userSetudent
                                  on u.Id equals us.UserId
                                  join s in studentModel
                                  on us.StudentId equals s.Id
                                  join ur in userRole
                                  on u.Id equals ur.UserId
                                  where ur.RoleId == (int)RoleEnums.Student
                                  select new StudentModel
                                  {
                                      Id = u.Id,
                                      LoginName = u.Login_Name,
                                      LoginPassword = u.Login_Password,
                                      Avatar = u.Avatar,
                                      SchoolId = u.School_Id,
                                      UserName = u.User_Name,
                                      Gender = u.Gender,
                                      Phone = u.Phone,
                                      UpdateTime = u.Update_Time,
                                      AddTime = u.Add_Time,
                                      ClientId = u.Client_Id,
                                      Status = u.Status,
                                      GradeId = s.GradeId,
                                      ClassId = s.ClassId,
                                      StudentNo = s.StudentNo

                                  };
                if (request.GradeId > 0)
                {

                    studentList = studentList.Where(en => en.GradeId == request.GradeId);
                }

                if (request.ClassId > 0)
                {
                    studentList = studentList.Where(en => en.ClassId == request.ClassId);
                }

                if (request.GradeIdList != null && request.GradeIdList.Count > 0)
                {
                    studentList = studentList.Where(s => request.GradeIdList.Contains(s.GradeId));
                }
                if (request.ClassIdList != null && request.ClassIdList.Count > 0)
                {
                    studentList = studentList.Where(s => request.ClassIdList.Contains(s.ClassId));
                }
                total = studentList.Count();
                var Page = Convert.ToInt32(request.Page);
                var Rows = Convert.ToInt32(request.Rows);
                if (request.IsPage == 0)
                {
                    studentList = studentList.OrderByDescending(i => i.Id).Skip((Page - 1) * Rows).Take(Rows);
                }
                var gradeIdList = studentList.Select(s => s.GradeId).ToList();
                var classIdList = studentList.Select(s => s.ClassId).ToList();
                var gradeList = new List<GradeInfo>();
                var classList = new List<ClassInfo>();
                var gradeData = ServiceCall.Call<GradeList>(SCHOOL, GETGRADEBYIDLIST, RequestType.Body, new { IdList = gradeIdList });
                if (gradeData.Code != 0)
                {
                    throw new ServiceException(gradeData.Message);
                }
                else
                {
                    if (gradeData.Data.Total > 0)
                    {
                        gradeList = gradeData.Data.Data;
                    }
                }

                var classData = ServiceCall.Call<ClassList>(SCHOOL, GETCLASSBYIDLIST, RequestType.Body, new { IdList = classIdList });
                if (classData.Code != 0)
                {
                    throw new ServiceException(classData.Message);
                }
                else
                {
                    if (classData.Data.Total > 0)
                    {
                        classList = classData.Data.Data;
                    }
                }


                var temp = studentList.Select(s => new StudentModel
                {
                    Id = s.Id,
                    LoginName = s.LoginName,
                    LoginPassword = s.LoginPassword,
                    Avatar = s.Avatar,
                    SchoolId = s.SchoolId,
                    UserName = s.UserName,
                    Gender = s.Gender,
                    Phone = s.Phone,
                    UpdateTime = s.UpdateTime,
                    AddTime = s.AddTime,
                    ClientId = s.ClientId,
                    Status = s.Status,
                    GradeId = s.GradeId,
                    ClassId = s.ClassId,
                    StudentNo = s.StudentNo,
                    ClassName = classList.Where(c => c.Id == s.ClassId).FirstOrDefault().ClassName,
                    GradeName = gradeList.Where(c => c.Id == s.GradeId).FirstOrDefault().GradeName,
                }).ToList();
                return new GetStudentListResult { Data = temp, Total = total };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// /获取学生列表Tree
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetStudentTreeList")]
        [ServiceRouter("GetStudentTreeList")]
        [Request(RequestType.Body)]
        public GetStudentTreeListResult GetStudentTreeList(GetStudentTreeListRequest request)
        {

            try
            {

                var entity = DataAdapter.Query<User>(q => q.Status != (int)StatusEnums.Delete);

                //获取学生
                var studentModel = DataAdapter.Query<Student>();
                var userRole = DataAdapter.Query<UserAndRole>();
                var userSetudent = DataAdapter.Query<UserAndStudent>();

                var studentList = from u in entity
                                  join us in userSetudent
                                  on u.Id equals us.UserId
                                  join s in studentModel
                                  on us.StudentId equals s.Id
                                  join ur in userRole
                                  on u.Id equals ur.UserId
                                  where ur.RoleId == (int)RoleEnums.Student
                                  select new StudentModel
                                  {
                                      Id = u.Id,
                                      LoginName = u.Login_Name,
                                      LoginPassword = u.Login_Password,
                                      Avatar = u.Avatar,
                                      SchoolId = u.School_Id,
                                      UserName = u.User_Name,
                                      Gender = u.Gender,
                                      Phone = u.Phone,
                                      UpdateTime = u.Update_Time,
                                      AddTime = u.Add_Time,
                                      ClientId = u.Client_Id,
                                      Status = u.Status,
                                      GradeId = s.GradeId,
                                      ClassId = s.ClassId,
                                      StudentNo = s.StudentNo

                                  };

                if (request.ClassIdList != null && request.ClassIdList.Count > 0)
                {
                    studentList = studentList.Where(s => request.ClassIdList.Contains(s.ClassId));
                }


                var gradeIdList = studentList.Select(s => s.GradeId).ToList();
                var classIdList = studentList.Select(s => s.ClassId).ToList();
                var gradeList = new List<GradeInfo>();
                var classList = new List<ClassInfo>();
                var gradeData = ServiceCall.Call<GradeList>(SCHOOL, GETGRADEBYIDLIST, RequestType.Body, new { IdList = gradeIdList });
                if (gradeData.Code != 0)
                {
                    throw new ServiceException(gradeData.Message);
                }
                else
                {
                    if (gradeData.Data.Total > 0)
                    {
                        gradeList = gradeData.Data.Data;
                    }
                }

                var classData = ServiceCall.Call<ClassList>(SCHOOL, GETCLASSBYIDLIST, RequestType.Body, new { IdList = classIdList });
                if (classData.Code != 0)
                {
                    throw new ServiceException(classData.Message);
                }
                else
                {
                    if (classData.Data.Total > 0)
                    {
                        classList = classData.Data.Data;
                    }
                }


                var temp = studentList.Select(s => new StudentTree
                {
                    Id = s.Id,
                    SchoolId = s.SchoolId,
                    Name = s.UserName,
                    GradeId = s.GradeId,
                    ClassId = s.ClassId,
                    Type = "student"

                }).ToList();
                return new GetStudentTreeListResult { Data = temp };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        #endregion

        #region 教师信息管理

        /// <summary>
        /// 添加教师
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("AddTeacher")]
        [ServiceRouter("AddTeacher")]
        [Request(RequestType.Body)]
        public AddTeacherResult AddTeacher(AddTeacherRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.LoginName))
                {
                    throw new ServiceException("登录名不能为空");
                }


                var model = DataAdapter.Query<User>(s => s.Login_Name == request.LoginName && s.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model != null)
                {
                    throw new ServiceException("登录名已经存在，不允许重复添加");
                }


                User user = new User()
                {
                    Login_Name = request.LoginName,
                    Login_Password = "123456",
                    Avatar = request.Avatar,
                    School_Id = request.SchoolId,
                    User_Name = request.UserName,
                    Gender = request.Gender,
                    Phone = request.Phone,
                    Update_Time = DateTime.Now,
                    Add_Time = DateTime.Now,
                    Client_Id = "",
                    Status = (int)StatusEnums.Normal

                };
                DataAdapter.Add(user);
                //添加权限
                var role = new UserAndRole
                {
                    User = user,
                    RoleId = (int)RoleEnums.Teacher,
                };
                DataAdapter.Add(role);

                //添加班级
                var teacher = new Teacher
                {
                    SubjectId = request.SubjectId,
                };
                DataAdapter.Add(teacher);

                var userTeacher = new UserAndTeacher
                {
                    Teacher = teacher,
                    User = user,
                };

                DataAdapter.Add(userTeacher);

                //添加年级
                var greadeList = new List<TeacherAndGreade>();

                foreach (var item in request.GreadeIdList)
                {

                    greadeList.Add(new TeacherAndGreade
                    {
                        Teacher = teacher,
                        GreadeId = item,
                        Add_Time = DateTime.Now,
                        Update_Time = DateTime.Now,
                        Status = (int)StatusEnums.Normal

                    });

                    //添加班级

                }
                DataAdapter.AddRange(greadeList);

                var classList = new List<TeacherAndClass>();
                foreach (var _item in request.ClassList)
                {

                    classList.Add(new TeacherAndClass
                    {
                        Teacher = teacher,
                        GradeId = _item.GradeId,
                        ClassId = _item.Id,
                        Add_Time = DateTime.Now,
                        Update_Time = DateTime.Now,
                        Status = (int)StatusEnums.Normal
                    });
                }
                DataAdapter.AddRange(classList);

                DataAdapter.SaveChange();
                return new AddTeacherResult { Id = user.Id };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }


        /// <summary>
        /// 导入教师
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("ImportTeacher")]
        [ServiceRouter("ImportTeacher")]
        [Request(RequestType.Body)]
        public ImportResult ImportTeacher(ImportRequest request)
        {
            try
            {

                var entity = DataAdapter.Query<User>(s => s.Status != (int)StatusEnums.Delete);
                var subjectEntity = DataAdapter.Query<Subject>(s => s.Status != (int)StatusEnums.Delete);

                var data = common.ReadExcel(request.FileId, apiUrl);
                var gradeList = new List<GradeInfo>();
                var classList = new List<ClassInfo>();




                var gradeData = ServiceCall.Call<GradeList>(SCHOOL, GETALLGRADE, RequestType.Body, new { request.SchoolId });
                if (gradeData.Code != 0)
                {
                    throw new ServiceException(gradeData.Message);
                }
                else
                {

                    gradeList = gradeData.Data.Data;

                }

                var index = 1;

                System.Data.DataView dv = new System.Data.DataView(data);

                if (dv.Count != dv.ToTable(true, "登录账号").Rows.Count)
                {
                    throw new ServiceException("登录账号有重复值，请修改后重试");
                }

                foreach (System.Data.DataRow dr in data.Rows)
                {

                    var loginName = dr[0].ToString();
                    if (string.IsNullOrWhiteSpace(loginName))
                    {
                        throw new ServiceException("第" + index + "行登录账号：" + loginName + "为空");
                    }
                    if (ContainChinese(loginName))
                    {

                        throw new ServiceException("第" + index + "行登录账号：" + loginName + ";登录账号中不允许出现中文");
                    }
                    var studentModel = entity.Where(i => i.Login_Name == loginName).FirstOrDefault();
                    if (studentModel != null)
                    {
                        throw new ServiceException("第" + index + "行登录账号：" + loginName + "已经存在");
                    }

                    var userName = dr[1].ToString();
                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        throw new ServiceException("第" + index + "行姓名：" + userName + "为空");
                    }

                    var userSex = dr[2].ToString();
                    if (string.IsNullOrWhiteSpace(userSex))
                    {
                        throw new ServiceException("第" + index + "行性别：" + userSex + "为空");
                    }

                    var subjectName = dr[3].ToString();
                    if (string.IsNullOrWhiteSpace(subjectName))
                    {
                        throw new ServiceException("第" + index + "行学科教师：" + subjectName + "为空");
                    }
                    var subjectModel = subjectEntity.Where(s => s.Name == subjectName).FirstOrDefault();

                    if (subjectModel == null)
                    {
                        throw new ServiceException("第" + index + "行学科教师：" + subjectName + "不存在");
                    }


                    var gradeName = dr[4].ToString();
                    if (string.IsNullOrWhiteSpace(gradeName))
                    {
                        throw new ServiceException("第" + index + "行年级：" + gradeName + "为空");

                    }

                    var gradeNameList = gradeName.Split('、');

                    if (gradeNameList.Length > 0)
                    {
                        var gradeIdList = new List<int>();

                        foreach (var name in gradeNameList)
                        {

                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                var gradeId = 0;
                                foreach (var item in gradeList)
                                {
                                    if (item.GradeName == name)
                                    {
                                        gradeId = item.Id;
                                        gradeIdList.Add(gradeId);
                                    }
                                }
                                if (gradeId == 0)
                                {
                                    throw new ServiceException("第" + index + "行年级：" + gradeName + "不存在");
                                }

                            }
                        }

                        if (gradeIdList.Count > 0)
                        {
                            var result = ServiceCall.Call<ClassList>(SCHOOL, GETALLCLASS, RequestType.Body, new { GradeIdList = gradeIdList });
                            if (result.Code != 0)
                            {
                                throw new ServiceException(result.Message);

                            }
                            else
                            {

                                classList = result.Data.Data;

                            }
                        }

                        var className = dr[5].ToString();
                        if (string.IsNullOrWhiteSpace(className))
                        {
                            throw new ServiceException("第" + index + "行班级：" + className + "为空");
                        }

                        var classNameList = className.Split('、');
                        if (classNameList.Length > 0)
                        {

                            foreach (var cName in classNameList)
                            {
                                if (!string.IsNullOrWhiteSpace(cName))
                                {

                                    var calssId = 0;
                                    foreach (var item in classList)
                                    {
                                        if (item.ClassName == cName)
                                        {
                                            calssId = item.Id;
                                        }
                                    }
                                    if (calssId == 0)
                                    {
                                        throw new ServiceException("第" + index + "行班级：" + cName + "不存在");
                                    }
                                }
                            }

                        }
                    }

                    index += 1;
                }

                foreach (System.Data.DataRow dr in data.Rows)
                {

                    var loginName = dr[0].ToString();
                    var userName = dr[1].ToString();
                    var userSex = dr[2].ToString();
                    var subjectName = dr[3].ToString();

                    var gradeName = dr[4].ToString();
                    var className = dr[5].ToString();
                    var Gender = 1;
                    if (userSex == "女")
                    {

                        Gender = 0;
                    }


                    var user = new User()
                    {
                        Login_Name = loginName,
                        Login_Password = "123456",
                        Avatar = 0,
                        School_Id = request.SchoolId,
                        User_Name = userName,
                        Gender = Gender,
                        Phone = "",
                        Update_Time = DateTime.Now,
                        Add_Time = DateTime.Now,
                        Client_Id = "",
                        Status = (int)StatusEnums.Normal

                    };

                    DataAdapter.Add(user);
                    //添加权限
                    var role = new UserAndRole
                    {
                        User = user,
                        RoleId = (int)RoleEnums.Teacher,
                    };
                    DataAdapter.Add(role);

                    var teacher = new Teacher
                    {
                        SubjectId = subjectEntity.Where(s => s.Name == subjectName).FirstOrDefault().Id,
                    };
                    DataAdapter.Add(teacher);

                    var userTeacher = new UserAndTeacher
                    {
                        Teacher = teacher,
                        User = user,
                    };

                    DataAdapter.Add(userTeacher);

                    //添加年级
                    var insertGreadeList = new List<TeacherAndGreade>();
                    var insertClassList = new List<TeacherAndClass>();

                    var gradeNameList = gradeName.Split("、");


                    var calssId = 0;//班级ID
                    var gradeIdList = new List<int>();

                    foreach (var name in gradeNameList)
                    {

                        if (!string.IsNullOrWhiteSpace(name))
                        {

                            foreach (var item in gradeList)
                            {
                                if (item.GradeName == name)
                                {
                                    gradeIdList.Add(item.Id);
                                    insertGreadeList.Add(new TeacherAndGreade
                                    {
                                        Teacher = teacher,
                                        GreadeId = item.Id,
                                        Add_Time = DateTime.Now,
                                        Update_Time = DateTime.Now,
                                        Status = (int)StatusEnums.Normal

                                    });
                                }
                            }
                        }
                    }
                    if (gradeIdList.Count > 0)
                    {

                        var result = ServiceCall.Call<ClassList>(SCHOOL, GETALLCLASS, RequestType.Body, new { GradeIdList = gradeIdList });
                        if (result.Code != 0)
                        {
                            throw new ServiceException(result.Message);

                        }
                        else
                        {
                            classList = result.Data.Data;

                        }

                    }
                    //获取班级名称List
                    var classNameList = className.Split('、');
                    if (classNameList.Length > 0)
                    {

                        foreach (var cName in classNameList)
                        {
                            if (!string.IsNullOrWhiteSpace(cName))
                            {

                                var GradeId = 0;
                                foreach (var item in classList)
                                {
                                    if (item.ClassName == cName)
                                    {
                                        calssId = item.Id;
                                        GradeId = item.GradeId;
                                    }
                                }
                                insertClassList.Add(new TeacherAndClass
                                {
                                    Teacher = teacher,
                                    ClassId = calssId,
                                    GradeId = GradeId,
                                    Add_Time = DateTime.Now,
                                    Update_Time = DateTime.Now,
                                    Status = (int)StatusEnums.Normal
                                });


                            }
                        }
                    }
                    DataAdapter.AddRange(insertGreadeList);
                    DataAdapter.AddRange(insertClassList);
                    DataAdapter.SaveChange();


                }

                return new ImportResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 编辑教师信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("EditTeacher")]
        [ServiceRouter("EditTeacher")]
        [Request(RequestType.Body)]
        public EditTeacherResult EditTeacher(EditTeacherRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }

                var userModel = DataAdapter.Query<User>(q => q.Login_Name == request.LoginName && q.Status != (int)StatusEnums.Delete).FirstOrDefault();

                if (userModel != null && userModel.Id != model.Id)
                {

                    throw new ServiceException("登录名已经存在，不允许重复！");
                }

                model.Login_Name = request.LoginName;
                model.Avatar = request.Avatar;
                model.User_Name = request.UserName;
                model.Gender = request.Gender;
                model.Phone = request.Phone;
                model.Update_Time = DateTime.Now;

                var teacherId = DataAdapter.Query<UserAndTeacher>(q => q.UserId == request.Id).FirstOrDefault().TeacherId;
                var teacherModel = DataAdapter.Query<Teacher>(q => q.Id == teacherId).FirstOrDefault();
                teacherModel.SubjectId = request.SubjectId;

                var entityClass = DataAdapter.Query<TeacherAndClass>(q => teacherId == q.TeacherId).ToList();
                foreach (var item in entityClass)
                {
                    var _class = entityClass.Where(en => en.Id == item.Id).FirstOrDefault();
                    _class.Status = -1;
                }

                var entityGreade = DataAdapter.Query<TeacherAndGreade>(q => teacherId == q.TeacherId).ToList();
                foreach (var item in entityGreade)
                {
                    var _grade = entityGreade.Where(en => en.Id == item.Id).FirstOrDefault();
                    _grade.Status = -1;
                }
                //添加年级
                var greadeList = new List<TeacherAndGreade>();
                var classList = new List<TeacherAndClass>();
                foreach (var item in request.GreadeIdList)
                {

                    greadeList.Add(new TeacherAndGreade
                    {
                        TeacherId = teacherId,
                        GreadeId = item,
                        Add_Time = DateTime.Now,
                        Update_Time = DateTime.Now,
                        Status = (int)StatusEnums.Normal
                    });

                }
                DataAdapter.AddRange(greadeList);

                foreach (var _item in request.ClassList)
                {

                    classList.Add(new TeacherAndClass
                    {
                        GradeId = _item.GradeId,
                        TeacherId = teacherId,
                        ClassId = _item.Id,
                        Add_Time = DateTime.Now,
                        Update_Time = DateTime.Now,
                        Status = (int)StatusEnums.Normal
                    });
                }
                DataAdapter.AddRange(classList);

                //添加班级



                DataAdapter.SaveChange();
                return new EditTeacherResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 根据ID获取教师信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetTeacherById")]
        [ServiceRouter("GetTeacherById")]
        [Request(RequestType.Body)]
        public GetTeacherByIdResult GetTeacherById(GetGetMenuTreeRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                var teacherInfo = new TeacherModel()
                {
                    Id = model.Id,
                    LoginName = model.Login_Name,
                    LoginPassword = model.Login_Password,
                    Avatar = model.Avatar,
                    SchoolId = model.School_Id,
                    UserName = model.User_Name,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    UpdateTime = model.Update_Time,
                    AddTime = model.Add_Time,
                    ClientId = model.Client_Id,
                    Status = model.Status
                };
                var teacherId = DataAdapter.Query<UserAndTeacher>(q => q.UserId == request.Id).FirstOrDefault().TeacherId;
                var teacherModel = DataAdapter.Query<Teacher>(q => q.Id == teacherId).FirstOrDefault();
                teacherInfo.SubjectId = teacherModel.SubjectId;

                var greadeList = DataAdapter.Query<TeacherAndGreade>(q => q.TeacherId == teacherId && q.Status == (int)StatusEnums.Normal).Select(s => s.GreadeId).ToList();
                teacherInfo.GreadeIdList = greadeList;
                var classList = DataAdapter.Query<TeacherAndClass>(q => q.TeacherId == teacherId && q.Status == (int)StatusEnums.Normal).Select(s => new ClassModel { Id = s.ClassId, GradeId = s.GradeId }).ToList();
                teacherInfo.ClassList = classList;


                return new GetTeacherByIdResult { Data = teacherInfo };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 删除教师信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("DelTeacher")]
        [ServiceRouter("DelTeacher")]
        [Request(RequestType.Body)]
        public DeleteTeacherResult DelTeacher(DeleteTeacherRequest request)
        {
            try
            {
                var model = DataAdapter.Query<User>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                model.Status = (int)StatusEnums.Delete;
                DataAdapter.SaveChange();
                return new DeleteTeacherResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 查询教师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetTeacherList")]
        [ServiceRouter("GetTeacherList")]
        [Request(RequestType.Body)]
        public GetTeacherListResult GetTeacherList(GetTeacherListRequest request)
        {
            try
            {
                int total = 0;
                var entity = DataAdapter.Query<User>(q => q.Status != (int)StatusEnums.Delete);

                if (!string.IsNullOrWhiteSpace(request.Seach))
                {
                    entity = entity.Where(i => i.User_Name.Contains(request.Seach) || i.Login_Name.Contains(request.Seach));
                }

                if (request.SchoolId > 0)
                {
                    entity = entity.Where(i => i.School_Id == request.SchoolId);
                }

                if (request.IdList != null && request.IdList.Count > 0)
                {
                    entity = entity.Where(i => request.IdList.Contains(i.Id));
                }
                //获取学生
                var teacherModel = DataAdapter.Query<Teacher>();
                var userRole = DataAdapter.Query<UserAndRole>();
                var userTeacher = DataAdapter.Query<UserAndTeacher>();

                var teacherList = from u in entity
                                  join ut in userTeacher
                                  on u.Id equals ut.UserId
                                  join t in teacherModel
                                  on ut.TeacherId equals t.Id
                                  join ur in userRole
                                  on u.Id equals ur.UserId
                                  where ur.RoleId == (int)RoleEnums.Teacher
                                  select new TeacherModel()
                                  {
                                      Id = u.Id,
                                      LoginName = u.Login_Name,
                                      LoginPassword = u.Login_Password,
                                      Avatar = u.Avatar,
                                      SchoolId = u.School_Id,
                                      UserName = u.User_Name,
                                      Gender = u.Gender,
                                      Phone = u.Phone,
                                      UpdateTime = u.Update_Time,
                                      AddTime = u.Add_Time,
                                      ClientId = u.Client_Id,
                                      Status = u.Status,
                                      SubjectId = t.SubjectId,
                                      TeacherId = t.Id,

                                  };
                total = teacherList.Count();
                var Page = Convert.ToInt32(request.Page);
                var Rows = Convert.ToInt32(request.Rows);
                if (request.IsPage == 0)
                {
                    teacherList = teacherList.OrderByDescending(i => i.Id).Skip((Page - 1) * Rows).Take(Rows);
                }
                var temp = teacherList.ToList();
                temp = temp.Select(s => new TeacherModel()
                {
                    Id = s.Id,
                    LoginName = s.LoginName,
                    LoginPassword = s.LoginPassword,
                    Avatar = s.Avatar,
                    SchoolId = s.SchoolId,
                    UserName = s.UserName,
                    Gender = s.Gender,
                    Phone = s.Phone,
                    UpdateTime = s.UpdateTime,
                    AddTime = s.AddTime,
                    ClientId = s.ClientId,
                    Status = s.Status,
                    SubjectId = s.SubjectId,
                    GreadeIdList = DataAdapter.Query<TeacherAndGreade>(q => q.TeacherId == s.TeacherId && q.Status == (int)StatusEnums.Normal).Select(q => q.GreadeId).ToList(),
                    ClassList = DataAdapter.Query<TeacherAndClass>(q => q.TeacherId == s.TeacherId && q.Status == (int)StatusEnums.Normal).Select(c => new ClassModel { Id = c.ClassId, GradeId = c.GradeId }).ToList(),

                }).ToList();
                return new GetTeacherListResult { Data = temp, Total = total };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 根据学生ID集合获取教师ID集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [ServiceMethod("GetTeacherListByStuIdList")]
        [ServiceRouter("GetTeacherListByStuIdList")]
        [Request(RequestType.Body)]
        public GetTeacherListByStuIdListResult GetTeacherListByStuIdList(GetTeacherListByStuIdListRequest request)
        {
            try
            {

                var entity = DataAdapter.Query<User>(q => q.Status != (int)StatusEnums.Delete);


                if (request.StudentIdList != null && request.StudentIdList.Count > 0)
                {
                    entity = entity.Where(i => request.StudentIdList.Contains(i.Id));
                }
                //获取学生
                var studentModel = DataAdapter.Query<Student>();
                var userSetudent = DataAdapter.Query<UserAndStudent>();

                var studentList = from u in entity
                                  join us in userSetudent
                                  on u.Id equals us.UserId
                                  join s in studentModel
                                  on us.StudentId equals s.Id
                                  select new StudentModel
                                  {
                                      Id = u.Id,
                                      LoginName = u.Login_Name,
                                      GradeId = s.GradeId,
                                      ClassId = s.ClassId,
                                      StudentNo = s.StudentNo

                                  };


                var classIdList = studentList.Select(s => s.ClassId).ToList();

                var teacherIdList = new List<int>();
                var userIdList = new List<int>();
                if (classIdList.Count > 0)
                {

                    var teacherTemp = DataAdapter.Query<TeacherAndClass>(q => q.Status != (int)StatusEnums.Delete);
                    teacherTemp = teacherTemp.Where(i => classIdList.Contains(i.ClassId));
                    teacherIdList = teacherTemp.Select(s => s.TeacherId).ToList();
                    var userTemp = DataAdapter.Query<UserAndTeacher>(i => teacherIdList.Contains(i.TeacherId));
                    userIdList = userTemp.Select(s => s.UserId).ToList();
                }

                return new GetTeacherListByStuIdListResult { TeacherIdList = userIdList };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 导出教师信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("ExportTeacher")]
        [ServiceRouter("ExportTeacher")]
        [Request(RequestType.Body)]
        public ExportResult ExportTeacher(ExportRequest request)
        {
            try
            {

                var entity = DataAdapter.Query<User>(q => q.Status != (int)StatusEnums.Delete);

                if (!string.IsNullOrWhiteSpace(request.Seach))
                {
                    entity = entity.Where(i => i.User_Name.Contains(request.Seach) || i.Login_Name.Contains(request.Seach));
                }

                if (request.SchoolId > 0)
                {
                    entity = entity.Where(i => i.School_Id == request.SchoolId);
                }

                if (request.IdList != null && request.IdList.Count > 0)
                {
                    entity = entity.Where(i => request.IdList.Contains(i.Id));
                }

                var teacherModel = DataAdapter.Query<Teacher>();
                var userRole = DataAdapter.Query<UserAndRole>();
                var userTeacher = DataAdapter.Query<UserAndTeacher>();

                var teacherData = from u in entity
                                  join ut in userTeacher
                                  on u.Id equals ut.UserId
                                  join t in teacherModel
                                  on ut.TeacherId equals t.Id
                                  join ur in userRole
                                  on u.Id equals ur.UserId
                                  where ur.RoleId == (int)RoleEnums.Teacher
                                  select new TeacherModel()
                                  {
                                      Id = u.Id,
                                      LoginName = u.Login_Name,
                                      LoginPassword = u.Login_Password,
                                      Avatar = u.Avatar,
                                      SchoolId = u.School_Id,
                                      UserName = u.User_Name,
                                      Gender = u.Gender,
                                      Phone = u.Phone,
                                      UpdateTime = u.Update_Time,
                                      AddTime = u.Add_Time,
                                      ClientId = u.Client_Id,
                                      Status = u.Status,
                                      SubjectId = t.SubjectId,
                                      TeacherId = t.Id,
                                  };

                var temp = teacherData.ToList();
                temp = temp.Select(s => new TeacherModel()
                {

                    LoginName = s.LoginName,
                    LoginPassword = s.LoginPassword,
                    Avatar = s.Avatar,
                    SchoolId = s.SchoolId,
                    UserName = s.UserName,
                    Gender = s.Gender,
                    Phone = s.Phone,
                    UpdateTime = s.UpdateTime,
                    AddTime = s.AddTime,
                    ClientId = s.ClientId,
                    Status = s.Status,
                    SubjectId = s.SubjectId,
                    GreadeIdList = DataAdapter.Query<TeacherAndGreade>(q => q.TeacherId == s.TeacherId && q.Status == (int)StatusEnums.Normal).Select(q => q.GreadeId).ToList(),
                    ClassIdList = DataAdapter.Query<TeacherAndClass>(q => q.TeacherId == s.TeacherId && q.Status == (int)StatusEnums.Normal).Select(q => q.ClassId).ToList()

                }).ToList();


                var teacherList = temp.Select(s => new ExportTeacherModel()
                {
                    LoginName = s.LoginName,
                    UserName = s.UserName,
                    Gender = s.Gender == 0 ? "女" : "男",
                    SubjectName = DataAdapter.Query<Subject>(q => q.Id == s.SubjectId).FirstOrDefault().Name,
                    GradeName = ServiceCall.Call<NameModel>(SCHOOL, GETGRADENAME, RequestType.Body, new { IdList = s.GreadeIdList }).Data.Names,
                    ClassName = ServiceCall.Call<NameModel>(SCHOOL, GETCLASSNAME, RequestType.Body, new { IdList = s.ClassIdList }).Data.Names,

                }).ToList();

                var buffer = common.ExportTeacher(teacherList, "教师信息");
                var FileId = FileInfo(buffer, "教师信息.xlsx", ".xlsx");


                return new ExportResult { FileId = FileId };

            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 教师学科列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetSubjectList")]
        [ServiceRouter("GetSubjectList")]
        [Request(RequestType.Body)]
        public GetSubjectListResult GetSubjectList(GetSubjectListRequest request)
        {
            try
            {
                int total = 0;
                var entity = DataAdapter.Query<Subject>(q => q.Status != (int)StatusEnums.Delete);
                var temp = entity.OrderBy(i => i.Id).Select(s => new SubjectModel
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList();
                return new GetSubjectListResult { Data = temp, Total = total };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("Login")]
        [ServiceRouter("Login")]
        [Request(RequestType.Body)]
        public LoginResult Login(LoginRequest request)
        {
            try
            {
                var Success = true;
                var model = DataAdapter.Query<User>(q => q.Login_Name == request.LoginName && q.Login_Password == request.LoginPwd && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    Success = false;
                    throw new ServiceException("登录名或者密码错误，请重试");

                }
                if (model.Status == 1)
                {
                    Success = false;
                    throw new ServiceException("账号禁用！");

                }
                var RoleIdList = DataAdapter.Query<UserAndRole>(q => q.UserId == model.Id).Select(s => s.RoleId);
                var roleId= DataAdapter.Query<UserAndRole>(q => q.UserId == model.Id).FirstOrDefault().RoleId;
                var roleList = DataAdapter.Query<Role>(q => RoleIdList.Contains(q.Id)).Select(s => new RoleModel
                {
                    Id = s.Id,
                    RoleName = s.RoleName
                }).ToList();
               
                UserModel userInfo = new UserModel()
                {
                    Id = model.Id,
                    LoginName = model.Login_Name,
                    LoginPassword = model.Login_Password,
                    Avatar = model.Avatar,
                    SchoolId = model.School_Id,
                    UserName = model.User_Name,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    UpdateTime = model.Update_Time,
                    AddTime = model.Add_Time,
                    ClientId = model.Client_Id,
                    Status = model.Status,
                    RoleList = roleList

                };
                
                //获取权限
                var entityList = DataAdapter.Query<Menu>(q => q.Status != (int)StatusEnums.Delete);
                var menuList = entityList.Where(s=>s.IsMenu==1);
                var entityMenu = DataAdapter.Query<RoleAndMenu>();
                var tempList = from en in menuList
                               join m in entityMenu
                               on en.Id equals m.MenuId
                               where m.RoleId == roleId
                               select new MenuModel()
                               {
                                   Id = en.Id,
                                   Name = en.Name,
                                   MenuKey = en.MenuKey,
                                   Router = en.MenuKey,
                                   ParentId = en.ParentId,
                                   SortId = en.SortId,
                                   UpdateTime = en.UpdateTime,
                                   AddTime = en.AddTime,
                                   IsMenu = en.IsMenu,
                                   ViewUrl = en.ViewUrl,
                                   ViewName = en.ViewName,
                                   IsInsidePages=en.IsInsidePages,
                                   Folder=en.Folder,
                                   Icon = en.Icon,
                                   Status = en.Status


                               };


                var temp  = tempList.Distinct().ToList();
                var menuTree = CreateLeftTree(0, temp);
                userInfo.MenuTree = menuTree;

                var btnList = entityList.Where(s => s.IsMenu == 0);
                var strBtn = new List<string>();
                strBtn = btnList.Select(s => s.MenuKey).ToList();
                userInfo.btnPermissions = strBtn;
                return new LoginResult { Data = userInfo, Success = Success };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }



        /// <summary>
        /// 文件信息
        /// </summary>
        /// <param name="buf">二进制</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public int FileInfo(byte[] buf, string name, string type)
        {
            var FileId = 0;
            var base64Str = Convert.ToBase64String(buf);
            var result = ServiceCall.Call<Models.Result.FileModel>("File", "AddFileBase64", RequestType.Body,
                       new { Buffer = base64Str, FileName = name, });
            if (result.Code != 0)
            {
                throw new ServiceException(result.Message);
            }
            return FileId = result.Data.FileID;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ContainChinese(string input)
        {
            string pattern = "[\u4e00-\u9fbb]";
            return Regex.IsMatch(input, pattern);
        }

        #region 角色管理

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("AddRole")]
        [ServiceRouter("AddRole")]
        [Request(RequestType.Body)]
        public AddRoleResult AddRole(AddRoleRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    throw new ServiceException("角色名称不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.RoleName))
                {
                    throw new ServiceException("角色代码不能为空");
                }

                var model = DataAdapter.Query<Role>(s => (s.Name == request.Name || s.RoleName == request.RoleName) && s.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model != null)
                {
                    throw new ServiceException("角色名称或者角色代码已经存在，不允许重复添加");
                }


                Role role = new Role()
                {
                    Name = request.Name,
                    RoleName = request.RoleName,
                    Status = (int)StatusEnums.Normal

                };
                DataAdapter.Add(role);
                DataAdapter.SaveChange();
                return new AddRoleResult { Id = role.Id };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("EditRole")]
        [ServiceRouter("EditRole")]
        [Request(RequestType.Body)]
        public EditRoleResult EditRole(EditRoleRequest request)
        {
            try
            {
                var model = DataAdapter.Query<Role>(s => s.Id == request.Id && s.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或已经删除！");
                }

                var roleModel = DataAdapter.Query<Role>(q => (q.Name == request.Name || q.RoleName == request.RoleName) && q.Status != (int)StatusEnums.Delete).FirstOrDefault();

                if (roleModel != null && roleModel.Id != model.Id)
                {

                    throw new ServiceException("角色名称或者角色代码已经存在，不允许重复！");
                }

                model.Name = request.Name;
                model.RoleName = request.RoleName;
                model.Status = request.Status;



                DataAdapter.SaveChange();
                return new EditRoleResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 根据ID获取角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetRoleById")]
        [ServiceRouter("GetRoleById")]
        [Request(RequestType.Body)]
        public GetRoleByIdResult GetRoleById(GetRoleByIdRequest request)
        {
            try
            {
                var model = DataAdapter.Query<Role>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                RoleModel roleInfo = new RoleModel()
                {
                    Name = model.Name,
                    RoleName = model.RoleName,
                    Id = model.Id,

                    Status = model.Status
                };
                return new GetRoleByIdResult { Data = roleInfo };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("DelRole")]
        [ServiceRouter("DelRole")]
        [Request(RequestType.Body)]
        public DeleteRoleResult DelRole(DeleteRoleRequest request)
        {
            try
            {
                var roleList = DataAdapter.Query<Role>(q => request.Id.Contains(q.Id) && q.Status != (int)StatusEnums.Delete).ToList();
                if (roleList == null || roleList.Count < 0)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                foreach (var item in roleList)
                {

                    item.Status = (int)StatusEnums.Delete;
                }
                DataAdapter.SaveChange();
                return new DeleteRoleResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 批量修改角色状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("UpdateRoleStatus")]
        [ServiceRouter("UpdateRoleStatus")]
        [Request(RequestType.Body)]
        public DeleteRoleResult UpdateRoleStatus(UpdateRoleStatusRequest request)
        {
            try
            {
                var roleList = DataAdapter.Query<Role>(q => request.Id.Contains(q.Id) && q.Status != (int)StatusEnums.Delete).ToList();
                if (roleList == null || roleList.Count < 0)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                foreach (var item in roleList)
                {

                    item.Status = request.Status;
                }
                DataAdapter.SaveChange();
                return new DeleteRoleResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }


        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetRoleList")]
        [ServiceRouter("GetRoleList")]
        [Request(RequestType.Body)]
        public GetRoleListResult GetRoleList(GetRoleListRequest request)
        {
            try
            {
                int total = 0;
                var entity = DataAdapter.Query<Role>(q => q.Status != (int)StatusEnums.Delete);

                if (!string.IsNullOrWhiteSpace(request.Seach))
                {
                    entity = entity.Where(i => i.Name.Contains(request.Seach) || i.RoleName.Contains(request.Seach));
                }

                var roleList = entity.Select(s => new RoleModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    RoleName = s.RoleName,
                    Status = s.Status
                });

                total = entity.Count();
                if (request.IsPage == 0)
                {
                    var Page = Convert.ToInt32(request.Page);
                    var Rows = Convert.ToInt32(request.Rows);
                    var temp = roleList.OrderByDescending(i => i.Id).Skip((Page - 1) * Rows).Take(Rows).ToList();
                    return new GetRoleListResult { Data = temp, Total = total };
                }
                else
                {
                    var Page = Convert.ToInt32(request.Page);
                    var Rows = Convert.ToInt32(request.Rows);
                    var temp = roleList.OrderByDescending(i => i.Id).ToList();
                    return new GetRoleListResult { Data = temp, Total = total };
                }

            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        #endregion


        #region 菜单管理

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("AddMenu")]
        [ServiceRouter("AddMenu")]
        [Request(RequestType.Body)]
        public AddMenuResult AddMenu(AddMenuRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    throw new ServiceException("菜单标题不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.MenuKey))
                {
                    throw new ServiceException("菜单编码不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Router))
                {
                    throw new ServiceException("路由不能为空");
                }
                var model = DataAdapter.Query<Menu>(s => (s.Name == request.Name || s.MenuKey == request.MenuKey) && s.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model != null)
                {
                    throw new ServiceException("菜单标题或者菜单编码已经存在，不允许重复添加");
                }


                Menu role = new Menu()
                {
                    Name = request.Name,
                    MenuKey = request.MenuKey,
                    Router = request.MenuKey,
                    ParentId = request.ParentId,
                    SortId = request.SortId,
                    UpdateTime = DateTime.Now,
                    AddTime = DateTime.Now,
                    IsMenu = request.IsMenu,
                    ViewName = request.ViewName,
                    ViewUrl = request.ViewUrl,
                    Icon = request.Icon,
                    Folder = request.Folder,
                    IsInsidePages=request.IsInsidePages,
                    Status = (int)StatusEnums.Normal

                };
                DataAdapter.Add(role);
                DataAdapter.SaveChange();
                return new AddMenuResult { Id = role.Id };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("EditMenu")]
        [ServiceRouter("EditMenu")]
        [Request(RequestType.Body)]
        public EditMenuResult EditMenu(EditMenuRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    throw new ServiceException("菜单标题不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.MenuKey))
                {
                    throw new ServiceException("菜单编码不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Router))
                {
                    throw new ServiceException("路由不能为空");
                }

                var model = DataAdapter.Query<Menu>(s => s.Id == request.Id && s.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或已经删除");
                }

                var menuModel = DataAdapter.Query<Menu>(s => (s.Name == request.Name || s.MenuKey == request.MenuKey) && s.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (menuModel != null && menuModel.Id != model.Id)
                {
                    throw new ServiceException("菜单标题或者菜单编码已经存在");
                }

                model.Name = request.Name;
                model.MenuKey = request.MenuKey;
                model.Router = request.MenuKey;
                model.ParentId = request.ParentId;
                model.SortId = request.SortId;
                model.UpdateTime = DateTime.Now;
                model.IsMenu = request.IsMenu;
                model.Icon = request.Icon;
                model.ViewName = request.ViewName;
                model.ViewUrl = request.ViewUrl;
                model.IsInsidePages = request.IsInsidePages;
                model.Folder = request.Folder;


                DataAdapter.SaveChange();
                return new EditMenuResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 根据ID获取菜单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetMenuById")]
        [ServiceRouter("GetMenuById")]
        [Request(RequestType.Body)]
        public GetMenuByIdResult GetMenuById(GetRoleByIdRequest request)
        {
            try
            {
                var model = DataAdapter.Query<Menu>(q => q.Id == request.Id && q.Status != (int)StatusEnums.Delete).FirstOrDefault();
                if (model == null)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                MenuModel menuInfo = new MenuModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    MenuKey = model.MenuKey,
                    Router = model.MenuKey,
                    ParentId = model.ParentId,
                    SortId = model.SortId,
                    UpdateTime = model.UpdateTime,
                    AddTime = model.AddTime,
                    IsMenu = model.IsMenu,
                    ViewUrl=model.ViewUrl,
                    ViewName=model.ViewName,
                    IsInsidePages=model.IsInsidePages,
                    Folder=model.Folder,
                    Icon = model.Icon,
                    Status = model.Status,

                };
                return new GetMenuByIdResult { Data = menuInfo };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("DelMenu")]
        [ServiceRouter("DelMenu")]
        [Request(RequestType.Body)]
        public DeleteRoleResult DelMenu(DeleteRoleRequest request)
        {
            try
            {
                var roleList = DataAdapter.Query<Menu>(q => request.Id.Contains(q.Id) && q.Status != (int)StatusEnums.Delete).ToList();
                if (roleList == null || roleList.Count < 0)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                foreach (var item in roleList)
                {

                    item.Status = (int)StatusEnums.Delete;
                }
                DataAdapter.SaveChange();
                return new DeleteRoleResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 批量修改菜单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("UpdateMenuStatus")]
        [ServiceRouter("UpdateMenuStatus")]
        [Request(RequestType.Body)]
        public DeleteMenuResult UpdateMenuStatus(UpdateMenuStatusRequest request)
        {
            try
            {
                var roleList = DataAdapter.Query<Menu>(q => request.Id.Contains(q.Id) && q.Status != (int)StatusEnums.Delete).ToList();
                if (roleList == null || roleList.Count < 0)
                {
                    throw new ServiceException("数据不存在或者已被删除");
                }
                foreach (var item in roleList)
                {

                    item.Status = request.Status;
                }
                DataAdapter.SaveChange();
                return new DeleteMenuResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 调整排序
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("UpdateMenuSort")]
        [ServiceRouter("UpdateMenuSort")]
        [Request(RequestType.Body)]
        public DeleteMenuResult UpdateMenuSort(UpdateMenuSortRequest request)
        {
            try
            {



                var menuInfo = DataAdapter.Query<Menu>(c => c.Id == request.Id).FirstOrDefault();
                if (menuInfo == null)
                {
                    throw new ServiceException("菜单不存在");
                }

                Menu currItem = null;
                Menu preItem = null;
                Menu nextItem = null;
                var list = DataAdapter.Query<Menu>(c => c.ParentId == menuInfo.ParentId).OrderBy(o => o.SortId).ToList();
                DataAdapter.Transaction(ta =>
                {
                    for (int i = 0; i < list.Count(); i++)
                    {
                        currItem = list[i];
                        if (currItem.Id == request.Id)
                        {
                            if (request.Type == 1)
                            {
                                if (i > 0)
                                {
                                    preItem = list[i - 1];
                                    var currItemSort = preItem.SortId;
                                    var preItemSort = currItem.SortId;
                                    currItem.SortId = currItemSort;
                                    preItem.SortId = preItemSort;

                                }
                            }
                            else
                            {
                                if (i < list.Count() - 1)
                                {
                                    nextItem = list[i + 1];

                                    var currItemSort = nextItem.SortId;
                                    var nextItemSort = currItem.SortId;

                                    currItem.SortId = currItemSort;
                                    nextItem.SortId = nextItemSort;

                                }
                            }
                            break;
                        }
                    }
                });


                DataAdapter.SaveChange();
                return new DeleteMenuResult { Result = true };
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetMenuList")]
        [ServiceRouter("GetMenuList")]
        [Request(RequestType.Body)]
        public GetMenuListResult GetMenuList(GetMenuListRequest request)
        {
            try
            {
                int total = 0;
                var entity = DataAdapter.Query<Menu>(q => q.Status != (int)StatusEnums.Delete);

                if (!string.IsNullOrWhiteSpace(request.Seach))
                {
                    entity = entity.Where(i => i.Name.Contains(request.Seach) || i.MenuKey.Contains(request.Seach));
                }
                if (request.IsMenu > 0)
                {
                    entity = entity.Where(i => i.IsMenu == request.IsMenu);
                }
                if (request.ParentId > -1)
                {
                    entity = entity.Where(i => i.ParentId == request.ParentId);
                }
                var menuList = entity.Select(s => new MenuModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    MenuKey = s.MenuKey,
                    Router = s.MenuKey,
                    ParentId = s.ParentId,
                    SortId = s.SortId,
                    UpdateTime = s.UpdateTime,
                    AddTime = s.AddTime,
                    IsMenu = s.IsMenu,
                    ViewName=s.ViewName,
                    ViewUrl=s.ViewUrl,
                    IsInsidePages=s.IsInsidePages,
                    Folder=s.Folder,
                    Icon = s.Icon,
                    Status = s.Status,
                });

                total = entity.Count();
                if (request.IsPage == 0)
                {
                    var Page = Convert.ToInt32(request.Page);
                    var Rows = Convert.ToInt32(request.Rows);
                    var temp = menuList.OrderByDescending(i => i.Id).Skip((Page - 1) * Rows).Take(Rows).ToList();
                    return new GetMenuListResult { Data = temp, Total = total };
                }
                else
                {
                    var Page = Convert.ToInt32(request.Page);
                    var Rows = Convert.ToInt32(request.Rows);
                    var temp = menuList.OrderByDescending(i => i.Id).ToList();
                    return new GetMenuListResult { Data = temp, Total = total };
                }

            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        /// <summary>
        /// 查询菜单树
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetMenuTree")]
        [ServiceRouter("GetMenuTree")]
        [Request(RequestType.Body)]
        public GetMenuListResult GetMenuTree(GetMenuTreeRequest request)
        {
            try
            {
                int total = 0;
                var entity = DataAdapter.Query<Menu>(q => q.Status != (int)StatusEnums.Delete);

                if (!string.IsNullOrWhiteSpace(request.Seach))
                {
                    entity = entity.Where(i => i.Name.Contains(request.Seach) || i.MenuKey.Contains(request.Seach));
                }
                if (request.IsMenu > 0)
                {
                    entity = entity.Where(i => i.IsMenu == request.IsMenu);
                }
                if (request.ParentId > -1)
                {
                    entity = entity.Where(i => i.ParentId == request.ParentId);
                }

                var menuList = entity.ToList();

                var temp = CreateTree(0, menuList);

                return new GetMenuListResult { Data = temp, Total = total };


            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }


        /// <summary>
        /// 根据获取菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetMenuLeftTree")]
        [ServiceRouter("GetMenuLeftTree")]
        [Request(RequestType.Body)]
        public GetMenuTreeResult GetMenuLeftTree(GetMenuTreeByRoleRequest request)
        {
            try
            {
                int total = 0;
                var entityList = DataAdapter.Query<Menu>(q => q.Status != (int)StatusEnums.Delete&&q.IsMenu==1&& q.IsInsidePages==false);
                var entityMenu = DataAdapter.Query<RoleAndMenu>();


                var tempList = from en in entityList
                               join m in entityMenu
                               on en.Id equals m.MenuId
                               where m.RoleId == request.RoleId
                               select new MenuModel()
                               {
                                   Id = en.Id,
                                   Name = en.Name,
                                   MenuKey = en.MenuKey,
                                   Router = en.MenuKey,
                                   ParentId = en.ParentId,
                                   SortId = en.SortId,
                                   UpdateTime = en.UpdateTime,
                                   AddTime = en.AddTime,
                                   IsMenu = en.IsMenu,
                                   ViewUrl = en.ViewUrl,
                                   IsInsidePages=en.IsInsidePages,
                                   ViewName = en.ViewName,
                                   Folder = en.Folder,
                                Icon = en.Icon,
                                Status = en.Status

                            };


                var menuList = tempList.Distinct().ToList();

                var temp = CreateLeftTree(0, menuList);

                return new GetMenuTreeResult { Data = temp, Total = total };


            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }


        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("GetMenuByRole")]
        [ServiceRouter("GetMenuByRole")]
        [Request(RequestType.Body)]
        public GetMenuListResult GetMenuByRole(GetMenuTreeByRoleRequest request)
        {
            try
            {
                int total = 0;
                var entityList = DataAdapter.Query<Menu>(q => q.Status != (int)StatusEnums.Delete);
                var entityMenu = DataAdapter.Query<RoleAndMenu>();


                var tempList = from en in entityList
                               join m in entityMenu
                               on en.Id equals m.MenuId
                               where m.RoleId == request.RoleId
                               select new Menu()
                               {
                                   Id = en.Id,
                                   Name = en.Name,
                                   MenuKey = en.MenuKey,
                                   Router = en.MenuKey,
                                   ParentId = en.ParentId,
                                   SortId = en.SortId,
                                   UpdateTime = en.UpdateTime,
                                   AddTime = en.AddTime,
                                   IsMenu = en.IsMenu,
                                   ViewUrl = en.ViewUrl,
                                   ViewName = en.ViewName,
                                   IsInsidePages = en.IsInsidePages,
                                   Folder = en.Folder,
                                   Icon = en.Icon,
                                   Status = en.Status

                               };


                var menuList = tempList.Distinct().ToList();

                var temp = CreateTree(0, menuList);

                return new GetMenuListResult { Data = temp, Total = total };


            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static List<MenuModel> CreateTree(int ParentId, List<Menu> dataList)
        {
            var list = dataList.Where(s => s.ParentId == ParentId).OrderBy(s => s.SortId);
            List<MenuModel> tmpList = new List<MenuModel>();
            foreach (var item in list)
            {
                var model = new MenuModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.MenuKey = item.MenuKey;
                model.Router = item.MenuKey;
                model.ParentId = item.ParentId;
                model.SortId = item.SortId;
                model.UpdateTime = item.UpdateTime;
                model.AddTime = item.AddTime;
                model.IsMenu = item.IsMenu;
                model.ViewUrl = item.ViewUrl;
                model.ViewName = item.ViewName;
                model.Folder = item.Folder;
                model.IsInsidePages = item.IsInsidePages;
                model.Icon = item.Icon;
                model.Status = item.Status;
                List<MenuModel> tmpChildren = CreateTree(item.Id, dataList);
                if (tmpChildren.Count != 0)
                {
                    model.Children = tmpChildren;
                }

                tmpList.Add(model);
            }
            return tmpList;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static List<MenuTreeModel> CreateLeftTree(int ParentId, List<MenuModel> dataList)
        {
            var list = dataList.Where(s => s.ParentId == ParentId).OrderBy(s => s.SortId);
            List<MenuTreeModel> tmpList = new List<MenuTreeModel>();
            foreach (var item in list)
            {
                var model = new MenuTreeModel();
                model.id = item.Id;
                model.name = item.ViewName;
                model.path = item.Router;
                model.redirect = "";
                model.hidden = item.IsInsidePages;
                model.component = item.ViewUrl;
                var mateModel = new MataModel();
                mateModel.title = item.Name;
                mateModel.icon = item.Icon;
                model.folder = item.Folder;
                model.meta = mateModel;

           
                List<MenuTreeModel> tmpChildren = CreateLeftTree(item.Id, dataList);
                if (tmpChildren.Count != 0)
                {
                    model.children = tmpChildren;
                }

                tmpList.Add(model);
            }
            return tmpList;
        }


        /// <summary>
        /// 给角色分配权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceMethod("RoleEmpower")]
        [ServiceRouter("RoleEmpower")]
        [Request(RequestType.Body)]
        public DeleteMenuResult RoleEmpower(RoleEmpowerRequest request)
        {
            try
            {

                var entity = DataAdapter.Query<RoleAndMenu>(q => q.RoleId == request.RoleId);
                if (entity.Count() > 0)
                {
                    DataAdapter.RemoveRange(entity);
                }

                var insertList = new List<RoleAndMenu>();

                foreach (var item in request.MenuId)
                {

                    insertList.Add(new RoleAndMenu()
                    {
                        MenuId = item,
                        RoleId = request.RoleId
                    });
                }

                DataAdapter.AddRange(insertList);
                DataAdapter.SaveChange();
                return new DeleteMenuResult { Result = true };

            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }


        #endregion
    }
}
