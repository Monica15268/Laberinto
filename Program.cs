


using System;
using System.Collections.Generic;
using System.ComponentModel;
using Spectre.Console;
namespace Creaciondeljuego {
public class Laberinto
{
     private static int anchodellaberinto = 23;
    private  static int largodellaberinto = 23;
        private static int[,] Maze = new int[anchodellaberinto, largodellaberinto];
        private static Random random = new Random();
       private  Personajes user1;
        private  Personajes user2;
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







        public void SeleccionarPersonaje()
        {
            //Lista de personajes
            List<Personajes> elecciones = new List<Personajes>  { new Personajes (  "Calamardo",  'C', 1,  1,100 ), new Personajes ("Bob Esponja",  'B',  1,  2,  50 ), new Personajes (  "Patricio", 'P',  1,  1,  75 ), new Personajes ("Don Cangrejo",  'D',  1,  1,  90 ), new Personajes (  "Camgreburger",  'C', 1,  1,  30 ), new Personajes (  "Juanita",  'J',  1,  1,  70 ) };
            Console.WriteLine("Elijan sus personaje");
            for (int i = 0; i < elecciones.Count; i++)
            {
                Console.WriteLine(" {0} - Nombre : {1} " + " Vida: {2}", i + 1, elecciones[i].Nombre, elecciones[i].Vida); //Mostrar Menu con personajes 
            }
            List<Personajes> seleccionados = new() { };
            while (seleccionados.Count < 2)
            {
                int seleccionuser1 = Convert.ToInt32(Console.ReadLine()); //Comprobar si el 1er personaje es valido 
                while (seleccionuser1 < 0 || seleccionuser1 > elecciones.Count)
                {
                    Console.WriteLine("Debes introducir uno de los valores de la lista");
                    seleccionuser1 = Convert.ToInt32(Console.ReadLine());
                }
                //Basicamente la eleccion,todavia falta hacer que el usuario siempre introduzca un numero valido
                var selecciondelusuario = elecciones[seleccionuser1 - 1]; //Se escoge el numero y se le resta  1 para acceder al elemento correspondiente
                if (!seleccionados.Contains(selecciondelusuario))
                {
                    seleccionados.Add(selecciondelusuario);
                }

            }

            user1 = seleccionados[0];
            user2 = seleccionados [1];
            user1.PosicionX = 1;
            user1.PosicionY = 1;
            user2.PosicionX = 1;
            user2.PosicionY = 1;

            Sistemadeturnos = 0;
        }

    public static void Piedras(int piedra) //obstaculos
    {
        int cantidadobstaculos = 9;
        int posdelobstaculoX;
        int posdelobstaculoY;
        for (int i = 0; i < cantidadobstaculos; i++) //ciclo para que me ponga la cantidad de piedras que le indique
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
        for (int i = 0; i < cantidadesmeraldas; i++)
        {
            do
            {
                positionesmeraldaX = random.Next(3, anchodellaberinto - 2);
                positionesmeraldaY = random.Next(3, largodellaberinto - 2);
            }


            while (Maze[positionesmeraldaX, positionesmeraldaY] == 1);
            Maze[positionesmeraldaX, positionesmeraldaY] = esmeralda;
        }
    }





        //Modificar esta parte despues

        public void Mover(ConsoleKeyInfo tecla)
    {
            int nuevaposicionuser1X = user1.PosicionX; //Asignar posiciones, y como son 2 jugadores, entonces cada uno tendra una posicion unica
            int nuevaposicionuser1Y = user1.PosicionY;
            int nuevaposicionuser2X = user2.PosicionX;
            int nuevaposicionuser2Y = user2.PosicionY;

            if(Sistemadeturnos == 0) {
                switch (tecla.Key)
                {
                    case ConsoleKey.W: nuevaposicionuser1X--; break; // Arriba
                    case ConsoleKey.S: nuevaposicionuser1X++; break; // Abajo
                    case ConsoleKey.A:  nuevaposicionuser1Y--; break; // Izquierda
                    case ConsoleKey.D: nuevaposicionuser1Y++; break; // Derecha
                    default: return;
                }
            }
            if(Sistemadeturnos == 1)
            {
                switch(tecla.Key)
                {
                    case ConsoleKey.UpArrow: nuevaposicionuser2X--; break;
                    case ConsoleKey.DownArrow: nuevaposicionuser2X++; break;
                    case ConsoleKey.LeftArrow: nuevaposicionuser2Y--; break;
                    case ConsoleKey.RightArrow: nuevaposicionuser2Y++; break;
                    default: return;
                }
            }
            if (nuevaposicionuser1X > 0 && nuevaposicionuser1X < anchodellaberinto && nuevaposicionuser1Y > 0 && nuevaposicionuser1Y < largodellaberinto && Maze[nuevaposicionuser1X, nuevaposicionuser1Y] == 0 && nuevaposicionuser2X > 0 && nuevaposicionuser2X < anchodellaberinto && nuevaposicionuser2Y > 0 && nuevaposicionuser2Y < largodellaberinto && Maze[nuevaposicionuser2X, nuevaposicionuser2Y] == 0)
            {
                user1.PosicionX = nuevaposicionuser1X;
               user1.PosicionY = nuevaposicionuser1Y;
                user2.PosicionX = nuevaposicionuser2X;
                user2.PosicionY = nuevaposicionuser2Y;

            }

           Sistemadeturnos = (Sistemadeturnos + 1) % 2;
        }
    
        public void RecorrerMaze()
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
                        Console.Write("#"); // Pared
                        
                    }
                    else if (i == user1.PosicionX && j == user1.PosicionY)
                    {
                        Console.Write(user1.Simbolo); // Mostrar Jugador 1
                    }
                    else if (i == user2.PosicionX && j == user2.PosicionY)
                    {
                        Console.Write(user2.Simbolo); // Mostrar Jugador 2
                    }
                    else
                    {
                        Console.Write(" "); // Espacio abierto
                    }
                }
                Console.WriteLine(); // Nueva línea al final de cada fila
            }

            Console.WriteLine($"Turno de: {(Sistemadeturnos == 0 ? "Jugador 1" : "Jugador 2")}"); //Indicacion del turno

                }

            




public static void Main(string[] args) //basicamente como funciona el proyecto
   
     {
            Laberinto laberinto = new Laberinto(21, 21);

            laberinto.SeleccionarPersonaje();

            while (true)
            {
                laberinto.RecorrerMaze();
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.Escape) break; // Salir del juego
                laberinto.Mover(tecla);
















            }
}

      

    }
    public class Habilidad
    {
        public string Nombre { get; set; }
        public int Daño { get; set; }

        public Habilidad(string nombre, int daño)
        {
            Nombre = nombre;
            Daño = daño;
        }

        public void Uso()   //invocar uso y que diga el nombre y el daño
        {


            Console.WriteLine("Se uso habilidad {0} causando daño de {1}", Nombre, Daño);
        }

    }
    public class Personajes
{
    //Clase personaje
    public string Nombre {  get; set; }
     public char Simbolo { get; set; }
     public int PosicionX { get; set; }
        public int PosicionY { get; set; }
      public int Vida { get; set; }
            public List <Habilidad> Habilidades { get; set; }
      
        public Personajes(string nombre, char simbolo, int posicionx, int posiciony, int vida)
        {
            Nombre = nombre;
            Simbolo = simbolo;
            PosicionX = posicionx;
            PosicionY = posiciony;
             Vida = vida;
                Habilidades = new List<Habilidad>(); //para agregar personaje
        }
            public void Agregarhabilidad(Habilidad habilidad) //Agregar habilidad a cada personaje
            {
                Habilidades.Add(habilidad);
            }
        public void UsarHabilidad(int index) //Esto es para invocarla,dependiendo del personaje
        {
            if (index >= 0 && index < Habilidades.Count)
            {
                Habilidades[index].Uso();
            }
           
        }

    }

     
    }




