using System;
using System.IO;

namespace Application
{

    public class TextFile
    {
        private string filePath;

        public TextFile(string path)
        {
            filePath = path;
        }


        public void Create()
        {
            File.WriteAllText(filePath, "old text - ");
            Console.WriteLine($"Створено текстовий файл: {filePath}");

        }

        public void Rename(string newFileName)
        {
            string directory = Path.GetDirectoryName(filePath);
            string newFilePath = Path.Combine(directory, newFileName);
            File.Move(filePath, newFilePath);
            filePath = newFilePath;
            Console.WriteLine($"Файл перейменовано на: {newFilePath}");

        }


        public void PrintContent()
        {
            string content = File.ReadAllText(filePath);
            Console.WriteLine($"Вміст файлу {filePath}:");
            Console.WriteLine(content);
        }


        public void Append(string text)
        {
            File.AppendAllText(filePath, text);
            Console.WriteLine($"Доповнено текстовий файл {filePath}");
        }


        //public void Delete()
        //{
        //    File.Delete(filePath);
        //    Console.WriteLine($"Видалено текстовий файл: {filePath}");
        //}


        public override bool Equals(object? obj)
        {
            if (obj is TextFile other)
            {
                return filePath == other.filePath;
            }
            return false;


        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override string ToString()
        {
            return $"TextFile is {filePath}";
        }
    }
    public class Directory
    {
        public string directoryPath;

        public Directory(string path)

        {
            directoryPath = path;
        }

        public void Create()
        {
            System.IO.Directory.CreateDirectory(directoryPath);
            Console.WriteLine($"Створено директорію: {directoryPath}");
        }

        public override bool Equals(object? obj)
        {
            if (obj is Directory other)
            {
                return directoryPath == other.directoryPath;
            }
            return false;


        }


        public override int GetHashCode()
        {
            return directoryPath.GetHashCode();
        }


        public override string ToString()
        {
            return $"TextFile: {directoryPath}";
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = System.IO.Directory.GetCurrentDirectory();
            string filePath = Path.Combine(directoryPath, "text.txt");


            TextFile textFile = new TextFile(filePath);
            Directory directory = new Directory(directoryPath);


            directory.Create();
            textFile.Create();
            textFile.Rename("new_text.txt");
            textFile.PrintContent();
            textFile.Append("new text");
            textFile.PrintContent();
            //textFile.Delete();

            Console.ReadKey();
        }
    }
}