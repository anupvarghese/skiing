using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPath
{
    public class Map
    {
        //
        // Array to find out neighbouring nodes
        //                           E, N, W, S
        public static int[] xDir = { 1, 0, -1, 0 };
        public static int[] yDir = { 0, 1, 0, -1 };

        public Node[,] map;
        public int[,] mapData;

        private int width, height;
        private readonly Stopwatch stopWatch;

        public Map(int[,] mapData, int width, int height)
        {
            this.mapData = mapData;
            this.width = width;
            this.height = height;

            makeNodes();
            initializeNeighbors();
        }
        /// <summary>
        /// Create nodes
        /// </summary>
        private void makeNodes()
        {
            map = new Node[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = new Node(i, j, mapData[i, j]);
                }
            }
        }
        /// <summary>
        /// Initialize neighbours
        /// </summary>
        private void initializeNeighbors()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //Console.WriteLine("Setting neighbour for map[" + i + "][" + j + "] = " + map[i, j].data);
                    for (int k = 0; k < 4; k++)
                    {
                        if (checkRange(i + xDir[k], j + yDir[k]))
                        {
                            //Console.WriteLine("Neighbour " + k + " for map[" + (i + xDir[k]) + "][" + (j + yDir[k]) + "] = " + map[i + xDir[k], j + yDir[k]].data);
                            map[i, j].neighbors[k] = map[i + xDir[k], j + yDir[k]];
                        }
                    }
                }
            }
        }

        private bool checkRange(int r, int c)
        {
            return r < height && r >= 0 && c < width && c >= 0;
        }

        public void loopthrough()
        {

            List<int> max = new List<int>();
            List<int> second = new List<int>();

            // Iterate through each node
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // find longest path in each node
                    var str = longest(i, j);

                    // find the max path
                    if (max != null && (str.Count > max.Count))
                    {
                        second = max;
                        max = str;
                    }
                    // just incase if two paths are equal => check steep
                    else if (max != null && (str.Count == max.Count))
                    {
                        var top1 = str[0];
                        var bottom1 = str[str.Count - 1];

                        var top2 = max[0];
                        var bottom2 = max[max.Count - 1];

                        // steep comparison
                        if ((top1 - bottom1) > (top2 - bottom2))
                        {
                            second = max;
                            max = str;
                        }
                    }
                }
            }
            Console.WriteLine("Longest and steepest = ");
            foreach(var n in max)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine("Drop = {0} ", max[0] - max[max.Count - 1]);
            Console.WriteLine("Length = {0} ", max.Count);
            Console.WriteLine("Email Id = {0}{1}@redmart.com", max.Count,max[0] - max[max.Count - 1]);
            Console.WriteLine("Second Longest and steepest = ");
            foreach (var n in second)
            {
                Console.WriteLine(n);
            }
        }

        /// <summary>
        /// Recursively call this method to find longest path
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <returns></returns>
        public List<int> longest(int x, int y)
        {
            // Initialize
            map[x, y].directions[0] = new List<int>();
            map[x, y].directions[1] = new List<int>();
            map[x, y].directions[2] = new List<int>();
            map[x, y].directions[3] = new List<int>();
            
            // Add first position
            map[x, y].directions[0].Add(map[x, y].data);
            map[x, y].directions[1].Add(map[x, y].data);
            map[x, y].directions[2].Add(map[x, y].data);
            map[x, y].directions[3].Add(map[x, y].data);

            // Check in East direction
            if (map[x, y].neighbors[0] != null && map[x, y].data > map[x, y].neighbors[0].data)
                // Add new range of values after recursive call
                map[x, y].directions[0].AddRange(longest(map[x, y].neighbors[0].r, map[x, y].neighbors[0].c));
            // Check in North direction
            if (map[x, y].neighbors[1] != null && map[x, y].data > map[x, y].neighbors[1].data)
                map[x, y].directions[1].AddRange(longest(map[x, y].neighbors[1].r, map[x, y].neighbors[1].c));
            // Check in West direction
            if (map[x, y].neighbors[2] != null && map[x, y].data > map[x, y].neighbors[2].data)
                map[x, y].directions[2].AddRange(longest(map[x, y].neighbors[2].r, map[x, y].neighbors[2].c));
            // Check in South direction
            if (map[x, y].neighbors[3] != null && map[x, y].data > map[x, y].neighbors[3].data)
                map[x, y].directions[3].AddRange(longest(map[x, y].neighbors[3].r, map[x, y].neighbors[3].c));

            // Find the longest of the above 4
            map[x, y].longest = FindLongest(map[x, y].directions);

            // return longest 
            return map[x, y].longest;
        }

        /// <summary>
        /// Method to find longest list in each direction
        /// </summary>
        /// <param name="lists">lists of each directions (E, W, N, S)</param>
        /// <returns></returns>
        public List<int> FindLongest(params List<int>[] lists)
        {
            var longest = lists[0];
            for (var i = 1; i < lists.Length; i++)
            {
                if (lists[i].Count > longest.Count)
                    longest = lists[i];
            }
            return longest;
        }

    }
}
