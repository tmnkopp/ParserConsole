using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher
{
    public static class AppSettings
    {
            public static string BasePath = ConfigurationManager.AppSettings["BasePath"].ToString(); 
            public static string SourceDir = ConfigurationManager.AppSettings["SourceDir"].ToString();
            public static string DestDir = ConfigurationManager.AppSettings["DestDir"].ToString();
            public static string ExcludeList = ConfigurationManager.AppSettings["ExcludeList"].ToString();
    }
}
