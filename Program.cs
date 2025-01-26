using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Spectre.Console;
//public class Characters
//{

//    public string Nombre { get; set; }
//    public int Vida { get; set; }
//    public int Velocidad { get; set; }
//    public int Saltatrampas { get; set; }
//    public int Enfriamiento { get; set; }
//    public int posicionX { get; set; }
//    public int posicionY { get; set; }

//    public int Representacion { get; set; }

//    public Characters(string nombre, int vida, int velocidad, int posicionx, int posiciony, int representacion)
//    {

//        Nombre = nombre;
//        Vida = vida;
//        //Enfriamiento = enfriamiento;
//        //Saltatrampas = saltartrampas;
//        posicionX = posicionx;
//        posicionY = posiciony;
//        Velocidad = velocidad;
//        Representacion = representacion;
//    }
//    public void Move(int nuevaposicionX, int nuevaposicionY)
//    {


//        while (true)
//        {

//            int nuevaposicionX = seleccion1.posicionX;
//            int nuevaposicionY = seleccion1.posicionY;

//    public void Life(int vida)
//    {
//        vida = this.Vida;
//    }

//}
public class Laberinto
{
    public static int anchodellaberinto = 23;
    public static int largodellaberinto = 23;
    public static int[,] Maze = new int[anchodellaberinto, largodellaberinto];
   public static Random random = new Random();
   public static string[] nombres = { "Charly", "Johayron", "Miley Cyrus", "Andy Black", "Bon Jovi" };
   public static char[] Simbologia = { 'C', 'J', 'M', 'A', 'B' };
   public static int[] PosX = { 1, 1, 1, 1, 1 };
    public static int[] PosY = { 1, 1, 1, 1, 1 };


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

    public static void RecorrerMaze(char[] Simbologia, string[] nombres,int[] Posx, int[] PosY )
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


        //public static void Selecciones(int character)
        //{
        //     posx = 1;
        //     posy = 1;
        
        //    Maze[posx, posy] = character;
        //}
    public static void Movimientos(int g, int[] Posx, int[] Posy)
    {
        int posicionnuevax = Posx[g]; //para el mov x
        int posicionnuevay = Posy[g]; // para el mov y
        
        ConsoleKeyInfo tecla = Console.ReadKey(true);
        switch (tecla.Key)
        {
            case ConsoleKey.W:
                posicionnuevay--;
                break;
            case ConsoleKey.S:
                posicionnuevay++;
                break;
            case ConsoleKey.D:
               posicionnuevax++;
                break;
            case ConsoleKey.A:
                posicionnuevax--;
                break;
            

        }
        if (posicionnuevax> 0 && posicionnuevay > 0 && posicionnuevax < anchodellaberinto && posicionnuevay < anchodellaberinto)
        {
            Posx[g] = posicionnuevax;
            Posy[g] = posicionnuevay;
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

        string[] nombres = { "Charly", "Johayron", "Miley Cyrus", "Andy Black", "Bon Jovi" };
        char[] Simbologia = { 'C', 'J', 'M', 'A', 'B' };
        Console.WriteLine("Elige un personaje");
        for (int i = 0; i < nombres.GetLength(0); i++)
        {

            Console.WriteLine(" {0} - Nombre : {1}  Simbologia : {2}  ", i + 1, nombres[i], Simbologia[i]);

        }
        Console.WriteLine("Escoge los personajes");
        int seleccionadoone = Convert.ToInt32(Console.ReadLine());
        int seleccionadotwo = Convert.ToInt32(Console.ReadLine());
        while (seleccionadoone < 0 || seleccionadotwo < 0 || seleccionadoone > nombres.Length || seleccionadotwo > nombres.Length)
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

        Selecciondepersonajes();
        //int Characterone = seleccionadoone;
        //char Characterone = 'M';
      //Characterone = nombres[seleccionadoone - 1];
            //    Characters seleccion1 = personajes[seleccionadoone - 1];
            //    Characters seleccion2 = personajes[seleccionadotwo - 1];


            Iniciodellaberinto();
        Esmeraldas(15, 5);
       
        Piedras(7);

        RecorrerMaze(Simbologia, nombres, PosX, PosY);
       
      
    
  
   

}
    }













