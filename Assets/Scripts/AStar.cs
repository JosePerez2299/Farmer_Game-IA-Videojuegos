using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar : MonoBehaviour
{

    public Transform character;
    public Transform target;

    public Tilemap tilemap;

    TileGraphVisualizer graphVisualizer;

    public bool foundPath = false;

    private KinematicArrive movement;

    private LookWhereYoureGoing LWYG;

    private  GameObject positionObject;


    void Start()
    {
        graphVisualizer = FindObjectOfType<TileGraphVisualizer>();
        movement = FindObjectOfType<KinematicArrive>();
        LWYG = FindObjectOfType<LookWhereYoureGoing>();
        positionObject = new GameObject("PositionToMove");


    }

    void Update()
    {
        // Verifica que el grafo haya sido generado
        if (graphVisualizer != null && graphVisualizer.nodes != null && !foundPath)
        {
            // Obtener la referencia de TileGraphVisualizer

            Vector2Int start = (Vector2Int)tilemap.WorldToCell(character.position);
            Vector2Int end = (Vector2Int)tilemap.WorldToCell(target.position);

            TileNode startNode = graphVisualizer.GetNodeAtPosition(start);
            TileNode goalNode = graphVisualizer.GetNodeAtPosition(end);

            if (startNode != null && goalNode != null)
            {
                List<TileNode> path = FindPath(startNode, goalNode);

                if (path != null)
                {
                    printPath(path);
                    StartCoroutine(MoveByPath(path));
                    foundPath = true;

                }
                else
                {
                    Debug.Log("No se encontró un camino.");
                }
            }

        }

    }

    public static List<TileNode> FindPath(TileNode startNode, TileNode goalNode)
    {
        // Crear las listas abiertas y cerradas
        List<NodeRecord> openList = new List<NodeRecord>();
        List<NodeRecord> closedList = new List<NodeRecord>();

        // Crear el NodeRecord para el nodo inicial
        NodeRecord startRecord = new NodeRecord(startNode, null, 0, Heuristic(startNode, goalNode));
        openList.Add(startRecord);

        NodeRecord current = null;

        // Iterar mientras haya nodos por explorar
        while (openList.Count > 0)
        {
            // Obtener el nodo con menor estimatedTotalCost
            current = GetLowestCostNode(openList);

            // Si hemos llegado al nodo objetivo, terminamos
            if (current.node == goalNode)
                break;

            // Obtener las conexiones (vecinos)
            foreach (Edge connection in current.node.neighbors)
            {
                TileNode endNode = connection.to;
                float endNodeCost = current.costSoFar + connection.cost;

                // Buscar en la lista cerrada
                NodeRecord endNodeRecord = closedList.Find(record => record.node == endNode);

                // Si el nodo está en la lista cerrada y el costo es peor, continuar
                if (endNodeRecord != null && endNodeRecord.costSoFar <= endNodeCost)
                    continue;

                // Si no está en la lista abierta o cerrada, crear un nuevo record
                if (endNodeRecord == null)
                {
                    endNodeRecord = new NodeRecord(endNode, connection, endNodeCost, endNodeCost + Heuristic(endNode, goalNode));
                    openList.Add(endNodeRecord);
                }
                else
                {
                    // Si ya está en la lista, actualizar el costo si es mejor
                    endNodeRecord.connection = connection;
                    endNodeRecord.costSoFar = endNodeCost;
                    endNodeRecord.estimatedTotalCost = endNodeCost + Heuristic(endNode, goalNode);
                }
            }

            // Mover el nodo actual a la lista cerrada
            openList.Remove(current);
            closedList.Add(current);
        }

        // Si no encontramos el objetivo
        if (current.node != goalNode)
        {
            Debug.Log("No se encontró un camino.");
            return null;
        }

        // Reconstruir el camino
        List<TileNode> path = new List<TileNode>();
        while (current.node != startNode)
        {
            path.Add(current.node);
            current = closedList.Find(record => record.node == current.connection.from);
        }
        path.Reverse();
        return path;
    }

    // Heurística: Usamos la distancia Manhattan para grids
    private static float Heuristic(TileNode fromNode, TileNode toNode)
    {
        return Mathf.Abs(fromNode.position.x - toNode.position.x) + Mathf.Abs(fromNode.position.y - toNode.position.y);
    }

    // Obtener el nodo con menor costo estimado de la lista abierta
    private static NodeRecord GetLowestCostNode(List<NodeRecord> list)
    {
        NodeRecord lowest = list[0];
        foreach (NodeRecord record in list)
        {
            if (record.estimatedTotalCost < lowest.estimatedTotalCost)
            {
                lowest = record;
            }
        }
        return lowest;
    }

    void printPath(List<TileNode> path)
    {
        string result = "Camino encontrado: \n";

        foreach (TileNode node in path)
        {
            result += node.position.ToString() + ",";
        }

        result += "\n";

        Debug.Log(result);
    }

    IEnumerator MoveByPath(List<TileNode> path)
    {

   

        foreach (TileNode node in path)
        {
            // Obtener la posición del centro del TileNode en el mundo
            Vector3 posToMove = tilemap.GetCellCenterWorld((Vector3Int)node.position);
            positionObject.transform.position = posToMove;
            LWYG.target = positionObject.transform;
            // Asignar el nuevo objetivo al movementArrive
            movement.target = positionObject.transform;
            movement.arrive = false;

            // Esperar hasta que el personaje llegue al nodo
            yield return StartCoroutine(WaitUntilArrived());
        }

    }

    IEnumerator WaitUntilArrived()
    {
        // Esperar hasta que la distancia al objetivo sea lo suficientemente pequeña
        while (!movement.arrive)
        {
            yield return null;  // Espera un frame y vuelve a comprobar
        }

    }

}
