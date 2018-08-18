using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SoccerStats
{
    class Program
    {
        static void Main(string[] args)
        {
        //Assign currentDirectory to where we are in the file system
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
        //Append our current directory to the document we want to read so we have the full file path
            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
            var fileContents = ReadFile(fileName);

        //Using the carraige return and new line
        //characters as delimiters
        //we can split our one string into an array of strings
        //NOTE: StringSplitOptions is an IEnumerator used for limited options
        //This IEnumerator evaluates to 0 (Don't remove) or 1 (Remove)
            string[] fileLines = fileContents.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var line in fileLines)
            {
                Console.WriteLine(line);
            }
        }

        public static string ReadFile(string fileName)
        {
            using(var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
