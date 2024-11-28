using System.Collections.Generic;
using UnityEngine;

public class TileNode
{
    public Vector2Int position; // Posición del tile en la grilla
    public List<Edge> neighbors; // Lista de vecinos del nodo
    public GameObject gameObject; // Representación visual del nodo

    public TileNode(Vector2Int pos)
    {
        position = pos;
        neighbors = new List<Edge>();
        gameObject = CreateGameObject(); // Crear el GameObject al inicializar el nodo
    }

    public void AddNeighbor(TileNode neighbor)
    {
        neighbors.Add(new Edge(this, neighbor));
    }

    private GameObject CreateGameObject()
    {
        // Crear un nuevo GameObject
        GameObject nodeObject = new($"TileNode_{position.x }_{position.y}");

        // Asignar posición en el mundo
        nodeObject.transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);

        // Agregar un componente visual (SpriteRenderer)
        SpriteRenderer renderer = nodeObject.AddComponent<SpriteRenderer>();
        renderer.color = Color.green; // Color para identificar los nodos

        // Puedes agregar otros componentes como colisionadores si es necesario

        return nodeObject;
    }

    public override string ToString()
    {
        return $"TileNode(Position: {gameObject.transform.position}, Neighbors: {neighbors.Count})";
    }

    public void DrawGizmo()
    {
        // Posición del nodo en el mundo
        Vector3 worldPosition = new(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        // Dibujar un punto en la posición del nodo
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(worldPosition, 0.2f);

        // Dibujar líneas hacia los vecinos
        Gizmos.color = Color.yellow;
        foreach (Edge edge in neighbors)
        {
            Vector3 neighborPosition = new Vector3(edge.to.gameObject.transform.position.x, edge.to.gameObject.transform.position.y, 0);
            Gizmos.DrawLine(worldPosition, neighborPosition);
        }
    }
}

public class Edge
{
    public TileNode from,
        to;
    public int cost;

    public Edge(TileNode from, TileNode to, int cost = 1)
    {
        this.from = from;
        this.to = to;
        this.cost = cost;
    }
}
