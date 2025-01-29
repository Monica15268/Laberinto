using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;
using System.Timers;
using Creaciondeljuego;


namespace Creaciondeljuego
{
    public class Game
    {
        private new List<Personajes> PersonajesAelegir;
        //private Laberinto laberinto;
        private Personajes user1;
        private Personajes user2;
        private Laberinto laberinto;
        //private int Sistemadeturnos = 0; //turnos

        //private int[,] Maze;
        private int eleccion;



        public void Iniciar() //Bucle inicial del juego 
        {
            Console.WriteLine("Hola mi camarada");
            Console.WriteLine("En Fondo de Vikini se desarrollara una competencia en un peligroso laberinto");
            Console.WriteLine("¿Quieres ser parte de esta aventura?");
            Console.WriteLine("1 - Si!, mi capitan, estamos listos");
            Console.WriteLine("2 - No estoy listo, mi capitan, lo siento");
            eleccion = Convert.ToInt32(Console.ReadLine()); //Elegir si entrar o no
            while (eleccion < 1 || eleccion > 2)
            {
                eleccion = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Valor no valido, por favor, introduce una de las opciones validas");
            } //Repetir esto, mientras no sea valido, se repite
              //Debo lanzar una excepcion para cuando se introducen letras y cosas asi 
            if (eleccion == 1)
            {

                Console.Clear();
                CreacionDelMaze();
            }
            if (eleccion == 2)
            {
                Console.Clear();
                Console.WriteLine("Hasta la proxima");
                return;

            }

        }

        public void CreacionDelMaze()
        {
            laberinto = new Laberinto(21, 21);


            laberinto.ElegirJugadores(); //Para elegir jugadores, el metodo esta dentro de la clase laberinto para que no me traiga incovenientes
            laberinto.RecorrerElLaberinto();

        }



    }






    public class Ciclodeljuego
    {

        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Iniciar();
        }
    }
    public class Personajes
    {
        //Clase personaje
        public string Nombre { get; set; }
        public char Simbolo { get; set; }
        public int PosicionX { get; set; }
        public int PosicionY { get; set; }
        public int Vida { get; set; }
        public int Velocidad { get; set; }
        //public List<Habilidad> Habilidades { get; set; }
        public Personajes personajes { get; set; }

        public Personajes(string nombre, char simbolo, int posicionx, int posiciony, int vida, int velocidad)
        {
            Nombre = nombre;
            Simbolo = simbolo;
            posicionx = 1;
            posiciony = 1;
            Vida = vida;
            Velocidad = velocidad;
            //    Habilidades = new List<Habilidad>(); //para agregar personaje
            //}
        }
    }
    public class Laberinto
    {
        private static int anchodellaberinto;
        private static int largodellaberinto;
        private static int[,] Maze = new int[anchodellaberinto, largodellaberinto];
        private static Random random = new Random();
        private Personajes user1;
        private Personajes user2;
        private Game game;
        private new List<Personajes> PersonajesAelegir;


        private int Sistemadeturnos = 0; //turnos


        public Laberinto(int ancho, int largo)
        {
            anchodellaberinto = ancho;
            largodellaberinto = largo;
            Maze = new int[anchodellaberinto, largodellaberinto];

            // Inicializar el laberinto con paredes (1)
            for (int i = 0; i < anchodellaberinto; i++)
                for (int j = 0; j < largodellaberinto; j++)
                    Maze[i, j] = 1;

            Iniciodellaberinto();


        }
        public void CreacionDeLista()
        {
            PersonajesAelegir = new List<Personajes>
            { new Personajes("Patricio", 'L', 1, 1, 45, 2), new Personajes("Gary", 'G', 1, 1, 50, 1), new Personajes("Bob Esponja", 'B', 1, 1, 60, 1),
             new Personajes("Calamardo", 'C', 1, 1, 70, 1), new Personajes("Arenita", 'A', 1, 1, 90, 2), new Personajes("Don Cangrejo", 'D', 1, 1, 100,1 )};

            for (int i = 0; i < PersonajesAelegir.Count; i++)
            {
                var character = PersonajesAelegir[i]; //Para que salga el menu
                Console.WriteLine(": {0} - Nombre : {1} || Vida : {2} || Velocidad : {3}", i + 1, character.Nombre, character.Vida, character.Velocidad);
            }
        }
        public void ElegirJugadores() //Eleccion de jugadores
        {
            Console.WriteLine("Seleccione el personaje para Jugador 1:");
            CreacionDeLista();
            int seleccion1 = Convert.ToInt32(Console.ReadLine()) - 1;
            user1 = PersonajesAelegir[seleccion1];
            user1.PosicionX = 1;
            user1.PosicionY = 1;
            Console.WriteLine("Seleccione el personaje para Jugador 2:");
            CreacionDeLista(); //    Quiero que me salgan todas las opciones, menos la que eligio el jugador 
            int seleccion2 = Convert.ToInt32(Console.ReadLine()) - 1;
            user2 = PersonajesAelegir[seleccion2];
            user2.PosicionX = 1;
            user2.PosicionY = 1;
            //Quitar elemento de la lista
            Console.WriteLine($"Jugador 1 ha elegido a {user1.Nombre}.");
            Console.WriteLine($"Jugador 2 ha elegido a {user2.Nombre}.");
        }

        public static void Iniciodellaberinto()
        {
            int casillainicial = random.Next(1, anchodellaberinto - 1) / 2 * 2 + 1;
            int casillafinal = random.Next(1, largodellaberinto - 2) / 2 * 2 + 1;
            Expansiondecaminos(casillainicial, casillafinal);
            Maze[1, 1] = 0; // Punto de inicio
            Maze[anchodellaberinto - 2, largodellaberinto - 2] = 0; // Punto final
        }







        private static void Expansiondecaminos(int ancho, int largo)
        {
            int[] anchox = { 0, 2, -2, 0 }; //direcciones
            int[] largoY = { -2, 0, 0, 2 };


            for (int i = anchox.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Mezcla(ref anchox[i], ref anchox[j]);
                Mezcla(ref largoY[i], ref largoY[j]);
            }

            for (int i = 0; i < anchox.Length; i++)
            {
                int nuevomovimientoX = ancho + anchox[i];
                int nuevomovimientoY = largo + largoY[i];

                if (nuevomovimientoX > 0 && nuevomovimientoY > 0 &&
                    nuevomovimientoX < anchodellaberinto && nuevomovimientoY < largodellaberinto &&
                    Maze[nuevomovimientoX, nuevomovimientoY] == 1)
                {
                    Maze[ancho + anchox[i] / 2, largo + largoY[i] / 2] = 0; // Abrir camino
                    Maze[nuevomovimientoX, nuevomovimientoY] = 0;

                    Expansiondecaminos(nuevomovimientoX, nuevomovimientoY);
                }
            }
        }
        private static void Mezcla(ref int a, ref int b) //Mezclar direcciones
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public void RecorrerElLaberinto()
        {


            Console.Clear();

            int posiciondellaberinto = (Console.WindowWidth - largodellaberinto * 2) / 2; // Centrar el laberinto

            for (int i = 0; i < anchodellaberinto; i++)
            {
                Console.Write(new string(' ', posiciondellaberinto));
                for (int j = 0; j < largodellaberinto; j++)
                {
                    if (Maze[i, j] == 1)
                    {
                        Console.Write("#" + " "); // Pared

                    }
                    else if (i == user1.PosicionX && j == user1.PosicionY)
                    {
                        Console.Write(user1.Simbolo + " "); // Mostrar Jugador 1
                    }
                    else if (i == user2.PosicionX && j == user2.PosicionY)
                    {
                        Console.Write(user2.Simbolo + " "); // Mostrar Jugador 2
                    }
                    else
                    {
                        Console.Write(" " + " "); // Espacio abierto
                    }
                }
                Console.WriteLine(); // Nueva línea al final de cada fila
            }

            Console.WriteLine($"Turno de: {(Sistemadeturnos == 0 ? "Jugador 1" : "Jugador 2")}"); //Indicacion del turno

        }




    }

}




