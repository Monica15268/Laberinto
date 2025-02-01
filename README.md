# FondoVikini Maze
*Este proyecto es bastante sencillo de ejecutar, no tiene complicaciones, pues es solo bajarlo y ejecutarlo en Visual Studio o en Visual Studio Code. De esta manera, se debería ejecutar correctamente, y así poder probarlo.*
## ¿Como jugar?
*Este juego es bastante sencillo de jugar. Primeramente, a la hora de ejecutarlo, estarán disponibles dos opciones:*
**Jugar(Presionar 1)**
**Salir(Presionar 2)**
**(No saldrá asi textualmente escrito en el juego, pues lo hice de manera diferente para darle un toque estetico relacionado con el proyecto.)**
*Cabe aclarar que solo pueden jugar dos personas, las cuales podrán escoger una ficha entre 5 opciones. Queda totalmente permitido jugar con la misma ficha, pues no todas las personas juegan de la misma manera, y en mi opinión es válido. Luego aparecerán en el laberinto, en la misma posición, y se irán moviendo por turnos.*
### HAY TRAMPAS
*Pues sí, debes tener mucho cuidado en este peligroso laberinto, por suerte existen habilidades. Sin embargo, estas poseen tiempo de enfriamiento, mientras estén disponibles en el turno, se preguntará si el jugador desea usarlas, y para ello tendrá que presionar la tecla X, pero si no desea usarla, pues simplemente presione otra tecla.*
#### Movimiento
*Con respecto al tema movimiento, se podría decir que es bastante sencillo,  pues el primer jugador se moverá con las teclas W(arriba), S(abajo), A(izquierda), D(derecha). El segundo jugador se movera con las flechas.*
##### Habilidades
**Sanador** *: Aumenta la vida del personaje en 30*  <br>
**Master**	*:Aumenta la vida en 5 puntos y la velocidad en 1 punto.*
**Ignora Trampas** *:Otorga inmunidad a trampas.*
**Super pro**	*: Aumenta la vida en 10 puntos y otorga inmunidad a trampas.*
**Super Veloz**	*: Aumenta la velocidad en 2 puntos.*
###### Objetivo
*El objetivo de este juego es llegar al final del laberinto, el primero que llegue, sera el vencedor de esta reñida comptencia*

 ###### Implementación
**Este código se divide en 5 clases:**
**Game** *: En esta se inicializa el menú de entrada y gran parte del juego.*
**Personajes** *: Representa los personajes del juego con sus atributos.*
**Laberinto** *: En esta clase, se encuentra gran parte de la lógica del juego, pues se crea el laberinto, se implementa el movimiento de los personajes, se colocan trampas, se manejan varios elementos del código, entre otras funciones.*
**Habilidades** *: Se halla gran parte de lo relacionado a las habilidades y métodos para hacerlas funcionar.*
**Ciclo del juego** *: Aquí se encuentra el Main, es decir, el que ejecuta el proyecto.*
