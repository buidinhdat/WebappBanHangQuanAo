using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class Application
    {
        public static String PATH_EXPORT_FILE_EXCEL= "E:\\Đồ án tốt nghiệp\\BuiDinhDat_BaoCaoDoAnTotNghiep_KHMT1-K10\\EXPORT_EXCEL\\";
        private static readonly DateTime Jan1st1970 = new DateTime
        (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}