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

namespace Searcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("S>");
            //string result = Console.ReadLine();

            ParseBuilder<Parser> parser = new ParseBuilder<Parser>();
            parser.Find("Panel1") 
                .FileFilter("*aspx*")
                .Parse(); 

        }
        public class Parser : BaseParser
        {
            public Parser()
            {
                DirSource = AppSettings.SourceDir;
                ExcludeList.AddRange(AppSettings.ExcludeList.Split(new char[] { ',' }));
            }
            public override string ParseFinding(string content)
            {
                string result = new LineExtractor(Find, 3, false).Execute(content);
                string s = new string('#', 255);
                return $"{result}\n{s}\n";
            }
        } 
    }
}
 