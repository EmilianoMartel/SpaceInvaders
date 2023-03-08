# SpaceInvaders
Una copia del legendario juego japones del año 1978.
En este caso usado para practicar conceptos basicos de la progamacion en C# usando Unity.
Aqui voy a comentar el funcionamiento del juego con algunos datos o anecdotas mientras lo fui creando.

## Funcionamiento
Tiene un funcionamiento sencillo pero desafiante, en los siguientes items voy a escribir sobre las funciones que me resultaron mas desafiantes o alguna que tuve un problema sencillo anotanto su solucion.
### Colisiones
Un inconveniente encontrado en esta parte fue la colision con otros objetos.
la cual primero se soluciono con la grilla de colisiones propia de Uity, ya que por ejemplo los ataques enemigos podian impactar con otros enemigos y estos destruirlos. Otra forma de hacerlo es por codigo preguntanto por el LayerMask por ejemplo.
Luego me sucedio que mis ataques enemigos no impactaban con nada, eso es porque al hacerle un colider y ponerele que es "trigger" siempre para que impacte con otros objetos hay que agregarle un RigitBody2D.
### Grilla de Enemigos
Este momento fue sencillo ya que una grilla es facilmente generada por dos bucles for uno con las filas y otro dentro con las columnas.
Luego los acomodamos con un Vector2 para que no queden pegados o encimados.
### Movimiento Enemigos
Aca empezo lo mas interesante, como podemos ver en el juego original el conjunto enemigo recorre la pantalla hasta llegar al borde horizontal de la misma y cambian de direccion y bajan.
Esa pequeña parte del juego fue un desafio, en el cual fui por varios lugares pero la forma en la que logre solucionar esta funcion fue generando dentro del scrip Enemy 2 funciones importantes, la primera genere un "delegate" por fuera del objeto la cual se llama EnemyEvent y pide un objeto Enemy. La otra funcion generada fue una sencilla la cual se llama "SwapDirection" que sencillamente cambia la direccion multiplicando esta por -1 y baja en 1 el enemigo por el eje Y.
El siguiente paso es en el Scrip "Enemys", declaramos la variable para la Action "OnWallTouched" que le anidamos la funcion "SwapDirection" de ese mismo Scrip.
La funcion al final lo que hara es dentro del "Enemy" en la funcion Update una vez llegue a un punto de x, activa la Action "OnWallTouched" que a su vez activa la funcion "SwapDirection", que dentro del Scrip "Enemys" contiene un bucle foreach que activa la fucion "SwapDirection" en todos los enemigos.
