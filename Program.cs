using System;
using System.Collections.Generic;

public class Laberinto
{
    public static int anchodellaberinto = 31;
    public static int largodellaberinto = 27;
    public static int[,] Maze = new int[anchodellaberinto, largodellaberinto];
    static Random random = new Random();

   public static void Generaciondellaberinto()
    {
        for(int i = 0;i <Maze.GetLength(0);i++)
        {
            for(int j = 0;j < Maze.GetLength(1); j++)
            {
              Console.Write( Maze[i, j] = 1);
            }
            Console.WriteLine();
        }
    }
    public static void Main(string[] args)
    {

        Generaciondellaberinto();
   
    }

}


