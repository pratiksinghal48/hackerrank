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
        vec start = new vec(0,0);
        vec end = new vec(n-1, m-1);
        for(int i = 0; i < n; i++)
        {
                for(int j = 0; j < m; j++)
                {
                    if(visited[i, j] == 0)
                    {
                        count++;
                        visited = bfs(board, visited, new vec(i,j), start, end, count);
                    }
                }
          }
       /* for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < m; j++)
                Console.Write(board[i][j] + " ");
            Console.WriteLine();
        }*/
        
        
        for(int a0 = 0; a0 < q; a0++){
            string[] tokens_x1 = Console.ReadLine().Split(' ');
            int x2 = n - Convert.ToInt32(tokens_x1[0]);
            int y1 = Convert.ToInt32(tokens_x1[1])-1;
            int x1 = n - Convert.ToInt32(tokens_x1[2]);
            int y2 = Convert.ToInt32(tokens_x1[3])-1;
            int[,] visit = (int[,])visited.Clone();
            for(int x = x1; x <= x2; x++)
            {
                for(int y = y1; y <= y2; y++)
                {
                    //Console.WriteLine("("+x+","+y+")");
                    if(x != x2 && y != y1 && visit[x,y] == visit[x+1, y-1])
                    {
                        visit = bfs(board, visit, new vec(x+1, y-1), new vec(x1, y1), new vec(x2, y2), count, visit[x,y]);
                        count++;
                    }
                }
                //Console.WriteLine("HERE in outer loop");
            }
            var hash = new HashSet<int>();
            for(int x = x1; x <= x2; x++)
            {
                for(int y = y1; y <= y2; y++)
                {
                    hash.Add(visit[x,y]);
                }
                //Console.WriteLine("HERE in outer loop");
            }
            //Console.WriteLine("x and y "+x1+y1+x2+y2);
            Console.WriteLine(hash.Count);
        }
    }
    
    static int[,] bfs(char[][] board, int[,] visited, vec root, vec start, vec end ,int count, int countToCheck = 0)
    {
        Queue<vec> Q = new Queue<vec>();
        visited[root.x, root.y] = count;
        Q.Enqueue(root);
        //Console.Write("\nIn bfs with root ("+root.x+","+root.y+")");
        while(Q.Count > 0)
        {
            
            var current = Q.Dequeue();
            var neighbours = getNeighbours(current, start, end);
            //Console.Write("\nNeighbours of ("+current.x+","+current.y+")"+board[current.x][current.y]+" are ");
            //foreach(var nb in neighbours)
            //    Console.Write(" ("+nb.x+","+nb.y+")"/*+board[nb.x][nb.y]*/);
            
            foreach(var nb in neighbours)
            {
                if(visited[nb.x, nb.y] == countToCheck && board[current.x][current.y] == board[nb.x][nb.y])
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
    
    static List<vec> getNeighbours(vec current,vec start, vec end)
    {
        List<vec> nec = new List<vec>();
        int lx, ly, gx, gy;
        lx = current.x - 1;
        ly = current.y - 1;
        gx = current.x + 1;
        gy = current.y + 1;
        
        if(gx <= end.x)
        {
            nec.Add(new vec(gx, current.y));
            if(gy <= end.y)
            {
                nec.Add(new vec(gx, gy));
                nec.Add(new vec(current.x, gy));
            }
        }else if(gy <= end.y)
        {
            nec.Add(new vec(current.x, gy));
        }
        if(ly >= start.y)
        {
            nec.Add(new vec(current.x, ly));
            if(lx >= start.x)
            {
                nec.Add(new vec(lx, ly));
                nec.Add(new vec(lx, current.y));
            }
        }else if(lx >= start.x)
        {
            nec.Add(new vec(lx, current.y));
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
