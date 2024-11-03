using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode {
    public Vector2Int position;  // Posición del tile en la grilla
    public List<TileNode> neighbors;  // Lista de vecinos del nodo

    public TileNode(Vector2Int pos) {
        position = pos;
        neighbors = new List<TileNode>();
    }

    public void AddNeighbor(TileNode neighbor) {
        neighbors.Add(neighbor);
    }
}
