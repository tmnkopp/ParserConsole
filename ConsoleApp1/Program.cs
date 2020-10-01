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
            //BlockExtractor
            CodeBaseParser parse = new CodeBaseParser();
            parse.ParseResultMode = ParseResultMode.Verbose;
            parse.Parsers.Add(new LineExtractor("http:", 0)); 
            parse.ParseTo(new CacheWriter());
            Cache.CacheEdit(); 
        }
        public class CustomParse : BaseParser
        {
            public CustomParse()
            {
                Path = @"C:\Users\Tim\source\repos\AssistWith2\AssistWith2\Pages\Clients\*";
            }
        }

        public class DBParser : BaseParser
        {
            public DBParser()
            {
                Path =AppSettings.DirDB; 
                PathExclusions.AddRange(AppSettings.PathExclusions.Split(new char[] { ',' }));
                PathExclusions.AddRange("\\Triggers,\\Utils,\\InProgress".Split(new char[] { ',' }));
                //this.Compilers.Add(new RegexCompile("--.*", "")); 
            }
        }
     
        public class CodeBaseParser : BaseParser
        {
            public CodeBaseParser()
            {
                Path = AppSettings.DirCode + ""; 
                PathExclusions.AddRange(AppSettings.PathExclusions.Split(new char[] { ',' }));
                PathExclusions.AddRange(".Config,.config,.vbproj,.html,.htm,.css".Split(new char[] { ',' }));
                //PostCompile.Add(  new RegexCompile(@"\[.* \d*\]", "")  );
            }
        }
        public class AppSettingsParser : BaseParser
        {
            public AppSettingsParser()
            {
                //Path = @"c:\temp\save\*merge*.txt"; // AppSettings.DirCustomControls;
                Path = @"C:\Users\Tim\source\repos\somuing\src\app\*.html";
                PathExclusions.AddRange(AppSettings.PathExclusions.Split(new char[] { ',' }));
                PathExclusions.AddRange("\\Triggers,\\Utils,\\InProgress".Split(new char[] { ',' }));
       
            } 
        } 
    }
}
 