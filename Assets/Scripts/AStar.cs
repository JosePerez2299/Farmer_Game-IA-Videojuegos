using System.Collections.Generic;
using UnityEngine;

public class AStar: MonoBehaviour
{
    public  List<TileNode> FindPath(TileNode start, TileNode end)
    {
        // Conjuntos abiertos y cerrados
        var openSet = new HashSet<TileNode> { start };
        var closedSet = new HashSet<TileNode>();

        // Diccionarios para costos y rastreo de padres
        var gScore = new Dictionary<TileNode, int>();
        var fScore = new Dictionary<TileNode, int>();
        var cameFrom = new Dictionary<TileNode, TileNode>();

        gScore[start] = 0;
        fScore[start] = Heuristic(start, end);

        while (openSet.Count > 0)
        {
            // Encuentra el nodo con menor fScore
            TileNode current = GetLowestFScoreNode(openSet, fScore);

            // Si alcanzamos el objetivo, reconstruimos el camino
            if (current == end)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Edge edge in current.neighbors)
            {
                TileNode neighbor = edge.to;

                if (closedSet.Contains(neighbor))
                    continue;

                int tentativeGScore = gScore[current] + edge.cost;

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= gScore.GetValueOrDefault(neighbor, int.MaxValue))
                {
                    continue;
                }

                // Este es el mejor camino hasta ahora
                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, end);
            }
        }

        // No se encontró un camino
        return null;
    }

    private static TileNode GetLowestFScoreNode(HashSet<TileNode> openSet, Dictionary<TileNode, int> fScore)
    {
        TileNode lowestNode = null;
        int lowestScore = int.MaxValue;

        foreach (TileNode node in openSet)
        {
            int score = fScore.GetValueOrDefault(node, int.MaxValue);
            if (score < lowestScore)
            {
                lowestScore = score;
                lowestNode = node;
            }
        }

        return lowestNode;
    }

    private static int Heuristic(TileNode a, TileNode b)
    {
        // Distancia Manhattan como heurística
        return Mathf.Abs(a.position.x - b.position.x) + Mathf.Abs(a.position.y - b.position.y);
    }

    private static List<TileNode> ReconstructPath(Dictionary<TileNode, TileNode> cameFrom, TileNode current)
    {
        var path = new List<TileNode>();
        while (current != null)
        {
            path.Insert(0, current);
            cameFrom.TryGetValue(current, out current);
        }
        return path;
    }
}
