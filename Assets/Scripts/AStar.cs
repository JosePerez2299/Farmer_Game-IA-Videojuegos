using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour {
    public TileGraphVisualizer graphVisualizer;  // Referencia al visualizador del grafo
    public GameObject character;
    public GameObject target;

    void Start() {
        character = gameObject;
    }
    
    void Update() {
        FindPath();

    }



    void FindPath() {

        Vector3Int startTilePos = graphVisualizer.tilemap.WorldToCell(character.transform.position);
        Vector3Int targetTilePos = graphVisualizer.tilemap.WorldToCell(target.transform.position);

        // Obtener los nodos del TileGraphVisualizer
        TileNode startNode = graphVisualizer.GetNodeAtPosition(new Vector2Int(startTilePos.x , startTilePos.y ));
        TileNode goalNode = graphVisualizer.GetNodeAtPosition(new Vector2Int(targetTilePos.x, targetTilePos.y));
        Debug.Log(startNode.position);
        Debug.Log(goalNode.position);

        if (startNode != null && goalNode != null) {
            List<TileNode> path = AStarSearch(startNode, goalNode);


            if (path != null) {
                StartCoroutine(MoveAlongPath(path));
            }
        }
    }

    // Implementación del algoritmo A*
    List<TileNode> AStarSearch(TileNode startNode, TileNode goalNode) {
        List<TileNode> openList = new List<TileNode>();
        HashSet<TileNode> closedList = new HashSet<TileNode>();

        Dictionary<TileNode, TileNode> cameFrom = new Dictionary<TileNode, TileNode>();
        Dictionary<TileNode, float> gCost = new Dictionary<TileNode, float>();
        Dictionary<TileNode, float> fCost = new Dictionary<TileNode, float>();

        foreach (TileNode node in graphVisualizer.nodes.Values) {
            gCost[node] = Mathf.Infinity;
            fCost[node] = Mathf.Infinity;
        }

        gCost[startNode] = 0;
        fCost[startNode] = Heuristic(startNode, goalNode);

        openList.Add(startNode);

        while (openList.Count > 0) {
            TileNode current = GetLowestFCostNode(openList, fCost);

            if (current == goalNode) {
                return ReconstructPath(cameFrom, current);
            }

            openList.Remove(current);
            closedList.Add(current);

            foreach (TileNode neighbor in current.neighbors) {
                if (closedList.Contains(neighbor)) continue;

                float tentativeGCost = gCost[current] + Heuristic(current, neighbor);

                if (tentativeGCost < gCost[neighbor]) {
                    cameFrom[neighbor] = current;
                    gCost[neighbor] = tentativeGCost;
                    fCost[neighbor] = gCost[neighbor] + Heuristic(neighbor, goalNode);

                    if (!openList.Contains(neighbor)) {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;  // No se encontró un camino
    }

    // Función heurística (distancia Euclidiana)
    float Heuristic(TileNode a, TileNode b) {
        return Vector2Int.Distance(a.position, b.position);
    }

    TileNode GetLowestFCostNode(List<TileNode> openList, Dictionary<TileNode, float> fCost) {
        TileNode lowest = openList[0];
        foreach (TileNode node in openList) {
            if (fCost[node] < fCost[lowest]) {
                lowest = node;
            }
        }
        return lowest;
    }

    List<TileNode> ReconstructPath(Dictionary<TileNode, TileNode> cameFrom, TileNode current) {
        List<TileNode> path = new List<TileNode>();
        while (cameFrom.ContainsKey(current)) {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }

    IEnumerator MoveAlongPath(List<TileNode> path) {
        foreach (TileNode node in path) {
            Vector3 worldPos = graphVisualizer.tilemap.GetCellCenterWorld(new Vector3Int(node.position.x, node.position.y, 0));
            while (Vector3.Distance(character.transform.position, worldPos) > 0.1f) {
                character.transform.position = Vector3.MoveTowards(character.transform.position, worldPos, Time.deltaTime * 5f);
                yield return null;
            }
        }
    }
}
