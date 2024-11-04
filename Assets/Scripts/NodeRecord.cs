using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRecord
{
    public TileNode node;
    public Edge connection;
    public float costSoFar;          // Costo total hasta este nodo
    public float estimatedTotalCost; // Costo total estimado

    public NodeRecord(TileNode node, Edge connection, float costSoFar, float estimatedTotalCost)
    {
        this.node = node;
        this.connection = connection;
        this.costSoFar = costSoFar;
        this.estimatedTotalCost = estimatedTotalCost;
    }
}

