using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.IO;
using System.Text;
using UserService.Models;
using MicroService.Services;
using UserService.Common.Models;
using System.Net;

namespace UserService.Common
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class Common
    {
        
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; set; }
    


        #region  调用接口

      

        /// <summary>
        /// 文件服务
        /// </summary>
        public const string FILE = "File";

        /// <summary>
        /// 获取文件
        /// </summary>
        public const string GETFILE = "GetFile";
        #endregion


        private string DirPath
        {
            get
            {
                return Configuration.GetSection("Path")["FilePath"];
            }
        }

        private string DefaultPath
        {
            get
            {
                return Configuration.GetSection("Path")["DefaultPath"];
            }
        }




        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="FileId"></param>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public DataTable ReadExcel(int FileId,string apiUrl)
        {
            var serviceAddress = apiUrl + FILE + "/" + GETFILE + "?fileID=" + FileId;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(serviceAddress);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/octet-stream";

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream responseStream = httpResponse.GetResponseStream();
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[64 * 1024];
            int s;
            while ((s = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                stream.Write(buffer, 0, s);
            }

            DataTable dt = null;
            try
            {
                byte[] bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);

                // 设置当前流的位置为流的开始 

                stream.Seek(0, SeekOrigin.Begin);


                //打开文件


                XSSFWorkbook workbook = new XSSFWorkbook(stream);

                ISheet sheet = null;
                IRow row = null;

                for (int i = 0; i < workbook.Count; i++)
                {
                    dt = new DataTable();
                    //dt.TableName = "table" + i.ToString();

                    //获取 sheet 表
                    sheet = workbook.GetSheetAt(i);

                    //起始行索引
                    int rowIndex = sheet.FirstRowNum;

                    //获取行数
                    int rowCount = sheet.LastRowNum;
                    //获取第一行
                    IRow firstRow = sheet.GetRow(rowIndex);

                    //起始列索引
                    int colIndex = firstRow.FirstCellNum;

                    //获取列数
                    int colCount = firstRow.LastCellNum;

                    DataColumn dc = null;

                    //获取列名
                    for (int j = colIndex; j < colCount; j++)
                    {
                        dc = new DataColumn(firstRow.GetCell(j).StringCellValue);
                        dt.Columns.Add(dc);
                    }

                    //跳过第一行列名
                    rowIndex++;

                    for (int k = rowIndex; k <= rowCount; k++)
                    {
                        DataRow dr = dt.NewRow();
                        row = sheet.GetRow(k);

                        for (int l = colIndex; l < colCount; l++)
                        {
                            if (row.GetCell(l) == null)
                            {
                                continue;
                            }
                            dr[l] = row.GetCell(l).StringCellValue;
                        }

                        dt.Rows.Add(dr);
                    }


                }

                sheet = null;
                workbook = null;
              
                stream.Close();

            }
            catch (Exception ex)
            {
                stream.Close();
                throw new ServiceException("导入数据读取失败，请检查模板中数据是否为文本格式！");
               
               
            }
            return dt;

        }
        /// <summary>
        /// 导出学生
        /// </summary>
        /// <param name="list"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public byte[] ExportStudent(List<ExportStudentModel> list, string title)
        {


            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(title);
            DataTable tblDatas = new DataTable("Datas");
            //设置边框样式
            ICellStyle style0 = workbook.CreateCellStyle();
            BasicStyles(style0);
            Title(5, sheet, title, workbook);
            List<string> TableHead = new List<string>
            {
                "学籍号",
                "姓名",
                "性别",
                "年级",
                "班级"
            };
            DataList(tblDatas, TableHead, sheet, workbook, 1);
            //数据
            for (int i = 0; i < list.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 2);
                ICell cell = row1.CreateCell(0);
                cell.SetCellValue(i + 1);
                cell = row1.CreateCell(1);
                cell.SetCellValue(list[i].StudentNo);
                cell = row1.CreateCell(2);
                cell.SetCellValue(list[i].UserName);
                cell = row1.CreateCell(3);
                cell.SetCellValue(list[i].Gender);
                cell = row1.CreateCell(4);
                cell.SetCellValue(list[i].GradeName);
                cell = row1.CreateCell(5);
                cell.SetCellValue(list[i].ClassName);

                TraverseStyle(5, row1, style0);
            }
            sheet.SetColumnWidth(0, 6 * 256);
            sheet.SetColumnWidth(1, 34 * 256);
            ColumnWidth(2, 5, 15, sheet);
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();
            return buf;


        }
        /// <summary>
        /// 导出教师
        /// </summary>
        /// <param name="list"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public byte[] ExportTeacher(List<ExportTeacherModel> list, string title)
        {

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(title);
            DataTable tblDatas = new DataTable("Datas");
            //设置边框样式
            ICellStyle style0 = workbook.CreateCellStyle();
            BasicStyles(style0);
            Title(6, sheet, title, workbook);
            List<string> TableHead = new List<string>
            {
                "帐号",
                "姓名",
                "性别",
                "学科",
                "年级",
                "班级"
            };
            DataList(tblDatas, TableHead, sheet, workbook, 1);
            //数据
            for (int i = 0; i < list.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 2);
                ICell cell = row1.CreateCell(0);
                cell.SetCellValue(i + 1);
                cell = row1.CreateCell(1);
                cell.SetCellValue(list[i].LoginName);
                cell = row1.CreateCell(2);
                cell.SetCellValue(list[i].UserName);
                cell = row1.CreateCell(3);
                cell.SetCellValue(list[i].Gender);
                cell = row1.CreateCell(4);
                cell.SetCellValue(list[i].SubjectName);
                cell = row1.CreateCell(5);
                cell.SetCellValue(list[i].GradeName);
                cell = row1.CreateCell(6);
                cell.SetCellValue(list[i].ClassName);

                TraverseStyle(6, row1, style0);
            }
            sheet.SetColumnWidth(0, 6 * 256);
            sheet.SetColumnWidth(1, 34 * 256);
            ColumnWidth(2, 6, 15, sheet);
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();
            return buf;
        }

        /// <summary>
        /// 基本样式设置
        /// </summary>
        /// <param name="cellStyle"></param>
        /// <returns></returns>
        public void BasicStyles(ICellStyle cellStyle)
        {
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            //自动换行
            cellStyle.WrapText = true;
            //设置水平对齐
            cellStyle.Alignment = HorizontalAlignment.Center;
            //设置垂直对齐
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public void Title(int mergeCount, ISheet sheet, string name, XSSFWorkbook xssfworkbook, bool IsFont = true, int rowCount = 0)
        {
            //在工作表中：建立行，参数为行号，从0计
            IRow rowT = sheet.CreateRow(rowCount);
            //在行中：建立单元格，参数为列号，从0计
            ICell cellT = rowT.CreateCell(0);
            //设置单元格内容
            //cellT.SetCellValue(name);

            //设置单元格的高度
            rowT.Height = 30 * 20;
            //设置单元格的宽度
            sheet.SetColumnWidth(0, 30 * 256);

            ICellStyle styleT = xssfworkbook.CreateCellStyle();
            if (IsFont)
            {
                IFont font = xssfworkbook.CreateFont();
                font.FontHeightInPoints = 16;
                font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                //font.FontName = "標楷體";
                styleT.SetFont(font);//HEAD 样式
            }

            //styleT.BorderBottom = BorderStyle.Thin;
            //styleT.BorderLeft = BorderStyle.Thin;
            //styleT.BorderRight = BorderStyle.Thin;
            //styleT.BorderTop = BorderStyle.Thin;

            styleT.Alignment = HorizontalAlignment.Center;
            //设置垂直对齐
            styleT.VerticalAlignment = VerticalAlignment.Center;
            for (int i = 0; i < mergeCount + 1; i++)
            {
                ICell cell = rowT.CreateCell(i);
                cell.SetCellValue(name);
                cell.CellStyle = styleT;
            }
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            //sheet.AddMergedRegion(new CellRangeAddress(2, 4, 3, 3));
            sheet.AddMergedRegion(new CellRangeAddress(rowCount, rowCount, 0, mergeCount));
        }
        /// <summary>
        /// 表头
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="TableHead"></param>
        /// <param name="sheet"></param>
        /// <param name="xssfworkbook"></param>
        /// <param name="mergeCount"></param>
        public void DataList(DataTable dataTable, List<string> TableHead, ISheet sheet, XSSFWorkbook xssfworkbook, int mergeCount)
        {
            //设置边框样式
            ICellStyle style = xssfworkbook.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            //自动换行
            style.WrapText = true;
            //设置水平对齐
            style.Alignment = HorizontalAlignment.Center;
            //设置垂直对齐
            style.VerticalAlignment = VerticalAlignment.Center;
            IFont font = xssfworkbook.CreateFont();
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            font.FontHeightInPoints = 12;
            style.SetFont(font);

            DataColumn dc = null;
            //赋值给dc，是便于对每一个datacolumn的操作
            dc = dataTable.Columns.Add("序号", Type.GetType("System.Int32"));
            dc.AutoIncrement = true;//自动增加
            dc.AutoIncrementSeed = 1;//起始为1
            dc.AutoIncrementStep = 1;//步长为1
            dc.AllowDBNull = false;//
            foreach (var item in TableHead)
            {
                dc = dataTable.Columns.Add(item, Type.GetType("System.String"));
            }
            //表头
            IRow row = sheet.CreateRow(mergeCount);
            row.Height = 30 * 20;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dataTable.Columns[i].ColumnName);
                //自动适应宽度
                sheet.AutoSizeColumn(i);
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i));
                cell.CellStyle = style;
            }
        }
        /// <summary>
        /// 给每个单元添加样式
        /// </summary>
        /// <param name="num"></param>
        /// <param name="row"></param>
        /// <param name="cellStyle"></param>
        public void TraverseStyle(int num, IRow row, ICellStyle cellStyle)
        {
            for (int i = 0; i <= num; i++)
            {
                row.GetCell(i).CellStyle = cellStyle;
            }
        }
        /// <summary>
        /// 设置每列宽度
        /// </summary>
        /// <param name="startNum">开始列</param>
        /// <param name="endNum">结束列</param>
        /// <param name="value">宽度值</param>
        /// <param name="sheet"></param>
        public void ColumnWidth(int startNum, int endNum, int value, ISheet sheet)
        {
            for (int i = startNum; i <= endNum; i++)
            {
                sheet.SetColumnWidth(i, value * 256);
            }
        }
        /// <summary>
        /// 保存Excel文件返回文件ID
        /// </summary>
        /// <param name="xssfworkbook"></param>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ExportUrl(XSSFWorkbook xssfworkbook, string name, string format)
        {
            string url = "";
            /*不能使用如下方法生成Excel，因为在xssfworkbook.Write(stream);操作后会关闭流，导致报错【不能操作已关闭的流】*/ ////转为字节数组
            MemoryStream stream = new MemoryStream();
            xssfworkbook.Write(stream);
            var buf = stream.ToArray();
            //转换64位            
            var base64Str = Convert.ToBase64String(buf);
            //var result = serviceSDKAdapter.CallService<JObject>("Files", "AddFileBase64", new { FileName = name + format, Buffer = base64Str, Token = token });
            //var fileId = result.Content?.Value<string>("FileId") ?? "";
            //stream.Close();
            //stream.Dispose();
            //url = ATTACHMENT_COMMANDACTION + fileId;
            return url;
        }


    }
}
