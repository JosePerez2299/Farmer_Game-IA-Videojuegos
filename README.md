# Farmer Game - IA en Videojuegos

## Descripción

**Farmer Game** es un videojuego de simulación agrícola desarrollado en Unity que se centra en la implementación de algoritmos de inteligencia artificial para lograr movimientos suaves y naturales. El juego cuenta con tres personajes:

- **El Granjero**: Siembra en spots fijos y, al desplazarse de un punto a otro, evalúa la posibilidad de colocar trampas en las huellas.
- **El Pollito**: Deja huellas aleatorias mientras busca el trigo sembrado por el granjero, calculando rutas alternativas para evitar trampas.
- **La Vaca Endemoniada**: Activa un comportamiento de alarma de forma periódica, lo que hace que tanto el granjero como el pollito se oculten.

La dinámica del juego se basa en la planificación táctica de rutas. Cada tile del mapa se representa como un grafo no dirigido y se utiliza el algoritmo de Dijkstra para calcular las rutas óptimas, complementado por algoritmos de movimiento dinámico que proporcionan desplazamientos naturales.

## Enlaces a Videos

- **1era etapa de desarrollo: Algoritmos de movimiento**  
  [Ver video](https://www.youtube.com/watch?v=siDSHHC1HEw)

- **2da etapa de desarrollo: Tilemap y definición de comportamientos**  
  [Ver video](https://www.youtube.com/watch?v=Xi-Ckm9N6ws)

- **Proyecto final: Demo del juego final**  
  [Ver video](https://www.youtube.com/watch?v=MmS2ASVySog)
