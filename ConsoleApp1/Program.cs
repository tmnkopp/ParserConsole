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
using NDesk.Options;
using System.Text.RegularExpressions;
using System.Collections;
using System.Reflection;

namespace Searcher
{
    class Program
    {
        static void Main(string[] args)
        {   
            bool show_help = false;
            string cmd = null;
            string typ = null;
            string dir = "";
            string folder = null; 
            List<string> search = new List<string>();
            var cnt = 0;
            var parms = new Hashtable();

            var p = new OptionSet() {
                { "c|cmd:", $"cmd ", v => cmd = v },
                { "t|type=", $"type ", v => typ = v  ?? "1"   },
                { "d|dir=", $"dir  ", v => dir = v  ?? "0"  },
                { "f|folder=", $"folder  ", v => folder = v  ?? "*.*"  },
                { "s|search=", $"search  ", v => search.Add(v)  },
                { "p|param={:}{/}", (n, v) => parms.Add(n, v)},
                { "h|help",  "show this message and exit", v => show_help = v != null }
            };  
            List<string> extra = p.Parse(args);

            if (show_help)
                ShowHelp(p);  
            if (search.Count == 0)
                search.Add("*.*");

            cnt = 0;
            Console.Write($"{string.Join("", from d in dirs() let c = cnt++ select $"\n({c}) {d}")}\ndir:"); 
            dir = dirs().ElementAt(Convert.ToInt32(Console.ReadLine())) + folder;  
            Console.WriteLine($"dir: {dir}");

            cnt = 0;
            Console.Write($"{string.Join("", from t in types() let c = cnt++ select $" | ({c}) {t}")}\ntyp:");
            typ = types().ElementAt(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine($"typ: {typ}");
             
            Type type = Type.GetType($"SOM.Procedures.{typ}, SOM"); 
            ParameterInfo[] PI = type.GetConstructors()[0].GetParameters();
            List<object> oparms = new List<object>();
            foreach (ParameterInfo parm in PI)
            {
                Console.Write($"{parm.Name} ({parm.ParameterType.Name}):");
                var item = Console.ReadLine();
                if (parm.ParameterType.Name.Contains("Int"))
                    oparms.Add(Convert.ToInt32(item));
                else
                    oparms.Add(item);
            } 
            DirectoryParser parser = new DirectoryParser(dir);
            parser.Parser = (IParser)Activator.CreateInstance(type, oparms.ToArray());
            parser.Parser.ParseResultMode = ParseResultMode.Verbose;
            parser.Inspect();
             
        }

        /*  utils  */
        private static List<string> dirs() { 
            return new List<string> {
                @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\",
                @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\",
                @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\CustomControls\",
                @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\sprocs\",
                @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\archive\"
            }; 
        }
        private static List<string> types()
        {
            var type = typeof(IParser);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass).ToList();
            return (from t in types select t.Name.ToString()).ToList(); 
        }
        static void ShowHelp(OptionSet p)
        {
            System.Console.WriteLine("Usage: myconsole [OPTIONS]");
            System.Console.WriteLine();
            System.Console.WriteLine("Options:");
            p.WriteOptionDescriptions(System.Console.Out);
        }
        private void Search() {
            List<string> _patterns = new List<string>();
            _patterns.Add("CBArtifact");
            string rt = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\sprocs\*.sql";
            //rt = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\CustomControls\*.*";
            rt = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\*";
            DirectoryParser parser = new DirectoryParser(rt);
            //parser.Parser = new RangeExtractor(_patterns[0], "fsma_POAM", ""); 
            parser.Parser = new LineExtractor(_patterns[0], 5);
            parser.Parser.ParseResultMode = ParseResultMode.Verbose;
            parser.ContentFormatter = (c) => (c + "\n\n\n");
            parser.Inspect();
            // + new String('-', 94) + "\n" 
        }

        private void SearchRMAGovernmentWideCalculate()
        {

            List<string> _patterns = new List<string>();
            _patterns.Add("CreateOrUpdateSPtoParms.*isactive");
            _patterns.Add("CreateOrUpdateSPtoParms.*date");
            _patterns.Add("CreateOrUpdateSPtoParms.*PickList");

            string rt = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\sprocs\cq_*.sql";
            DirectoryParser parser = new DirectoryParser(rt);
            parser.Parser = new ExpressionExtractor(_patterns);
            parser.Parser.ParseResultMode = ParseResultMode.Verbose;
            parser.Inspect();
             
        }

        private void SearchCode() {
            StringBuilder result = new StringBuilder();
            DirectoryParser parser = new DirectoryParser();
            parser = new DirectoryParser();
            parser.Parser = new LineExtractor("PK_PickListType", 12);
            parser.Directory = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\archive\DB_Update7.*.sql";
            parser.ParseDirectory();
            result.Append(parser.ToString());
            Cache.Inspect(result.ToString());
 

        }  
    }
}
 