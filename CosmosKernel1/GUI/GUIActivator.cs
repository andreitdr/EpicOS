using Cosmos.HAL.Drivers.PCI.Video;
using GUI;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CosmosKernel1.GUI
{
    public class GUIActivator
    {
        /// <summary>
        /// Cursor style.
        /// </summary>
        public int[] cursor = new int[]
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
        public static uint                       screenWidth  = 1280;
        public static uint                       screenHeight = 720;

        public static DoubleBufferedVMWareSVGAII vMWareSVGAII;

        public void Refresh()
        {
            vMWareSVGAII.DoubleBuffer_Clear((uint)Color.Black.ToArgb());
            DrawCursor(vMWareSVGAII, Cosmos.System.MouseManager.X, Cosmos.System.MouseManager.Y);
            vMWareSVGAII.DoubleBuffer_Update();
        }

        public void Initialize()
        {
            // Start GUI
            vMWareSVGAII = new DoubleBufferedVMWareSVGAII();
            vMWareSVGAII.SetMode(screenWidth, screenHeight, 32);

            // Initialize Mouse
            Cosmos.System.MouseManager.ScreenWidth  = screenWidth;
            Cosmos.System.MouseManager.ScreenHeight = screenHeight;

            Cosmos.System.MouseManager.X = screenWidth  / 2;
            Cosmos.System.MouseManager.Y = screenHeight / 2;
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
