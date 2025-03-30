using System;
using System.IO;
using System.Linq;

namespace Yafr
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool exitProgram = false;
            string userInput;
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo parentDirectory = Directory.GetParent(currentDirectory);
            string[] files = Directory.GetFiles(currentDirectory, "*.jpg");
            string filePrefix = parentDirectory.Name + " ";
            string destinationDirectory = currentDirectory;

            ShowHeader(currentDirectory, parentDirectory, destinationDirectory, filePrefix);

            do
            {
                Console.WriteLine();
                Console.WriteLine("Menu options.");
                Console.WriteLine("1. List Files");
                Console.WriteLine("2. Set file prefix");
                Console.WriteLine("3. Set destination folder");
                Console.WriteLine("4. Preview new files names");
                Console.WriteLine("5. Rename the files");
                Console.WriteLine("8. Clear screen");
                Console.WriteLine("9. Exit Program.");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Enter choice. - ");

                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        ListFiles(files);
                        break;
                    case "2":
                        filePrefix = GetFilePrefix(parentDirectory.Name);
                        break;
                    case "3":
                        destinationDirectory = GetDestinationDirectory(destinationDirectory);
                        ShowHeader(currentDirectory, parentDirectory, destinationDirectory, filePrefix);
                        break;
                    case "4":
                        RenameTheFiles(true, files, filePrefix, destinationDirectory);
                        break;
                    case "5":
                        RenameTheFiles(false, files, filePrefix, destinationDirectory);
                        break;
                    case "8":
                        ShowHeader(currentDirectory, parentDirectory, destinationDirectory, filePrefix);
                        break;
                    case "9":
                        exitProgram = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Unknown input, enter the menu option number!");
                        Console.WriteLine();
                        break;
                }
            }
            while (!exitProgram);
        }

        private static string GetDestinationDirectory(string destinationDirectory)
        {
            Console.WriteLine("");
            Console.WriteLine("Adjust the destination folder");
            Console.WriteLine($"{destinationDirectory}");
            string newDirectory = Path.Combine(destinationDirectory, Console.ReadLine());
            Console.WriteLine("");
            Console.WriteLine($"Destination set to: {newDirectory}");
            Console.WriteLine("");
            return newDirectory;
        }

        private static void RenameTheFiles(bool PreviewOnly, string[] files, string filePrefix, string currentDirectory)
        {
            string newFileName;
            Console.WriteLine("");
            if (files.Count() == 0)
            {
                Console.WriteLine("No files in the folder!?!");
            }
            else
            {
                foreach (var file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    newFileName = filePrefix + fileInfo.Name;

                    if (PreviewOnly)
                    {
                        Console.WriteLine($"file: '{fileInfo.Name}' will become: '{newFileName}'");
                    }
                    else
                    {
                        Directory.CreateDirectory(currentDirectory);
                        fileInfo.MoveTo(Path.Combine(currentDirectory, newFileName));
                        Console.WriteLine($"{fileInfo.Name} has been renamed to: {newFileName}");
                    }
                }
            }
            Console.WriteLine("");
        }

        private static string GetFilePrefix(string parentDirectoryName)
        {
            string prefix = parentDirectoryName;
            Console.WriteLine();
            Console.WriteLine($"Enter 'y' to use the folder name - {parentDirectoryName}");
            Console.WriteLine("or enter 'n' to use something else.");
            string prefixChoice = Console.ReadLine();

            if (prefixChoice == "n")
            {
                Console.WriteLine("Enter the filename prefix you wish to use.");
                prefix = Console.ReadLine();
            }

            Console.WriteLine("");
            Console.WriteLine($"file name prefix set as : {prefix}");
            Console.WriteLine("");

            return prefix + " ";
        }

        private static void ListFiles(string[] files)
        {
            Console.WriteLine();
            if (files.Count() == 0)
            {
                Console.WriteLine("The folder is empty!");
            }
            else
            {
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
            }
            
            Console.WriteLine();
        }

        private static void ShowHeader(string currentDirectory, DirectoryInfo parentDirectory, string destinationDirectory, string filePrefix)
        {
            Console.Clear();
            Console.WriteLine("************************************");
            Console.WriteLine("*** Yet Another File Renamer.");
            Console.WriteLine("************************************");
            Console.WriteLine();
            Console.WriteLine($"Current folder - {currentDirectory}");
            Console.WriteLine($"Parent folder - {parentDirectory.Name}");
            Console.WriteLine($"Destination folder - {destinationDirectory}");
            Console.WriteLine($"File prefix - {filePrefix}");
        }
    }
}
