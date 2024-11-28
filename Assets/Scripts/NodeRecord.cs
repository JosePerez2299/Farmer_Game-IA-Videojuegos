using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRecord
{
    public TileNode node;
    public Edge connection;
    public float costSoFar;

    public NodeRecord(TileNode node, Edge connection, float costSoFar)
    {
        this.node = node;
        this.connection = connection;
        this.costSoFar = costSoFar;
    }
}