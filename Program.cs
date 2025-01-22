using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Laberinto
{
    public static int anchodellaberinto = 21;
    public static int largodellaberinto = 21;
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

        int anchodelaconsola = Console.WindowWidth;
        int largodelaconsola = largodellaberinto;
        int posiciondellaberinto = (anchodelaconsola - largodelaconsola) / 2;

        for (int i = 0; i < anchodellaberinto; i++)
        {
            Console.Write(new string(' ', posiciondellaberinto));
            for (int j = 0; j < largodellaberinto; j++)
            {    if(Maze[i, j] == 1)
                {
                    Console.BackgroundColor= ConsoleColor.DarkMagenta;
                    Console.ForegroundColor= ConsoleColor.DarkMagenta;
                }
            if(Maze[i, j] == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            if(Maze[i, j] == 5)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                if (Maze[i, j] == 7)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(Maze[i, j] + " " );

               
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
    public static void Piedras(int piedra)
    {
        int cantidadobstaculos = 9;
        int posdelobstaculoX;
        int posdelobstaculoY;
        for(int i = 0;i < cantidadobstaculos; i++)
        {
            do
            {
                posdelobstaculoX = random.Next(3, anchodellaberinto - 3);
                posdelobstaculoY = random.Next(3, largodellaberinto - 3);
            }
            while (Maze[posdelobstaculoX, posdelobstaculoY] == 0 || Maze[posdelobstaculoX, posdelobstaculoY] == 5);


            Maze[posdelobstaculoX, posdelobstaculoY] = piedra;
        }
    }
    public static void Esmeraldas(int cantidadesmeraldas, int esmeralda)
    {
        int positionesmeraldaX;
        int positionesmeraldaY;
        for(int i = 0; i < cantidadesmeraldas; i++)
        {
            do {
            positionesmeraldaX = random.Next(3, anchodellaberinto - 2);
            positionesmeraldaY = random.Next(3, largodellaberinto - 2);
            }


            while (Maze[positionesmeraldaX, positionesmeraldaY] == 1);
            Maze[positionesmeraldaX, positionesmeraldaY] = esmeralda;
        }
    }
    public static void Main(string[] args)
    {

        Generaciondellaberinto();
        Iniciodellaberinto();
        Esmeraldas(15, 5);
        Piedras(7);
        RecorrerMaze();
        
    }

}



