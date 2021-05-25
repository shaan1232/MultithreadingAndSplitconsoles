using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Globalization;
using System.Diagnostics;

namespace IntroToClasses
{
    class TestConsoles
    {
        // We need to use unmanaged code
        [DllImport("user32.dll")]

        // GetCursorPos() makes everything possible
        static extern bool GetCursorPos(ref Point lpPoint);

        private static void printXYThread(TwoConsoles twoConsoles)
        {
            while (true)
            {
                // New point that will be updated by the function with the current coordinates
                Point defPnt = new Point();

                // Call the function and pass the Point, defPnt
                GetCursorPos(ref defPnt);

                // Now after calling the function, defPnt contains the coordinates which we can read
                twoConsoles.WriteConsole(0, "X = " + defPnt.X.ToString() + " , Y = " + defPnt.Y.ToString());
                if (defPnt.Y < 400)
                {
                    twoConsoles.WriteConsole(0, "YOU ARE IN THE TOP HALF");
                }
                else
                {
                    twoConsoles.WriteConsole(0, "YOU ARE IN THE BOTTOM");
                }
                Thread.Sleep(1000);
            }
        }

        private static void printXYThread(TwoConsoles twoConsoles, int secondsAlive)
        {
            long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            long endTime = currentTime + secondsAlive*1000;
            while (true)
            {
                // New point that will be updated by the function with the current coordinates
                Point defPnt = new Point();

                // Call the function and pass the Point, defPnt
                GetCursorPos(ref defPnt);

                // Now after calling the function, defPnt contains the coordinates which we can read
                twoConsoles.WriteConsole(0, "X = " + defPnt.X.ToString() + " , Y = " + defPnt.Y.ToString());
                if (defPnt.Y < 400)
                {
                    twoConsoles.WriteConsole(0, "YOU ARE IN THE TOP HALF");
                }
                else
                {
                    twoConsoles.WriteConsole(0, "YOU ARE IN THE BOTTOM");
                }
                currentTime =DateTimeOffset.Now.ToUnixTimeMilliseconds();

                if (currentTime > endTime)
                {
                    break;
                }
                Thread.Sleep(1000);

            }
        }

        private static void guessGame(TwoConsoles twoConsoles)
        {

            int upper = 500;
            int lower = 0;
            //int guess;
            Point cursor = new Point();

            while (true)
            {
                twoConsoles.WriteConsole(1, "Between " + upper + "-" + lower);
                twoConsoles.WriteConsole(1, "TOP: [" + upper + "-" + ((upper + lower) / 2) + "]");
                twoConsoles.WriteConsole(1, "LOWER: [" + ((upper + lower) / 2) + "-" + lower + "]");
                Thread.Sleep(3000);
                GetCursorPos(ref cursor);


                if (cursor.Y < 400)
                {
                    lower = (upper+lower) / 2;
                }
                else
                {
                    upper = (upper+lower) / 2;
                }
                if (upper == lower)
                {
                    break;
                }
            }
            twoConsoles.WriteConsole(1, "The number was " + upper);
                        
            
        }



        static void printTime(TwoConsoles a)
        {
            while (true)
            {
                a.WriteConsole(1, DateTime.Now.ToString());
                Thread.Sleep(3000);
            }
        }


        public static void Main(String[] args)
        {
            TwoConsoles twoConsoles = new TwoConsoles();
            var thread1 = new Thread(() => printXYThread(twoConsoles,30));
            var thread2 = new Thread(() => guessGame(twoConsoles));
            thread1.Start();

            thread2.Start();


        }
    }
}
