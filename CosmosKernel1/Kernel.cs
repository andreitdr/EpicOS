using System;

using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using CosmosKernel1.Functions;
using GUI;
using System.Drawing;
using Cosmos.System.Graphics;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        private CosmosVFS fs;

        private bool GUI;


        private Items.ProgressBar progressBar = new Items.ProgressBar('[', ']', '=', 100);
        //private Items.Spinner spinner = new Items.Spinner();

        public static uint screenWidth = 1280;
        public static uint screenHeight = 720;

        public static DoubleBufferedVMWareSVGAII vMWareSVGAII;

        int[] cursor = new int[]
        {
                1,0,0,0,0,0,0,0,0,0,0,0,
                1,1,0,0,0,0,0,0,0,0,0,0,
                1,2,1,0,0,0,0,0,0,0,0,0,
                1,2,2,1,0,0,0,0,0,0,0,0,
                1,2,2,2,1,0,0,0,0,0,0,0,
                1,2,2,2,2,1,0,0,0,0,0,0,
                1,2,2,2,2,2,1,0,0,0,0,0,
                1,2,2,2,2,2,2,1,0,0,0,0,
                1,2,2,2,2,2,2,2,1,0,0,0,
                1,2,2,2,2,2,2,2,2,1,0,0,
                1,2,2,2,2,2,2,2,2,2,1,0,
                1,2,2,2,2,2,2,2,2,2,2,1,
                1,2,2,2,2,2,2,1,1,1,1,1,
                1,2,2,2,1,2,2,1,0,0,0,0,
                1,2,2,1,0,1,2,2,1,0,0,0,
                1,2,1,0,0,1,2,2,1,0,0,0,
                1,1,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,0,1,1,0,0,0,
        };

        protected override void BeforeRun()
        {

            GUI = false;

            Console.WriteLine("Press Any key to start in GUI mode ");
            int s = 5;
            while (s >= 0)
            {
                if (Console.KeyAvailable)
                {
                    GUI = true;
                    break;
                }
                Console.Write(s);
                Cosmos.HAL.Global.PIT.Wait(1000);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                s--;
            }



            if (GUI)
            {
                // Start GUI
                vMWareSVGAII = new DoubleBufferedVMWareSVGAII();
                vMWareSVGAII.SetMode(screenWidth, screenHeight, 32);

                // Initialize Mouse
                Sys.MouseManager.ScreenWidth = screenWidth;
                Sys.MouseManager.ScreenHeight = screenHeight;

                Sys.MouseManager.X = screenWidth / 2;
                Sys.MouseManager.Y = screenHeight / 2;

                return;
            }

            fs = new CosmosVFS();
            VFSManager.RegisterVFS(fs);
            fs.Initialize(true);

            Console.Clear();


            Console.WriteLine("Available commands:\n" +
                     "wf [dir] [file.extension] [text] - write to the specified file\n" +
                     "rf [fullFilePath] - read from file\n" +
                     "cpuram - displays the amount of RAM in MB\n" +
                     "exit - stops the OS\n" +
                     "part_free_space - get the free space on one partition\n" +
                     "dirInfo [fullDirPath] - get the information about specified directory\n" +
                     "delDir [fullDirPath] - delete the specified folder\n" +
                     "delFile [fullFilePath] - delete the specified file");

        }
        protected override void Run()
        {
            if (GUI)
            {
                vMWareSVGAII.DoubleBuffer_Clear((uint)Color.Black.ToArgb());
                DrawCursor(vMWareSVGAII, Sys.MouseManager.X, Sys.MouseManager.Y);
                vMWareSVGAII.DoubleBuffer_Update();

                return;
            }


            Console.Write("> ");
            var line = Console.ReadLine();
            var input = line.Split(' ');
            switch (input[0])
            {
                case "wf":
                    FileManagement.WriteToFile(input[1], input[2], input[3]);
                    break;
                case "rf":
                    string content = FileManagement.ReadFromFile(input[1]);
                    Console.WriteLine(content);
                    break;
                case "exit":
                    Stop();
                    break;
                case "cpuram":
                    Console.WriteLine("You have " + Cosmos.Core.CPU.GetAmountOfRAM() + " MB of RAM");
                    break;
                case "part_free_space":
                    try
                    {
                        if (input[1].Length == 0 || input[1] == null || input.Length == 1)
                            throw new Exception();
                        long available_space = VFSManager.GetAvailableFreeSpace(input[1]);
                        Console.WriteLine("Available Free Space: " + Conversion.ToMB(available_space) + " MB " + available_space + " bytes");
                    }
                    catch
                    {
                        Console.WriteLine("The specified partition does not exist !");
                    }
                    break;
                case "dirInfo":
                    try
                    {
                        if (VFSManager.DirectoryExists(input[1]))
                        {
                            FileManagement.DisplayDirectoryData(input[1]);
                        }
                        else
                        {
                            Console.WriteLine("Directory: {0} can not be found", input[1]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "delDir":
                    try
                    {
                        if (!VFSManager.DirectoryExists(input[1]))
                            throw new Exception("Directory does not exist !");

                        var directory = VFSManager.GetDirectory(input[1]).GetUsedSpace();

                        Console.WriteLine("The following directory will be deleted: ");

                        FileManagement.DisplayDirectoryData(input[1]);

                        Console.WriteLine("Are you sure? [y/n]");
                        if (Console.ReadLine() != "y" && Console.Read() != 'y')
                            throw new Exception("Operation cancelled");

                        VFSManager.DeleteDirectory(input[1], true);

                        Console.WriteLine("Deleted directory {0}.\nCleaned : {1} MB of memory", input[1], directory);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "waitTime":
                    progressBar.WaitTime(350, int.Parse(input[1]), "Waiting time...");
                    break;
                case "delFile":
                    try
                    {
                        if (!VFSManager.FileExists(input[1]))
                            throw new Exception("File does not exist !");

                        var file = VFSManager.GetFile(input[1]).GetUsedSpace();

                        VFSManager.DeleteFile(input[1]);

                        Console.WriteLine("Cleaned {0} bytes", file);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
            }

        }

        public void DrawCursor(DoubleBufferedVMWareSVGAII driver, uint x, uint y)
        {
            for (uint h = 0; h < 19; h++)
            {
                for (uint w = 0; w < 12; w++)
                {
                    if (cursor[h * 12 + w] == 1)
                    {
                        driver.DoubleBuffer_SetPixel(w + x, h + y, (uint)Color.Black.ToArgb());
                    }
                    if (cursor[h * 12 + w] == 2)
                    {
                        driver.DoubleBuffer_SetPixel(w + x, h + y, (uint)Color.MediumPurple.ToArgb());
                    }
                }
            }
        }
    }
}
