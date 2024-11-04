using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileNode
{
    public Vector2Int position;  // Posición del tile en la grilla
    public List<Edge> neighbors;  // Lista de vecinos del nodo

    public TileNode(Vector2Int pos)
    {
        position = pos;
        neighbors = new List<Edge>();
    }

    public void AddNeighbor(TileNode neighbor)
    {
        neighbors.Add(new Edge(this, neighbor));
    }

    public override string ToString()
    {
        return $"TileNode(Position: {position}, Neighbors: {neighbors.Count})";
    }
}

public class Edge
{
    public TileNode from, to;
    public int cost;

    public Edge(TileNode from, TileNode to, int cost = 1)
    {
        this.from = from;
        this.to = to;
        this.cost = cost;
    }
}
