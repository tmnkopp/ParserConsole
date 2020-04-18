using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberBalance.VB.Web.UI;
using SOM;
using SOM.IO;
using SOM.Parsers;
using SOM.Procedures;
using SOM.Extentions;
namespace Searcher
{
    class Program
    {
        static void Main(string[] args)
        {

            Cache.Write("");
            ParseBuilder <AppSettingsParser> PB = new ParseBuilder<AppSettingsParser>();
            PB.Init()  
                .Compilers(new List<ICompiler>() {
                    new LineExtractor("ngModel", 1),
                    new RegexCompile(@"\[  ~~~~~ *\]", "")
                })
                .ParseTo( new SOM.IO.CacheEditor() );

            Cache.Write(
                Cache.Read().RemoveEmptyLines()
            );
            Cache.CacheEdit();
        }

        public class AppSettingsParser : BaseParser
        {
            public AppSettingsParser()
            {
                //Path = @"c:\temp\save\*merge*.txt"; // AppSettings.DirCustomControls;
                Path = @"C:\Users\Tim\source\repos\somuing\src\app\*.html";
                ExcludeList.AddRange(AppSettings.ExcludeList.Split(new char[] { ',' }));
                ExcludeList.AddRange("\\Triggers,\\Utils,\\InProgress".Split(new char[] { ',' }));
       
            } 
        } 
    }
}
 