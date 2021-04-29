using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace readExcel
{
    public class CachePrac
    {
        private ObjectCache _cache = MemoryCache.Default;

        public CacheEntryRemovedCallback DoAfterCacheRemove;

        public string PolicyType { get; set; }

        public string FileContents
        {
            get
            {
                string cacheKey = "FileContents";
                //http://www.itpow.com/c/2008/08/25KMCKQ5VOHXU9KF.asp
                //如果 obj 为 null，调用 obj.ToString 方法会导致 NullReferenceException 异常，
                //调用 Convert.ToString 不会抛出异常而返回一个 null
                //用 as 方法则会相对平稳，当 obj 的运行时类型不是 string 时会返回 null 而不抛出异常
                string fileContents = _cache[cacheKey] as string;

                if (string.IsNullOrWhiteSpace(fileContents))
                {
                    //載入檔案
                    string filePath = "C:/Users/JIN78/Desktop/資料庫差異/cachetest.txt";
                    fileContents = File.ReadAllText(filePath, Encoding.Default);
                    
                    // 設定 快取機制
                    var policy = new CacheItemPolicy();
                    //回收快取-後
                    //要執行的動作
                    policy.RemovedCallback = DoAfterCacheRemove;

                    switch (PolicyType)
                    {
                        case "1":
                            //距離設定快取時間超過2秒後，回收快取
                            //下一次取資料時，再次讀取實體檔案來載入至快取。
                            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(2);
                            break;
                        case "2":
                            // 3秒期限內未使用快取時，回收快取
                            policy.SlidingExpiration = TimeSpan.FromSeconds(3);
                            break;
                        default:
                            // 資料異動時，回收快取
                            policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string>() { filePath }));
                            break;
                    }

                    
                    
                    //Set(快取已存在時，直接覆寫)
                    _cache.Set(cacheKey, fileContents, policy);

                    //Add (快取已存在時，不會覆寫原有設定，會回傳false結果告知新增失敗)
                    bool addsus=_cache.Add(cacheKey, fileContents, policy);
                }

                return fileContents;
            }
        }
    }
}
