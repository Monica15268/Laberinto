using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Spectre.Console;
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

    public static void RecorrerMaze()
    {
        
        
        int anchodelaconsola = Console.WindowWidth;
        int largodelaconsola = largodellaberinto;
        int posiciondellaberinto = (anchodelaconsola - largodelaconsola) / 2; //centrar el laberinto en el medio
        StringBuilder Introducirlaberintodentrodelpanel = new StringBuilder();
        for (int i = 0; i < anchodellaberinto; i++)
        {
            Console.Write(new string(' ', posiciondellaberinto));
            for (int j = 0; j < largodellaberinto; j++)
            {    if(Maze[i, j] == 1) //diseñar laberinto
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
                Console.Write(Maze[i, j] + " ");
                
               
                Console.ResetColor();
            }
            Console.WriteLine();
            
        }
        //var panel = new Panel("Land");
        //{ 
        //panel.Width = largodellaberinto * 2;
        //panel.Height = anchodellaberinto * 2;
        //panel.Header = new PanelHeader("Laberinto");
        //panel.Border = BoxBorder.Heavy;
        //}
        //panel.Add(new Text(Introducirlaberintodentrodelpanel.ToString()));

        //AnsiConsole.Write(panel);
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
    public static void Seleccion(){
        List<Characters> personajes = new List<Characters>();
        {
            new Characters("Goro", 60, 1 );
            new Characters("Paco", 70,  3 );
            new Characters("Juan", 100, 7 );
        }
        Console.WriteLine("Elige un personaje");
        for(int i = 0;i < personajes.Count; i++)
        {
            Console.Write($"{i + 1}.{personajes[i].Nombre}"  + personajes[i].Vida);
        }
        int seleccionadoone;
        int seleccionadotwo;
        while (true)
        {
            seleccionadoone = Convert.ToInt32(Console.ReadLine());
            seleccionadotwo = Convert.ToInt32(Console.ReadLine());

            if(seleccionadoone > 0 && seleccionadoone < personajes.Count && seleccionadotwo> 0 && seleccionadotwo < personajes.Count) {
                break;
            }
            Console.WriteLine("Introduce otro numero");
        }
            
        Characters seleccion1 = personajes[seleccionadoone - 1];
        Characters seleccion2 = personajes[seleccionadotwo - 1];
}
    public static void Selecciondel1eerpersonaje(int seleccion1)
    {
        
        int posicionseleccion1;
        int posicionseleccion2;
        do
        {
            posicionseleccion1 = random.Next(1, 3);
            posicionseleccion2 = random.Next(1, 3);

            Maze[posicionseleccion1, posicionseleccion2] = seleccion1;
        }
        while (Maze[posicionseleccion1, posicionseleccion2] != 0);


    }

    public static void Selecciondel1eerpersonaje2(int seleccion2)
    {
       
        int posicionseleccion1;
        int posicionseleccion2;
        do
        {
            posicionseleccion1 = random.Next(1, 3);
            posicionseleccion2 = random.Next(1, 3);

            Maze[posicionseleccion1, posicionseleccion2] = seleccion2;
        }
        while (Maze[posicionseleccion1, posicionseleccion2] != 0);


    }

    public static void Main(string[] args) //basicamente como funciona el proyecto
    {
       
        Generaciondellaberinto();
        Iniciodellaberinto();
        Esmeraldas(15, 5);
        Selecciondel1eerpersonaje(3);
        Selecciondel1eerpersonaje2(9);
        Piedras(7);
        RecorrerMaze();
        
    }

}


public class Characters
{

    public string Nombre { get; set; }
    public int Vida { get; set; }
    public int Velocidad {  get; set; }
    public int Saltatrampas { get; set; }
    public int Enfriamiento { get; set; }
    //public int PosicionGorox { get; set; }
    //public int PosicionGoroy { get; set; }

    public Characters(string nombre, int vida,   int velocidad)
    {
    
        Nombre = nombre;
        Vida = vida;
        //Enfriamiento = enfriamiento;
        //Saltatrampas = saltartrampas;
        //PosicionGorox = posiciongorox;
        //PosicionGoroy = posiciongoroy;
        Velocidad = velocidad;
    }
   
}



public class Charatcer
{
    public string Nombre { get; set; }
    public int Vida { get; set; }
    public int Rapidez { get; set; }
    public int Enfriamiento { get; set; }
    public int PosicionMariano { get; set; }
    public int PosicionMarianoy { get; set; }
}


