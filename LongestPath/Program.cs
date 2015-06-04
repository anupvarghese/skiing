using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPath
{
    public class ConsoleAutoStopWatch : IDisposable
    {
        private readonly Stopwatch _stopWatch;

        public ConsoleAutoStopWatch()
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        public void Dispose()
        {
            _stopWatch.Stop();
            TimeSpan ts = _stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                               ts.Hours, ts.Minutes, ts.Seconds,
                                               ts.Milliseconds / 10);
            Console.WriteLine(elapsedTime, "RunTime");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            const int w = 4;
            const int h = 4;
            int[,] data;
            Map map;
            //Read from text file
            using (new ConsoleAutoStopWatch())
            {
                data = ReadFromFile();            
                map = new Map(data, w, h);            
                map.loopthrough();
            }
            Console.ReadLine();
        }

        private static int[,] ReadFromFile()
        {
            String input = File.ReadAllText(@"..\..\input.txt");

            int i = 0, j = 0;
            int[,] result = new int[1000, 1000];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(','))
                {
                    if(String.IsNullOrEmpty(col.Trim()))
                    {
                        continue;
                    }
                    result[i, j] = int.Parse(col.Trim());
                    j++;
                }
                i++;
            }
            return result;
        }
    }
}
