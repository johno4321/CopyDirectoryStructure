using System;
using System.IO;

namespace CopyDirectoryStructure
{
    public class CopyDirectoryStructure
    {
        private readonly string _sourceRootDirectory;
        private readonly string _targetRootDirectory;

        public static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("Usage: CopyDirectoryStructure <source root directory path> <target root directory path>");
                return;
            }

            if(!Directory.Exists(args[0]))
            {
                Console.WriteLine("The source directory {0} does not exist", args[0]);
                return;
            }

            if (Directory.Exists(args[1]))
            {
                Console.WriteLine("The target directory {0} exists and I don't overwrite", args[1]);
                return;
            }


            var program = new CopyDirectoryStructure(args[0], args[1]);

            program.Go();
        }

        public CopyDirectoryStructure(string sourceRootDirectory, string targetRootDirectory)
        {
            _sourceRootDirectory = sourceRootDirectory;
            _targetRootDirectory = targetRootDirectory;
        }

        public void Go()
        {
            Console.WriteLine("About to create directory {0}", _targetRootDirectory);
            Directory.CreateDirectory(_targetRootDirectory);
            BuildChildDirectories(_sourceRootDirectory, _targetRootDirectory);
        }

        public void BuildChildDirectories(string sourceParentDirectory, string targetParentDirectory)
        {
            var sourceRootDirectoryInfo = Directory.GetDirectories(sourceParentDirectory);
            
            foreach(var childDirectory in sourceRootDirectoryInfo)
            {
                var newChildDirectoryName = Path.GetFileName(childDirectory);
                var newChildDirectoryPath = Path.Combine(targetParentDirectory, newChildDirectoryName);

                Console.WriteLine("About to create directory {0}", newChildDirectoryPath);

                Directory.CreateDirectory(newChildDirectoryPath);

                BuildChildDirectories(childDirectory, newChildDirectoryPath);
            }
        }
    }
}
