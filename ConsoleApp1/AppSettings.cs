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
            public static string PathExclusions = ConfigurationManager.AppSettings["PathExclusions"].ToString();

            public static string DirCustomControls = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\CustomControls\";
            public static string DirCode = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\";
            public static string DirDB = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\";
    }
}
