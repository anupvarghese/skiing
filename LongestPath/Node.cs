using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPath
{
    public class Node
    {
        public int r, c; // row and column
        public int data; // value at a specific position
        public Node[] neighbors; // neighbour nodes 
        public List<int>[] directions; // E W S N 
        public List<int> longest; // longest path
        public Node(int r, int c, int data)
        {
            this.r = r; 
            this.c = c;
            this.data = data;
            this.neighbors = new Node[4];
            this.directions = new List<int>[4];
        }
    }
}
