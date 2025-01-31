using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;
using System.Timers;
using Creaciondeljuego;
using Spectre.Console;


namespace Creaciondeljuego
{
    public class Game
    {
        private new List<Personajes> PersonajesAelegir; //Crear lista para elegir personajes
        //private Laberinto laberinto;
        private Personajes user1;  //Personaje 1
        private Personajes user2; //Personaje 2 
        private Laberinto laberinto;

        private int eleccion;



        public void Iniciar() //Bucle inicial del juego 
        {

            string texto = "Hola mi camarada";

            foreach (var Text in texto)
            {
                AnsiConsole.Write(Text); //Para separar las letras 
                Thread.Sleep(100); //Tiempo que debe demorar
            }
            Console.WriteLine();
            string segundotexto = "En Fondo de Vikini se desarrollara una competencia en un peligroso laberinto";
            foreach (var segundoTexto in segundotexto)
            {


                AnsiConsole.Write(segundoTexto); //Para separar las letras 

                Thread.Sleep(100); //Tiempo que debe demorar
            }
            Console.WriteLine();
            string tercertexto = "¿Quieres ser parte de esta aventura?";
            foreach (var Tercertexto in tercertexto)
            {


                AnsiConsole.Write(Tercertexto); //Para separar las letras 

                Thread.Sleep(100); //Tiempo que debe demorar
            }
            Console.WriteLine();
            string cuartotexto = "1 - Si!, mi capitan, estamos listos";

            foreach (var cuartoTexto in cuartotexto)
            {
                AnsiConsole.Write(cuartoTexto);
            }
            Console.WriteLine();
            string quintotexto = "2 - No estoy listo, soy debil";
            foreach (var quintoTexto in quintotexto)
            {
                AnsiConsole.Write(quintoTexto);

            }
            Console.WriteLine();
            while (eleccion < 1 || eleccion > 2) //Hacerlo mientras no se cumpla eso
            {
                try
                {
                    eleccion = Convert.ToInt32(Console.ReadLine()); //Elegir si entrar o no



                    Console.WriteLine("Valor no valido, por favor, introduce una de las opciones validas");
                    //Repetir esto, mientras no sea valido, se repite
                    //Debo lanzar una excepcion para cuando se introducen letras posicionydelatrampa cosas asi 
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
                catch (Exception ex)
                {
                    Console.WriteLine("Caracter no valido"); //Cachar error posicionydelatrampa voler a repetir el bucle

                }
            }

        }

        public void CreacionDelMaze()
        {
            laberinto = new Laberinto(21, 21);


            laberinto.ElegirJugadores(); //Para elegir jugadores, el metodo esta dentro de la clase laberinto para que no me traiga incovenientes
            laberinto.Avancedeljuego(); //Aca adentro anda un ciclo para correr parte del juego

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
            ColocarTrampas();

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
            string Seleccionar = "Seleccione el personaje para Jugador 1:";
            foreach (var seleccionar in Seleccionar) //Para que salga poco poco 
            {
                AnsiConsole.Write(seleccionar);
                Thread.Sleep(100);
            }

            Console.WriteLine();
            CreacionDeLista();
            int seleccion1 = CaracterCorrecto(PersonajesAelegir.Count) - 1;
            user1 = PersonajesAelegir[seleccion1];
            user1.PosicionX = 1;
            user1.PosicionY = 1;




            string Seleccionar2 = "Seleccione el personaje para Jugador 2";

            Console.WriteLine();
            foreach (var seleccionar2 in Seleccionar2) //Para que salga poco poco 
            {
                AnsiConsole.Write(seleccionar2);
                Thread.Sleep(100);
            }
            Console.WriteLine();
            CreacionDeLista();

            int seleccion2 = CaracterCorrecto(PersonajesAelegir.Count) - 1;
            user2 = PersonajesAelegir[seleccion2];
            user2.PosicionX = 1;
            user2.PosicionY = 1;

            Console.WriteLine("El personajes 1 ha escogido a   {0} ", user1.Nombre);

            Console.WriteLine("El personajes 2 ha escogido a   {0} ", user2.Nombre); //Para mostrar selecciones
            Thread.Sleep(10000);
            //Poner barra de progreso
        }



        private int CaracterCorrecto(int personajess)
        {
            int seleccion1 = 0;
            bool caractervalido = false;

            while (!caractervalido)
            {
                try
                {
                    string input = Console.ReadLine(); //Leer texto
                    seleccion1 = int.Parse(input); // Intentar convertir a un número

                    if (seleccion1 >= 1 && seleccion1 <= personajess)
                    {
                        caractervalido = true; // Vakido
                    }
                    else
                    {
                        Console.WriteLine("Introduce un numero entre 1  posicionydelatrampa {0}", personajess);
                    }
                }
                catch (FormatException) // Captura excepciones si la entrada no es un número
                {
                    Console.WriteLine("Entrada no válida. Por favor, introduce un número.");
                }
                catch (Exception ex) // Captura cualquier otra excepción
                {
                    Console.WriteLine("Caracter no valido");
                }
            }

            return seleccion1; // Retorna la selección válida
        }



        private void ColocarTrampas()
        {
            // Trampa que quita vida 
            ColocarTrampaAleatoria(5);

            // Trampa que quita vida posicionydelatrampa reduce velocidad 
            ColocarTrampaAleatoria(7);

            // Trampa que reduce velocidad 
            ColocarTrampaAleatoria(9);
        }

        private void ColocarTrampaAleatoria(int tipodeTrampa)
        {
            int posicionxdelatrampa, posicionydelatrampa; //posicion de la trampa en el laberinto
            do
            {
                posicionxdelatrampa = random.Next(1, anchodellaberinto - 1);
                posicionydelatrampa = random.Next(1, largodellaberinto - 1);
            } while (Maze[posicionxdelatrampa, posicionydelatrampa] != 0); // Asegurarse de que la casilla esté vacía

            Maze[posicionxdelatrampa, posicionydelatrampa] = tipodeTrampa; // Colocar la trampa
        }
        private void AplicarEfectoTrampa(Personajes personajes) //Que le caigs el efecto
        {
            int celdaActual = Maze[personajes.PosicionX, personajes.PosicionY];

            switch (celdaActual)
            {
                case 5: // Trampa que quita vida
                    personajes.Vida -= 10;
                    Console.WriteLine("El personajes {0} ha caido en una trampa.Pierde 10 de vida ", personajes.Nombre);
                    MostrarEstadoJugador(personajes);
                    MoverTrampaAleatoria(personajes.PosicionX, personajes.PosicionY, 5);
                    break;

                case 7: // Trampa que quita vida nuevaposiciondelatrampaY reduce velocidad
                    int velocidadOriginal = personajes.Velocidad;
                    personajes.Velocidad = 1;
                    personajes.Vida -= 30;
                    Console.WriteLine("El personajes {0} ha caido en una trampa.Pierde 10 de vida . Velocidad reducida a 1 temporalmente", personajes.Nombre);
                    MostrarEstadoJugador(personajes);
                    RestaurarVelocidad(personajes, velocidadOriginal);
                    MoverTrampaAleatoria(personajes.PosicionX, personajes.PosicionY, 7);
                    break;

                case 9: // Trampa que reduce velocidad
                    velocidadOriginal = personajes.Velocidad;
                    personajes.Velocidad = 3;
                    Console.WriteLine("El personajes {0} ha caido en una trampa. Velocidad reducida a 2 temporalmente", personajes.Nombre);
                    MostrarEstadoJugador(personajes);
                    RestaurarVelocidad(personajes, velocidadOriginal);
                    MoverTrampaAleatoria(personajes.PosicionX, personajes.PosicionY, 9);
                    break;
            }
        }

        private void MoverTrampaAleatoria(int nuevaposiciondelatrampaX, int nuevaposiciondelatrampaY, int tipoTrampa)
        {
            Maze[nuevaposiciondelatrampaX, nuevaposiciondelatrampaY] = 0; // Eliminar la trampa de la posición actual
            ColocarTrampaAleatoria(tipoTrampa); // Colocar la trampa en una nueva posición aleatoria
        }

        private async void RestaurarVelocidad(Personajes jugador, int velocidadOriginal)
        {
            await Task.Delay(10000); //Simular tiempo
            jugador.Velocidad = velocidadOriginal;
            Console.WriteLine("El personajes {0} ha recuperado su velocidad{1}", jugador.Nombre, velocidadOriginal);
        }

        private void MostrarEstadoJugador(Personajes personajes)
        {
            Console.WriteLine("Nombre : {0} Vida : {1}  Velocidad {2}", personajes.Nombre, personajes.Vida, personajes.Velocidad);
        }


        public void MoverUser1(char direccion)
        {
            int deltaX = 0, deltaY = 0;

            // Determinar la dirección basada en la tecla presionada
            switch (direccion)
            {
                case 'W': deltaX = -1; break; // Arriba
                case 'S': deltaX = 1; break;  // Abajo
                case 'A': deltaY = -1; break; // Izquierda
                case 'D': deltaY = 1; break;  // Derecha
            }

            MoverJugadorConVelocidad(user1, deltaX, deltaY);
        }

        public void MoverUser2(ConsoleKey direccion)
        {
            int deltaX = 0, deltaY = 0;

            // Determinar la dirección basada en la tecla presionada
            switch (direccion)
            {
                case ConsoleKey.UpArrow: deltaX = -1; break;    // Arriba
                case ConsoleKey.DownArrow: deltaX = 1; break;   // Abajo
                case ConsoleKey.LeftArrow: deltaY = -1; break;  // Izquierda
                case ConsoleKey.RightArrow: deltaY = 1; break;  // Derecha
            }

            MoverJugadorConVelocidad(user2, deltaX, deltaY);
        }

        private bool EsMovimientoValido(int x, int y)
        {
            // Permitir movimiento a casillas vacías (0) o con trampas (5, 7, 9)
            return x >= 0 && x < anchodellaberinto && y >= 0 && y < largodellaberinto &&
                   (Maze[x, y] == 0 || Maze[x, y] == 5 || Maze[x, y] == 7 || Maze[x, y] == 9);
        }

        private void MoverJugadorConVelocidad(Personajes jugador, int deltaX, int deltaY)
        {
            int velocidadRestante = jugador.Velocidad;

            while (velocidadRestante > 0)
            {
                int nuevaX = jugador.PosicionX + deltaX;
                int nuevaY = jugador.PosicionY + deltaY;

                if (EsMovimientoValido(nuevaX, nuevaY))
                {
                    jugador.PosicionX = nuevaX;
                    jugador.PosicionY = nuevaY;
                    velocidadRestante--;

                    // Verificar si el personajes cayo en una trampa
                    AplicarEfectoTrampa(jugador);
                }
                else
                {
                    break; // choco con una pared, detener el movimiento
                }
            }
        }


        private bool MovimientoValido(char tecla) //Validar para que no  me pase el turno
        {
            return tecla == 'W' || tecla == 'A' || tecla == 'S' || tecla == 'D';
        }
        private bool MovimientoValido2(ConsoleKey tecla) //Validar para que no me pase el turno
        {
            return tecla == ConsoleKey.UpArrow || tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.LeftArrow || tecla == ConsoleKey.RightArrow;
        }

        public void Avancedeljuego()
        {
            while (true)
            {
                Console.Clear();
                PintarLaberinto();

                Console.WriteLine($"Turno de: {(Sistemadeturnos == 0 ? "Jugador 1" : "Jugador 2")}"); //Sistema de turnos

                bool movimientoValido = false;

                if (Sistemadeturnos == 0)
                {
                    Console.WriteLine("Jugador 1: Usa W (arriba), A (izquierda), S (abajo), D (derecha) para moverte.");
                    char tecla = Console.ReadKey().KeyChar;
                    tecla = char.ToUpper(tecla); // Convertir a mayúscula para evitar problemas con minúsculas

                    if (MovimientoValido(tecla))
                    {
                        MoverUser1(tecla); // Mover
                        movimientoValido = true;
                    }
                    else
                    {
                        Console.WriteLine("Tecla invalida, presiona otra"); //Para avisar que se debe presionar
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Jugador 2: Usa las flechas (↑, ↓, ←, →) para moverte.");
                    ConsoleKey tecla = Console.ReadKey().Key;

                    if (MovimientoValido2(tecla))
                    {
                        MoverUser2(tecla); // Mover 
                        movimientoValido = true;
                    }
                    else
                    {
                        Console.WriteLine("Tecla invalida"); //Para indicar que se debe presionar otra
                        Console.ReadKey(); // Esperar a que  presione una tecla
                    }
                }

                // Evitar que pase el turno si el movimiento no fue valido
                if (movimientoValido)
                {
                    Sistemadeturnos = (Sistemadeturnos + 1) % 2; // Alternar turnos
                }
                //Poner por aqui condicion de victoria
            }
        }
        private void PintarLaberinto()
        {
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
                        Console.Write(user1.Simbolo + " "); // Mostrar User1 
                    }
                    else if (i == user2.PosicionX && j == user2.PosicionY)
                    {
                        Console.Write(user2.Simbolo + " "); // Mostrar user2
                    }
                    else if (Maze[i, j] == 5)
                    {
                        Console.Write("T" + " "); // Trampa que quita vida
                    }
                    else if (Maze[i, j] == 7)
                    {
                        Console.Write("V" + " "); // Trampa que quita vida posicionydelatrampa reduce velocidad
                    }
                    else if (Maze[i, j] == 9)
                    {
                        Console.Write("S" + " "); // Trampa que reduce velocidad
                    }
                    else
                    {
                        Console.Write(" " + " "); // Espacio abierto
                    }
                }
                Console.WriteLine(); // Nueva línea al final de cada fila
            }
        }
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








