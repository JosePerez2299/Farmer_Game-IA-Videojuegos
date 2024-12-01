using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AStar : MonoBehaviour
{
   public List<TileNode> FindPath(TileNode start, TileNode end, AgentType agentType)
{
    var openSet = new HashSet<TileNode> { start };
    var closedSet = new HashSet<TileNode>();
    var gScore = new Dictionary<TileNode, int> { [start] = 0 };
    var fScore = new Dictionary<TileNode, int> { [start] = Heuristic(start, end, agentType) };
    var cameFrom = new Dictionary<TileNode, TileNode>();

    while (openSet.Count > 0)
    {
        TileNode current = GetLowestFScoreNode(openSet, fScore);

        if (current == end)
            return ReconstructPath(cameFrom, current);

        openSet.Remove(current);
        closedSet.Add(current);

        foreach (Edge edge in current.neighbors)
        {
            TileNode neighbor = edge.to;

            if (closedSet.Contains(neighbor))
                continue;

            int tentativeGScore = gScore[current] + edge.cost + neighbor.GetTotalCost(agentType);

            if (!openSet.Contains(neighbor))
                openSet.Add(neighbor);
            else if (tentativeGScore >= gScore.GetValueOrDefault(neighbor, int.MaxValue))
                continue;

            cameFrom[neighbor] = current;
            gScore[neighbor] = tentativeGScore;
            fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, end, agentType);
        }
    }

    return null; // No se encontró un camino
}


    private static TileNode GetLowestFScoreNode(
        HashSet<TileNode> openSet,
        Dictionary<TileNode, int> fScore
    )
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

    private static int Heuristic(TileNode a, TileNode b, AgentType agentType)
    {
        int heuristic =
            Mathf.Abs(a.position.x - b.position.x) + Mathf.Abs(a.position.y - b.position.y) ;

        // Calcula la distancia Manhattan + el costo táctico basado en el agente
        return heuristic;
        
    }

    private static List<TileNode> ReconstructPath(
        Dictionary<TileNode, TileNode> cameFrom,
        TileNode current
    )
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
