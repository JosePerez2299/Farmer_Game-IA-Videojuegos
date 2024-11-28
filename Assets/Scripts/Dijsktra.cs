using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dijkstra : MonoBehaviour
{
    private Tilemap tilemap;

    // Función para encontrar el camino más corto usando Dijkstra
    public List<TileNode> FindPath(TileNode startNode, TileNode goalNode)
    {
        // Inicializar las estructuras
        var openList = new List<NodeRecord>();  // Lista de nodos a explorar
        var closedSet = new HashSet<TileNode>();  // Conjunto de nodos ya procesados

        // Diccionario de costos acumulados para cada nodo
        Dictionary<TileNode, float> gCost = new Dictionary<TileNode, float>();

        // Diccionario para almacenar los predecesores de cada nodo
        Dictionary<TileNode, TileNode> cameFrom = new Dictionary<TileNode, TileNode>();

        // Inicializar el nodo de inicio
        gCost[startNode] = 0;
        openList.Add(new NodeRecord(startNode, null, 0));

        // Iterar mientras haya nodos por explorar
        while (openList.Count > 0)
        {
            // Obtener el nodo con el menor costo acumulado
            NodeRecord current = GetLowestCostNode(openList);

            // Si hemos llegado al nodo objetivo, terminamos
            if (current.node == goalNode)
                break;

            // Procesar los vecinos del nodo actual
            foreach (Edge connection in current.node.neighbors)
            {
                TileNode endNode = connection.to;
                float endNodeCost = current.costSoFar + connection.cost;

                // Si el nodo ya está en el conjunto cerrado, ignorarlo
                if (closedSet.Contains(endNode))
                    continue;

                // Si el nodo no está en la lista abierta o encontramos un camino más corto, actualizarlo
                if (!gCost.ContainsKey(endNode) || endNodeCost < gCost[endNode])
                {
                    gCost[endNode] = endNodeCost;
                    cameFrom[endNode] = current.node;

                    // Agregar el nodo a la lista abierta si no está
                    if (!openList.Exists(record => record.node == endNode))
                    {
                        openList.Add(new NodeRecord(endNode, connection, endNodeCost));
                    }
                }
            }

            // Mover el nodo actual al conjunto cerrado
            openList.Remove(current);
            closedSet.Add(current.node);
        }

        // Si no encontramos el objetivo
        if (!gCost.ContainsKey(goalNode))
        {
            Debug.Log("No se encontró un camino.");
            return null;
        }

        // Reconstruir el camino desde el objetivo hasta el inicio
        List<TileNode> path = new List<TileNode>();
        TileNode currentNode = goalNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = cameFrom[currentNode];
        }

        path.Add(startNode);
        path.Reverse();  // Invertir para que el camino vaya desde el inicio al objetivo

        return path;
    }

    // Función heurística: Dijkstra no necesita heurística, solo el costo acumulado
    private static NodeRecord GetLowestCostNode(List<NodeRecord> list)
    {
        NodeRecord lowest = list[0];
        foreach (NodeRecord record in list)
        {
            if (record.costSoFar < lowest.costSoFar)
            {
                lowest = record;
            }
        }
        return lowest;
    }
}

// Estructura para representar un registro de nodo
