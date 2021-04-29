using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace readExcel
{
    public class RecordCache
    {

        private static ObjectCache _cache ;

        private static ObjectCache Cache
        {
            get
            {
                if(_cache == null)
                {
                    var myNewCache = new RecordCache();
                    _cache = MemoryCache.Default;
                }
                return _cache;

            }
        }



        //判斷傳入的訂單號是否存在
        public static bool CheckRecord(string recordCode)
        {
            //快取機制: 5秒後到期
            var policy = new CacheItemPolicy();
            //policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(5);

            //未取用快取時回收快取
            policy.SlidingExpiration = TimeSpan.FromSeconds(5);


            //如果 recordCode 已經存在，就會新增失敗，返回false
            bool addRecord = Cache.Add(recordCode, recordCode, policy);
            return addRecord;
        }

        //顯示緩存內容
        public static string ShowCacheVal()
        {
            var showDic = new Dictionary<string, string>();
            foreach(var val in Cache)
            {
                showDic.Add(val.Key,val.Value.ToString());
            }

            string valinCache = string.Join(",", showDic.Select(o=>o.Key));

            return valinCache;
        }

        //初始化 ObjectCache
        private static ObjectCache InitCache()
        {
            if (_cache == null)
            {
                var myNewCache = new RecordCache();
                _cache = MemoryCache.Default;

            }
            return _cache;
        }


    }
}
