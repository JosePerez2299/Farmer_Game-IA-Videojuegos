using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileNode
{
    public Vector2Int position; // Posición del nodo en la grilla
    public List<Edge> neighbors; // Vecinos del nodo
    public GameObject gameObject; // Representación visual
    
    public int baseCost = 1; // Costo base para moverse por el nodo
    public Dictionary<AgentType, int> tacticalCosts = new Dictionary<AgentType, int>(); // Costos tácticos por agente

    public TileNode(Vector2Int pos)
    {
        position = pos;
        neighbors = new List<Edge>();
        gameObject = CreateGameObject(); // Crear el GameObject al inicializar el nodo
    }

    // Configurar el costo táctico para un agente específico
    public void SetTacticalCost(AgentType agentType, int cost)
    {
        tacticalCosts[agentType] = cost;
    }

    // Obtener el costo total de este nodo para un agente específico
    public int GetTotalCost(AgentType agentType)
    {
        // Si el agente tiene un costo táctico definido, se usa, sino se usa 0 (sin costo adicional)
        int tacticalCost = tacticalCosts.ContainsKey(agentType) ? tacticalCosts[agentType] : 0;
        return baseCost + tacticalCost;
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
        Gizmos.DrawSphere(worldPosition, 0.1f);

        // Dibujar líneas hacia los vecinos
        Gizmos.color = Color.yellow;
        foreach (Edge edge in neighbors)
        {
            Vector3 neighborPosition = new Vector3(edge.to.gameObject.transform.position.x, edge.to.gameObject.transform.position.y, 0);
            Gizmos.DrawLine(worldPosition, neighborPosition);
        }


         // Mostrar los costos tácticos por cada agente
        foreach (var tacticalCost in tacticalCosts)
        {
            // Crear texto con AgentType y costo
            string label = $"{tacticalCost.Key}\n{tacticalCost.Value}";

            // Mostrar el texto encima del nodo
            Handles.Label(
                worldPosition + Vector3.up , // Ajusta la altura del texto
                label,
                new GUIStyle
                {
                    fontSize = 12,
                    normal = new GUIStyleState { textColor = Color.white }
                }
            );
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
