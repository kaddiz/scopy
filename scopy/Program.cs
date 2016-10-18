using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scopy
{
    class Program
    {
        private static string[] keys = { "/?", "/S", "/s", "/Y", "/y" };

        private static bool withSubdirectories = false;
        private static bool help = false;
        private static bool withRewrite = false;

        private static string currentDirectoryPath = Directory.GetCurrentDirectory();
        private static string [] sourcePath;

        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid number of parameters");
                return 4;
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (i == 0)
                {
                    if (args[i] == "/?")
                    {
                        help = true;
                        break;
                    }
                    sourcePath = args[i].Split('+');
                    foreach (var item in sourcePath)
                    {
                        if (!Directory.Exists(item) && !File.Exists(item)) return 1;
                    }
                }
                else
                {
                    bool isKey = false;
                    if (args[i][0] == '/') isKey = true;
                    if (isKey)
                    {
                        int index = Array.IndexOf(keys, args[i]);
                        if (index == 0) help = true;
                        if (index == 1 || index == 2) withSubdirectories = true;
                        if (index == 3 || index == 4) withRewrite = true;
                    }
                }
            }
            if (help)
            {
                Console.WriteLine("help");
            }
            else
            {

            }
            return 0;
        }
    }
}
