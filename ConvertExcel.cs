using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace readExcel
{
    public class ConvertExcel
    {

        //轉換excel to dataTable
        public static DataTable ReadExcel(string filePath)
        {
            IWorkbook iwkX;
            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                iwkX = WorkbookFactory.Create(fs);
                fs.Close();
            }

            //工作表
            DataTable dt = new DataTable();

            for (int h = 0; h < iwkX.NumberOfSheets; h++)
            {
                ISheet sheet = iwkX.GetSheetAt(h);

                
                var rows = sheet.GetRowEnumerator();
                bool isMove = rows.MoveNext();
                //循环sheet
                if (isMove)
                {
                    //取得行
                    var Cols = (IRow)rows.Current;
                    //取得工作表名稱
                    dt.TableName = sheet.SheetName;

                    for (int i = 0; i < Cols.LastCellNum; i++)
                    {
                        //string str = Cols.GetCell(i).ToString();
                        dt.Columns.Add(Cols.GetCell(i).ToString());
                    }

                    while (rows.MoveNext())
                    {
                        //取得列
                        var row = (IRow)rows.Current;
                        var dr = dt.NewRow();
                        for (int i = 0; i < row.LastCellNum; i++)
                        {
                            var cell = row.GetCell(i);

                            if (cell == null)
                            {
                                dr[i] = "";
                            }
                            else
                            {
                                //string strdr = cell.ToString();
                                dr[i] = cell.ToString();
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }
    }
}
