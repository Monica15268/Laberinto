using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Laberinto
{
    public static int anchodellaberinto = 31;
    public static int largodellaberinto = 27;
    public static int[,] Maze = new int[anchodellaberinto, largodellaberinto];
    static Random random = new Random();

    public static void Generaciondellaberinto()
    {
        for (int i = 0; i < Maze.GetLength(0); i++)
        
            for (int j = 0; j < Maze.GetLength(1); j++)
            
                Maze[i, j] = 1; //Se llenea el laberinto de paredes
            

        
    }



    public static void Expansiondecaminos(int ancho, int largo)
    {
        int[] anchox = { 0, 2, -2, 0 }; //direcciones
        int[] largoY = { -2, 0, 0, 2 };


        for (int i = anchox.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);  //intercambiar filas y columnas
            var tempAncho = anchox[i];
            anchox[i] = anchox[j];
            anchox[j] = tempAncho;

            var tempLargo = largoY[i];
            largoY[i] = largoY[j];
            largoY[j] = tempLargo;
        }



        for (int i = 0; i < anchox.Length; i++)
        {
            int nuevomovimientoX = ancho + anchox[i];
            int nuevomovimientoY = largo + largoY[i];

            if (nuevomovimientoX > 0 && nuevomovimientoY > 0 && nuevomovimientoX < anchodellaberinto && nuevomovimientoY < largodellaberinto && Maze[nuevomovimientoX, nuevomovimientoY] == 1)
            {
                Maze[ancho + anchox[i] / 2, largo + largoY[i] / 2] = 0; //abrir camino
                Maze[nuevomovimientoX, nuevomovimientoY] = 0;

                Expansiondecaminos(nuevomovimientoX, nuevomovimientoY);
            }
        }
    }



    public static void Iniciodellaberinto()
    {

        int casillainicial = random.Next(1, anchodellaberinto - 1) / 2 * 2 + 1;
        int casillafinal = random.Next(1, largodellaberinto - 2) / 2 * 2 + 1;
        Expansiondecaminos(casillainicial, casillafinal);//empezar desde aca
        Maze[1, 1] = 0;
        Maze[anchodellaberinto - 1, largodellaberinto - 2] = 0;
    }

    public static void RecorrerMaze()
    {
        for (int i = 0; i < anchodellaberinto; i++)
        {
            for (int j = 0; j < largodellaberinto; j++)
            {
                Console.Write(Maze[i, j]);
            }
            Console.WriteLine();
        }
    }
    public static void Main(string[] args)
    {

        Generaciondellaberinto();
        Iniciodellaberinto();
        RecorrerMaze();
    }

}



