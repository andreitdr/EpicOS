using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CosmosKernel1.Functions
{
    public class FileManagement
    {

        public static void WriteToFile(string fileDirectory, string fileName, string content, bool displayMessage = true)
        {
            try
            {
                VFSManager.CreateDirectory(fileDirectory);
                var file = VFSManager.CreateFile(fileDirectory + "\\" + fileName);
                var buffer = Encoding.ASCII.GetBytes(content);
                file.GetFileStream().Write(buffer, 0, buffer.Length);
                if (displayMessage)
                    Console.WriteLine("Successfully wrote to file !");
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occured while trying to write to file : " + fileDirectory + "\\" + fileName);
                Console.WriteLine(ex.ToString());
            }

        }

        public static string ReadFromFile(string fileFullPath)
        {

            try
            {
                var file = VFSManager.GetFile(fileFullPath);
                Stream fileStream = file.GetFileStream();
                StreamReader reder = new StreamReader(fileStream);
                string content = reder.ReadToEnd();
                return content;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while trying to read from file : {0}", fileFullPath);
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        public static List<string> ReadAllLines(string fullPath)
        {
            List<string> lines = new List<string>();
            try
            {
                var file = VFSManager.GetFile(fullPath);
                Stream fileStream = file.GetFileStream();
                StreamReader reader = new StreamReader(fileStream);
                while (!reader.EndOfStream)
                    lines.Add(reader.ReadLine());
            }
            catch
            {
                Console.WriteLine("An error occured while trying to read from file : {0}", fullPath);
                lines = null;
            }
            return lines;
        }

        public static void DisplayDirectoryData(string dirPath)
        {
            var dir = VFSManager.GetDirectory(dirPath);
            Console.WriteLine("{0}\nUsed space: {1}", dirPath, dir.GetUsedSpace());
            var files = VFSManager.GetDirectoryListing(dirPath);
            foreach (var entry in files)
            {
                Console.WriteLine("{0} => {1} bytes", entry.mName, entry.GetUsedSpace());
            }
        }

    }
}
