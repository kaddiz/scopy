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
        private static string[] sourcePath;
        private static string destinationPath = "";

        private static int filesCopiedCount = 0;

        private static int ExitCode = 0;

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
                    if (sourcePath[0][0] == '/')
                    {
                        Console.WriteLine("Invalid number of parameters");
                        return 4;
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
                    if (!isKey && i == 1)
                    {
                        destinationPath = args[i];
                    }
                }
            }
            if (help)
            {
                WriteHelp();
                return ExitCode;
            }
            else
            {
                string DestinationPath = currentDirectoryPath;
                if (destinationPath != "") DestinationPath = destinationPath;
                if (DestinationPath[1] != ':' && DestinationPath[2] != '\\' && DestinationPath != currentDirectoryPath)
                {
                    DestinationPath = Path.Combine(currentDirectoryPath, DestinationPath);
                }
                SearchOption searchOption;
                if (withSubdirectories) searchOption = SearchOption.AllDirectories;
                else searchOption = SearchOption.TopDirectoryOnly;

                filesCopiedCount = 0;
                for (int i = 0; i < sourcePath.Length; i++)
                {
                    var item = sourcePath[i];
                    if (item.Length < 3 || item[1] != ':' && item[2] != '\\')
                    {
                        sourcePath[i] = Path.Combine(currentDirectoryPath, sourcePath[i]);
                    }

                    string[] items = sourcePath[i].Split('\\');
                    string path = "";
                    string searchPattern = "*";
                    for (int j = 0; j < items.Length; j++)
                    {                        
                        if (!Directory.Exists(path + items[j] + '\\') && searchPattern == "*") searchPattern = items[j];
                        else path += items[j] + '\\';
                    }
                    if (!Directory.Exists(path))
                    {
                        Console.WriteLine("Directory was not found: {0}", searchPattern);
                        ExitCode = 1;
                        continue;
                    }
                    string[] files = Directory.GetFiles(path, searchPattern, searchOption);
                    if (files.Length == 0)
                    {
                        Console.WriteLine("File(s) was not found: {0}", searchPattern);
                        ExitCode = 1;
                    }
                    
                    if (Directory.Exists(path))
                    {
                        foreach (var file in files)
                        {
                            if (!Directory.Exists(DestinationPath))
                            {
                                Directory.CreateDirectory(DestinationPath);
                            }
                            try
                            {
                                string FilePath = DestinationPath;
                                var paths = file.Replace(path, "").Split('\\');
                                for (int j = 0; j < paths.Length; j++)
                                {
                                    FilePath = Path.Combine(FilePath, paths[j]);
                                    if (paths.Length > 1 && j == paths.Length - 2)
                                        Directory.CreateDirectory(FilePath);
                                }
                                Console.WriteLine(file.Replace(currentDirectoryPath, "").TrimStart('\\'));
                                File.Copy(file, FilePath, withRewrite);
                                filesCopiedCount++;
                            }
                            catch (IOException e)
                            {
                                ExitCode = 1;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("File(s) copied: {0}.", filesCopiedCount);
            return ExitCode;
        }

        static void WriteHelp()
        {
            Console.WriteLine("Copies files and directory trees.");
            Console.WriteLine();
            Console.WriteLine("SCOPY source[+source2][+source3]... [destination] [/S] [/Y]");
            Console.WriteLine();
            Console.WriteLine("  source        Copied files.");
            Console.WriteLine("  destination   Destination directory.");
            Console.WriteLine("  /S            Copied with subdirectories.");
            Console.WriteLine("  /Y            Overwrite an existing destination file.");
            Console.WriteLine();
        }
    }
}
