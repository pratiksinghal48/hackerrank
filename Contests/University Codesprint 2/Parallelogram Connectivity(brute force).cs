//https://www.hackerrank.com/contests/university-codesprint-2/challenges/parallelogram-connectivity

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {

    static void Main(String[] args) {
        int[] temp = Array.ConvertAll(Console.ReadLine().Split(' '),Convert.ToInt32);
        int n = temp[0];
        int m = temp[1];
        char[][] board = new char[n][];
        for(int board_i = n-1; board_i >= 0; board_i--){
           string board_temp = Console.ReadLine();
           board[board_i] = board_temp.ToCharArray();
        }
        int q = Convert.ToInt32(Console.ReadLine());
        
        for(int a0 = 0; a0 < q; a0++){
            string[] tokens_x1 = Console.ReadLine().Split(' ');
            int x1t = Convert.ToInt32(tokens_x1[0]);
            int y1 = Convert.ToInt32(tokens_x1[1]) - 1;
            int x2t = Convert.ToInt32(tokens_x1[2]);
            int y2 = Convert.ToInt32(tokens_x1[3]) - 1;
            int x1 = n - x1t, x2 = n - x2t;
            // your code goes here
            int count = 0;
            int[,] visited = new int[n, m];
            for(int i = x1; i >= x2; i--)
            {
                for(int j = y1; j <= y2; j++)
                {
                    if(visited[i, j] == 0)
                    {
                        count++;
                        visited = bfs(board, visited, i, j, x1, y1, x2, y2);
                    }
                    
                }
                //Console.WriteLine("HERE in outer loop");
            }
            //Console.WriteLine("x and y "+x1+y1+x2+y2);
            Console.WriteLine(count);
        }
    }
    
    static int[,] bfs(char[][] board, int[,] visited, int i, int j, int x1, int y1, int x2, int y2)
    {
        Queue<vec> Q = new Queue<vec>();
        var root =  new vec(i, j);
        visited[i, j] = 1;
        Q.Enqueue(root);
        
        while(Q.Count > 0)
        {
            
            var current = Q.Dequeue();
            var neighbours = getNeighbours(current, x1, y1, x2, y2);
            //Console.Write("\nNeighbours of ("+current.x+","+current.y+")"+board[current.x][current.y]+" are ");
            foreach(var n in neighbours)
            {
                //Console.Write(" ("+n.x+","+n.y+")"+board[n.x][n.y]);
                if(visited[n.x, n.y] == 0 && board[current.x][current.y] == board[n.x][n.y])
                {
                    visited[n.x, n.y] = 1;
                    Q.Enqueue(n);
                    //Console.Write(" ("+n.x+","+n.y+") ");
                }
            }
            //Console.WriteLine("HERE");
        }
        return visited;
    }
    
    static List<vec> getNeighbours(vec current,int x1,int y1,int x2,int y2)
    {
        List<vec> nec = new List<vec>();
        int li, lj, gi, gj;
        li = current.x - 1;
        lj = current.y - 1;
        gi = current.x + 1;
        gj = current.y + 1;
        
        if(lj >= y1)
        {
            nec.Add(new vec(current.x, lj));
            if(li >= x2)
            {
                nec.Add(new vec(li, lj));
                nec.Add(new vec(li, current.y));
            }
        }else if(li >= x2)
        {
            nec.Add(new vec(li, current.y));
        }
        if(gi <= x1)
        {
            nec.Add(new vec(gi, current.y));
            if(gj <= y2)
            {
                nec.Add(new vec(gi, gj));
                nec.Add(new vec(current.x, gj));
            }
        }else if(gj <= y2)
        {
            nec.Add(new vec(current.x, gj));
        }
        return nec;
    }
}

public class vec
{
    public int x;
    public int y;
    
    public vec(int a, int b)
    {
        x = a; y = b;
    }
}
