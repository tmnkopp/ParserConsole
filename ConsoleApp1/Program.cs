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
            //new LineExtractor("COLUMN", 5)
            ParseBuilder<DBParser> PB = new ParseBuilder<DBParser>();
            PB.AddCompiler(new LineExtractor("INSERT INTO AuditLog", 2))
              .ParseTo(  new CacheWriter(), (r)=>(r)  ); 
            Cache.CacheEdit(); 
        }
        //       
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
 