using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace M_Gui
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

           /* int[,] m = new int[10, 7]{
            {0,        0,         0,       0,        -1,       0,       0},
            {0,        0,         0,      0,        0,       0,       0},
            {0,        5,         0,      -1,        0,       0,       -1},
            {0,        0,         0,       0,        0,       0,       0},
            {0,       0,          0,       0,        -1,       0,       0},
            {0,        0,         -1,       0,        0,       0,       0},
            {0,        0,         0,       0,        0,       0,       0},
            {-1,        0,         -1,       0,        0,       0,       0},
            {0,        0,         0,       0,        0,       0,       -1},
            {0,        0,         0,       0,        0,       0,       -1},

        };
          {0,        0,         0,        0,        -1,       0,       0},
            {0,        0,         0,        0,        0,       0,       0},
            {0,        5,         0,       -1,        0,       0,       -1},
            {0,        0,         0,       0,        0,       0,       0},
            {0,       0,          0,       0,        -1,       0,       0},
            {0,        0,         -1,       0,        0,       0,       0},
            {0,        0,         0,       0,        0,       0,       0},
            {-1,        0,         -1,       0,        0,       0,       0},
            {0,        0,         0,       0,        0,       0,       -1},
            {0,        0,         0,       0,        0,       0,       -1},

        };*/
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                FileManager F = new FileManager();
                F.ReadText1();
                Application.Run(new Form1(F.Row_Number_2, F.Column_Number_2, F.Matrix_2, F.Car.Row, F.Car.Column, F.Goal.Row, F.Goal.Column));
            }
            else
            {
                MessageBox.Show("Application is already executing");
                NativeMethods.PostMessage(
                (IntPtr)NativeMethods.HWND_BROADCAST,
                NativeMethods.WM_SHOWME,
                IntPtr.Zero,
                IntPtr.Zero);
            }

            //S.Generator();
        }
        internal class NativeMethods
        {
            public const int HWND_BROADCAST = 0xffff;
            public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
            [DllImport("user32")]
            public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
            [DllImport("user32")]
            public static extern int RegisterWindowMessage(string message);
        }
    }
}
