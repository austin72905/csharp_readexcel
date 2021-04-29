using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using NPOI.SS.UserModel;

namespace readExcel
{
    public class Program
    {
        //寫入的文件名
        private static string DifFileName => "C:/Users/JIN78/Desktop/difPayFix.txt";
        private static string DifFileNameDistinct => "C:/Users/JIN78/Desktop/difPaySoloFix.txt";

        private static string DifOldFileName => "C:/Users/JIN78/Desktop/difPayOld.txt";

        private static string DifFileOldNameDistinct => "C:/Users/JIN78/Desktop/difPaySoloOld.txt";

        const string DafaUnable= "C:/Users/JIN78/Desktop/資料庫差異/DafaUnable.txt";

        const string OurUnable = "C:/Users/JIN78/Desktop/資料庫差異/OurUnable.txt";

        static void Main(string[] args)
        {
            //新庫與大發筆較
            //var dtList = new Program().GetDif();
            //寫進文件
            //new Program().WriteToTxt(DifFileName, dtList);



            //var newdtlist = new Program().GetDistinct(dtList);
            //寫進文件
            //new Program().WriteToTxt(DifFileNameDistinct, newdtlist);

            //舊庫與大發筆較
            //var OlddtList = new Program().GetDifOld();
            //new Program().WriteToTxt(DifOldFileName, OlddtList);

            //var new1dtlist = new Program().GetDistinct(OlddtList);
            //new Program().WriteToTxt(DifFileOldNameDistinct, new1dtlist);


            //比較大發與我們資料庫停用的第三方差異
            //new Program().GetDifIsEnable();

            //string text = "众宝2.0";
            //if (!string.IsNullOrEmpty(text))
            //{
            //    if (text.Contains(".") && !PayNameInSetting.Contains(text))
            //    {
            //        new RobotSend().RobotApiNew("跑到網關判斷式");
            //    }
            //    else
            //    {
            //        if (PayNameInSetting.Contains(text))
            //        {
            //            new Program().ReturnSet(text);
            //        }
            //    }
            //}

            //資料來源
            //var dataSource = new CachePrac();
            //dataSource.DoAfterCacheRemove = (arguments) =>
            //{
            //    //緩存被清除時會執行

            //    Console.WriteLine("[remove time] " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));
            //    Console.WriteLine("======> 緩存已被消失");
            //};

            ////選擇快取機制
            //Console.WriteLine("[1] AbsoluteExpiration(2s) ");
            //Console.WriteLine("[2] SlidingExpiration(3s) ");
            //Console.WriteLine("[3] ChangeMonitors ");
            //Console.Write("Please choose cache policy: ");
            //dataSource.PolicyType = Console.ReadLine();

            ////show
            //while (true)
            //{
            //    Console.WriteLine("[time] " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));
            //    Console.WriteLine("[cache value] " + dataSource.FileContents);

            //    string cmd = Console.ReadLine();
            //    if(cmd == "exit")
            //    {
            //        break;
            //    }
            //}

            //string recordcode = "rk123";
            ////var checkRe = new RecordCache();
            //var dt = DateTime.Now;
            //while ((DateTime.Now-dt).TotalSeconds<5)
            //{
            //    //訂單資料
            //    var checkReExist = RecordCache.CheckRecord(recordcode);

            //    Console.WriteLine(checkReExist);
            //    //每0.5秒，確認一次訂單號是否已經在緩存裡面
            //    Thread.Sleep(500);

            //    //if(recordcode == "rk123")
            //    //{
            //    //    recordcode = "rk124";
            //    //}else if(recordcode== "rk124")
            //    //{
            //    //    recordcode = "rk123";
            //    //}

            //}


            ////緩存清除時會執行
            //RecordCache.DoAfterCacheRemove = (arguments) =>
            //{
            //    Console.WriteLine("[remove time] " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));
            //    Console.WriteLine("======> 緩存已被消失");
            //};

            Console.WriteLine("請輸入指令");
            Console.WriteLine("輸入訂單號 並按 enter 輸入");
            Console.WriteLine("exit 離開");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("------START-------");
                
                string cmd = Console.ReadLine();

                if(cmd == "exit")
                {
                    break;
                }

                var checkReExist = RecordCache.CheckRecord(cmd);
                if(checkReExist == false)
                {
                    Console.WriteLine("cache existed");
                }
                string showCache = RecordCache.ShowCacheVal();
                Console.WriteLine("緩存內容:["+ showCache+"]");
                Console.WriteLine();
                Console.WriteLine("-------END--------");
                Console.WriteLine();





            }

            



            Console.Read();
        }

        //用list 比較 大發後台、 新資料庫差異
        
        //public List<string> GetDif()
        //{
        //    string fileName = "C:/Users/JIN78/Desktop/dafaPay.xlsx";

        //    //將excel 轉換成datatable
        //    var dt = ConvertExcel.ReadExcel(fileName);

        //    List<string> dafaList = new List<string>();

        //    for(var i = 0; i < dt.Rows.Count; i++)
        //    {
        //        string PayWayStr = dt.Rows[i]["PayWay"].ToString();

        //        //如果支付方式不只一個
        //        if (PayWayStr.Contains(","))
        //        {
        //            string[] PayWayArr = PayWayStr.Split(',');
        //            foreach(var val in PayWayArr)
        //            {
        //                dafaList.Add(dt.Rows[i]["PayName"].ToString() + " " + dt.Rows[i]["PayChannel"].ToString() + val);
        //            }

        //        }
        //        else
        //        {
        //            dafaList.Add(dt.Rows[i]["PayName"].ToString() + " " + dt.Rows[i]["PayChannel"].ToString() + dt.Rows[i]["PayWay"].ToString());
        //        }
                
        //    }
        //    //新資料庫的
        //    List<string> NewThirdList = new List<string>();
        //    for(var i = 0; i < SettingDt.Rows.Count; i++)
        //    {
        //        string NewPcnl=SettingDt.Rows[i]["PayChannel"].ToString();



        //        if (NewPcnl == "雲閃付")
        //        {
        //            NewPcnl=NewPcnl.Replace("雲閃付", "云闪付");
        //        }else if (NewPcnl == "京東錢包")
        //        {
        //            NewPcnl=NewPcnl.Replace("京東錢包", "京东钱包");
        //        }

        //        NewThirdList.Add(SettingDt.Rows[i]["PayName"].ToString() + " " + NewPcnl + SettingDt.Rows[i]["PayWay"].ToString());
        //    }

        //    //比較兩者差異
        //    foreach(var val in NewThirdList)
        //    {
        //        if (dafaList.Contains(val))
        //        {
        //            dafaList.Remove(val);
        //        }
        //    }

        //    return dafaList;
        //}

        ////舊資料庫
        //public List<string> GetDifOld()
        //{
        //    string fileName = "C:/Users/JIN78/Desktop/dafaPay.xlsx";

        //    //將excel 轉換成datatable
        //    var dt = ConvertExcel.ReadExcel(fileName);

        //    List<string> dafaList = new List<string>();

        //    for (var i = 0; i < dt.Rows.Count; i++)
        //    {
        //        string PayWayStr = dt.Rows[i]["PayWay"].ToString();

        //        //如果支付方式不只一個
        //        if (PayWayStr.Contains(","))
        //        {
        //            string[] PayWayArr = PayWayStr.Split(',');
        //            foreach (var val in PayWayArr)
        //            {
        //                dafaList.Add(dt.Rows[i]["PayName"].ToString() + " " + dt.Rows[i]["PayChannel"].ToString() + val);
        //            }

        //        }
        //        else
        //        {
        //            dafaList.Add(dt.Rows[i]["PayName"].ToString() + " " + dt.Rows[i]["PayChannel"].ToString() + dt.Rows[i]["PayWay"].ToString());
        //        }

        //    }
        //    //舊資料庫的
        //    List<string> OldThirdList = new List<string>();
        //    for (var i = 0; i < SettingDtOld.Rows.Count; i++)
        //    {
        //        string[] setting = SettingDtOld.Rows[i]["setting"].ToString().Split('綁');
        //        if (!string.IsNullOrWhiteSpace(setting[0].Trim()))
        //        {

        //            //["支付宝:扫码","微信支付:H5","綁定說明:商戶號綁定appId"]
        //            string[] OldPayArr = setting[0].Trim().Split(' ');
        //            //a: b1,b2
        //            foreach (var val in OldPayArr)
        //            {

        //                //payCnl[0]=a
        //                //payCnl[1]=b1,b2
        //                //["支付宝","扫码"]
        //                //["支付宝","扫码,H5"]
        //                string[] payCnl = val.Split(':');
        //                //["支付宝","扫码","H5"]
        //                string[] payWay = payCnl.LastOrDefault().Split(',');

        //                for (var k = 0; k < payWay.Length; k++)
        //                {
        //                    OldThirdList.Add(SettingDtOld.Rows[i]["payName"].ToString() + " " + payCnl[0] + payWay[k].Replace("。", ""));
        //                }

        //            }

        //        }
                


                
        //    }



        //    //比較兩者差異
        //    foreach (var val in OldThirdList)
        //    {
        //        if (dafaList.Contains(val))
        //        {
        //            dafaList.Remove(val);
        //        }
        //    }

        //    return dafaList;
        //}


        ///// <summary>
        ///// 比較大發與我們資料庫停用的第三方差異
        ///// </summary>

        //public void GetDifIsEnable()
        //{
        //    //從哪個excel讀資料
        //    string fileNameDafa = "C:/Users/JIN78/Desktop/資料庫差異/DafaUnable.xlsx";
        //    string fileNameOurs = "C:/Users/JIN78/Desktop/資料庫差異/OurUnable.xlsx";
        //    //將excel 轉換成datatable
        //    var dtDafa = ConvertExcel.ReadExcel(fileNameDafa);
        //    var dtOurs = ConvertExcel.ReadExcel(fileNameOurs);
        //    //
        //    List<string> dafalist = new List<string>();
        //    List<string> ourslist = new List<string>();
        //    List<string> templist = new List<string>();

        //    for (var i=0;i<dtDafa.Rows.Count; i++)
        //    {
        //        dafalist.Add(dtDafa.Rows[i]["payName"].ToString());
        //    }

        //    for (var i = 0; i < dtOurs.Rows.Count; i++)
        //    {
        //        ourslist.Add(dtOurs.Rows[i]["payName"].ToString());
        //    }

        //    foreach(var val in ourslist)
        //    {
        //        templist.Add(val);
        //    }

        //    //資料庫多停用的部分
        //    foreach(var val in dafalist)
        //    {
        //        if (ourslist.Contains(val))
        //        {
        //            ourslist.Remove(val);
        //        }
        //    }

        //    //我們資料庫沒有資料，但大發停用的第三方
        //    foreach(var val in templist)
        //    {
        //        if (dafalist.Contains(val))
        //        {
        //            dafalist.Remove(val);
        //        }
        //    }

        //    //剩下的寫進文件
        //    //寫進文件
        //    new Program().WriteToTxt(OurUnable, ourslist);
        //    new Program().WriteToTxt(DafaUnable, dafalist);

        //}

        ////寫入文件
        //public void WriteToTxt(string path,List <string> dtList)
        //{
        //    FileStream fs = new FileStream(path, FileMode.Create);
        //    StreamWriter sw = new StreamWriter(fs);
        //    //寫入
        //    for(var i=0;i< dtList.Count; i++)
        //    {
        //        sw.WriteLine(dtList[i]);
        //    }
            
        //    //清空緩衝區
        //    sw.Flush();
        //    //關閉數據流
        //    sw.Close();
        //    fs.Close();


        //}


        

        //public List<string> GetDistinct(List<string> dtlist)
        //{
        //    var tempList = new List<string>();
        //    for (int i = 0; i < dtlist.Count; i++)
        //    {
        //        string[] thirdParty = dtlist[i].Split(' ');
        //        tempList.Add(thirdParty[0]);
        //    }

        //    //重複地只取一筆
        //    HashSet<string> hsList = new HashSet<string>(tempList);

        //    var newdtlist = hsList.ToList<string>();
        //    return newdtlist;
        //}
        

        //取得新資料庫的支付方式
        //public static DataTable _SettingDt = null;

        //public static DataTable SettingDt
        //{
        //    get
        //    {
        //        if (_SettingDt == null)
        //        {
        //            _SettingDt = new DbHelper().GetPaySet();
        //        }
        //        return _SettingDt;
        //    }
        //    set
        //    {
        //        _SettingDt = value;
        //    }
        //}

        ////取得新資料庫的支付方式
        //public static DataTable _SettingDtAll = null;

        //public static DataTable SettingDtAll
        //{
        //    get
        //    {
        //        if (_SettingDtAll == null)
        //        {
        //            _SettingDtAll = new DbHelper().GetPaySetting();
        //        }
        //        return _SettingDtAll;
        //    }
        //    set
        //    {
        //        _SettingDtAll = value;
        //    }
        //}


        ////舊資料庫的支付方式
        //public static DataTable _SettingDtOld = null;

        //public static DataTable SettingDtOld
        //{
        //    get
        //    {
        //        if (_SettingDtOld == null)
        //        {
        //            _SettingDtOld = new DbHelper().GetOldPaySet();
        //        }
        //        return _SettingDtOld;
        //    }
        //    set
        //    {
        //        _SettingDtOld = value;
        //    }
        //}


        ////payName in SettingDt
        //public static List<string> _PayNameInSetting = null;
        //public static List<string> PayNameInSetting
        //{
        //    get
        //    {
        //        if (_PayNameInSetting == null)
        //        {
        //            _PayNameInSetting = new List<string>();

        //            for (int i = 0; i < SettingDtAll.Rows.Count; i++)
        //            {
        //                string thirName = SettingDtAll.Rows[i]["PayName"].ToString();

        //                _PayNameInSetting.Add(thirName);

        //            }
        //        }
        //        return _PayNameInSetting;
        //    }
        //}


        ////返回訊息
        //public string ReturnSet(string text)
        //{
        //    string content = "";

        //    string Setting = "";
        //    string WithDrawSetting = "";
        //    string Remark = "";

        //    string isEnabled = "";
        //    for (int i = 0; i < SettingDtAll.Rows.Count; i++)
        //    {
        //        if (SettingDtAll.Rows[i]["payName"].ToString().Equals(text))
        //        {
        //            string RedirectURL = SettingDtAll.Rows[i]["RedirectURL"].ToString();
        //            string WithdrawURL = SettingDtAll.Rows[i]["WithdrawURL"].ToString();

        //            Remark = SettingDtAll.Rows[i]["remark"].ToString();
        //            Setting = SettingDtAll.Rows[i]["PayChannel"].ToString() + SettingDtAll.Rows[i]["PayWay"].ToString();
        //            WithDrawSetting = SettingDtAll.Rows[i]["WithdrawRemark"].ToString();
        //            if (SettingDtAll.Rows[i]["isEnabled"].ToString() == "0" && SettingDtAll.Rows[i]["WithdrawIsEnabled"].ToString() == "0")
        //            {
        //                if (!string.IsNullOrEmpty(RedirectURL) || !string.IsNullOrEmpty(WithdrawURL))
        //                {
        //                    isEnabled = "此第三方目前是停用狀態";
        //                }

        //            }

        //            //如果只有代付
        //            if (string.IsNullOrEmpty(SettingDtAll.Rows[i]["RedirectURL"].ToString()))
        //            {
        //                content += "";
        //            }
        //            else
        //            {
        //                content += Setting + ",";
        //            }


        //        }
        //    }
        //    new RobotSend().RobotApiNew(content + Environment.NewLine
        //        + "備註: " + Remark + Environment.NewLine
        //        + "代付: " + WithDrawSetting + Environment.NewLine
        //        + isEnabled);
        //    return content;
        //}





    }
}
