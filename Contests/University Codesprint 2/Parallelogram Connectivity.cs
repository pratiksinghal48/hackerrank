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
        int[,] visited = new int[n,m];
        int count = 0;
        for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(visited[i, j] == 0)
                    {
                        count++;
                        visited = bfs(board, visited, i, j, n-1, m-1, count);
                    }
                }
          }
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < m; j++)
                Console.Write(board[i][j] + " ");
            Console.WriteLine();
        }
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < m; j++)
                Console.Write(visited[i,j] + " ");
            Console.WriteLine();
        }
        
        for(int a0 = 0; a0 < q; a0++){
            string[] tokens_x1 = Console.ReadLine().Split(' ');
            int x1 = n - Convert.ToInt32(tokens_x1[0]);
            int y1 = Convert.ToInt32(tokens_x1[1])-1;
            int x2 = n - Convert.ToInt32(tokens_x1[2]);
            int y2 = Convert.ToInt32(tokens_x1[3])-1;
            // your code goes here
            var hash = new HashSet<int>();
            int
            for(int i = x2; i <= x1; i++)
            {
                for(int j = y1; j <= y2; j++)
                {
                   hash.Add(visited[i,j]);
                }
                //Console.WriteLine("HERE in outer loop");
            }
            //Console.WriteLine("x and y "+x1+y1+x2+y2);
            Console.WriteLine(hash.Count);
        }
    }
    
    static int[,] bfs(char[][] board, int[,] visited, int i, int j, int n, int m ,int count)
    {
        Queue<vec> Q = new Queue<vec>();
        var root =  new vec(i, j);
        visited[i, j] = count;
        Q.Enqueue(root);
        //Console.Write("\nIn bfs with root ("+root.x+","+root.y+")");
        while(Q.Count > 0)
        {
            
            var current = Q.Dequeue();
            var neighbours = getNeighbours(current, n, m);
            //Console.Write("\nNeighbours of ("+current.x+","+current.y+")"+board[current.x][current.y]+" are ");
            foreach(var nb in neighbours)
            {
                //Console.Write(" ("+n.x+","+n.y+")"+board[n.x][n.y]);
                if(visited[nb.x, nb.y] == 0 && board[current.x][current.y] == board[nb.x][nb.y])
                {
                    visited[nb.x, nb.y] = count;
                    Q.Enqueue(nb);
                    //Console.Write(" ("+nb.x+","+nb.y+")"+board[nb.x][nb.y]);
                }
            }
            //Console.WriteLine("HERE");
        }
        return visited;
    }
    
    static List<vec> getNeighbours(vec current,int n, int m)
    {
        List<vec> nec = new List<vec>();
        int li, lj, gi, gj;
        li = current.x - 1;
        lj = current.y - 1;
        gi = current.x + 1;
        gj = current.y + 1;
        
        if(lj >= 0)
        {
            nec.Add(new vec(current.x, lj));
            if(li >= 0)
            {
                nec.Add(new vec(li, lj));
                nec.Add(new vec(li, current.y));
            }
        }else if(li >= 0)
        {
            nec.Add(new vec(li, current.y));
        }
        if(gi <= n)
        {
            nec.Add(new vec(gi, current.y));
            if(gj <= m)
            {
                nec.Add(new vec(gi, gj));
                nec.Add(new vec(current.x, gj));
            }
        }else if(gj <= m)
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
