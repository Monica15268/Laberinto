using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Spectre.Console;

public class Laberinto
{
    public static int anchodellaberinto = 23;
    public static int largodellaberinto = 23;
    public static int[,] Maze = new int[anchodellaberinto, largodellaberinto];
   public static Random random = new Random();
  


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



        for (int i = 0; i < anchox.Length; i++) //una sola porque ambas tienen la misma extension
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

    public static void RecorrerMaze ( int [,] Maze ,List<Personajes> elecciones )
    {
       
        Console.Clear();
        
        int anchodelaconsola = Console.WindowWidth;
        int largodelaconsola = largodellaberinto;
        int posiciondellaberinto = (anchodelaconsola - largodelaconsola*2) / 2; //centrar el laberinto en el medio
       
        for (int i = 0; i < anchodellaberinto; i++)
        {
            Console.Write(new string(' ', posiciondellaberinto));
            for (int j = 0; j < largodellaberinto; j++)
            {
             
              
                

                if (Maze[i, j] == 9)
                {
                   Console.BackgroundColor = Color.Green;
                    Console.ForegroundColor = Color.Green;
                }
            
               if(Maze[i, j] == 1) //diseñar laberinto
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
                if (Maze[i, j] == 4){
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                if(Maze[i, j] == 'M')
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                bool ocupado = false;
                foreach(Personajes avatars in elecciones)
                {
                    if (avatars.posicionX == i &&  avatars.posicionY == j) {
                        Console.Write(avatars.Nombre.Substring(0, 1));
                        ocupado = true; break; }
                    if (!ocupado)
                    {
                        Console.Write(Maze[i, j] + " ");
                    }
             
                  
                


                Console.ResetColor();
            }
            Console.WriteLine();
            
        }

        }



    }

   


    public static void Piedras(int piedra) //obstaculos
    {
        int cantidadobstaculos = 9;
        int posdelobstaculoX;
        int posdelobstaculoY;
        for(int i = 0;i < cantidadobstaculos; i++) //ciclo para que me ponga la cantidad de piedras que le indique
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
    public static void Esmeraldas(int cantidadesmeraldas, int esmeralda) //lo que debe recoger el jugador
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

        public static void Selecciondepersonajes()
    {

        List<Personajes> elecciones = new List<Personajes>();
        elecciones.Add(new Personajes { Nombre = "Calamardo", posicionX = 1, posicionY = 1, vida = 100, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Bob Esponja", posicionX = 1, posicionY = 2, vida = 50, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Patricio", posicionX = 1, posicionY = 1, vida = 75, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Don Cangrejo", posicionX = 1, posicionY = 2, vida = 90, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Camgreburger", posicionX = 1, posicionY = 1, vida = 30, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes{         Nombre = "Juanita",posicionX = 1,posicionY = 2, vida = 70, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        Console.WriteLine("Elige un personaje");
        for (int i = 0; i < elecciones.Count; i++)
        {

            Console.WriteLine(" {0} - Nombre : {1}  vida : {2}  ", i + 1, elecciones[0],elecciones[3]);

        }
        Console.WriteLine("Escoge los personajes");
        int seleccionadoone = Convert.ToInt32(Console.ReadLine());
        int seleccionadotwo = Convert.ToInt32(Console.ReadLine());
        while (seleccionadoone < 0 || seleccionadotwo < 0 || seleccionadoone > elecciones.Count || seleccionadotwo > elecciones.Count)
        {
            Console.WriteLine("Introduce de nuevo el personaje");
            seleccionadoone = Convert.ToInt32(Console.ReadLine());
            seleccionadotwo = Convert.ToInt32(Console.ReadLine());
        }
        //Debo arreglarle los limites a esta parte
    }
   
        

    
public static void Main(string[] args) //basicamente como funciona el proyecto
    {
        Generaciondellaberinto();

        Iniciodellaberinto();
        Esmeraldas(15, 5);

        Piedras(7);
        Selecciondepersonajes();
        Console.Clear();
        while (true) { 
        List <Personajes> elecciones = new List<Personajes>();
        elecciones.Add(new Personajes { Nombre = "Calamardo" , posicionX = 1, posicionY = 1, vida = 100, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Bob Esponja", posicionX = 1, posicionY = 2, vida = 50, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Patricio", posicionX = 1, posicionY = 1, vida = 75, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Don Cangrejo", posicionX = 1, posicionY = 2, vida = 90, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Camgreburger", posicionX = 1, posicionY = 1, vida = 30, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
        elecciones.Add(new Personajes { Nombre = "Juanita", posicionX = 1, posicionY = 2, vida = 70, arriba = ConsoleKey.UpArrow, abajo = ConsoleKey.DownArrow, izquierda = ConsoleKey.LeftArrow, derecha = ConsoleKey.RightArrow });
     
      

     
    
   
        
        RecorrerMaze(Maze, elecciones);
            ConsoleKeyInfo tecla = Console.ReadKey();

            foreach(Personajes personajes in elecciones) {
                if (tecla.Key == personajes.arriba)
                {
                    personajes.Arriba(23);
                }
                if(tecla.Key == personajes.abajo)
                {
                    personajes.Abajo(23);
                }
                if(tecla.Key == personajes.izquierda)
                {
                    personajes.Izquierda(23);

                }
                if (tecla.Key == personajes.derecha)
                {
                    personajes.Derecha(23);
                }
            }

        }




    }
}
public class Personajes
{
    public string Nombre;
    public int posicionX;
    public int posicionY;
    public int vida;
    public ConsoleKey arriba, abajo, derecha, izquierda;

    public void Arriba(int altodelalberinto)
    {
        if(posicionY > 0)
        {
            posicionY--;
        }
    }
    public void Abajo( int altodellaberinto)
    {
        if(posicionY < 0)
        {
            posicionY++;    
        }
    }
    public void Izquierda (int anchodellaberinto)
    {
        if(posicionX > 0)
        {
            posicionX--;
        }
    }
    public void Derecha(int anchodellaberinto)
    {
        if(posicionX < 0)
        {
            posicionX--;
        }
    }

}








