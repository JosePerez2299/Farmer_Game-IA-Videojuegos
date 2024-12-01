using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float maxAcceleration = 5,
        maxSpeed = 5,
        targetRadius = 0.5f;
    public Transform target; // El objetivo actual
    public AStar aStar; // Referencia al script de A*
    private KinematicArrive kinematicArrive; // Referencia al script de DynamicArrive
    private TileGraphVisualizer graphVisualizer; // Referencia al grafo
    private Transform character; // El objeto que se moverá

    public Rigidbody2D rb;

    private Transform lastTarget;

    private Vector2Int lastTargetPos;
    public List<TileNode> path; // Ruta calculada
    private int currentNodeIndex = 0; // Índice del nodo actual
    private Vector2Int lastTargetPosition; // Posición previa del objetivo para detección de cambios
    private AgentTypeUtility agentTypeUtility = new();
    void Start()
    {
        aStar = gameObject.AddComponent<AStar>();
        kinematicArrive = GetComponent<KinematicArrive>();
        GameObject graph = GameObject.FindGameObjectWithTag("Map");
        graphVisualizer = graph.GetComponent<TileGraphVisualizer>();
        character = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null)
            return;
        else

            CalculatePath(agentTypeUtility.GetAgentTypeFromString(gameObject.name));

        // Verificar si hay una ruta válida
        if (path != null && currentNodeIndex < path.Count)
        {
            // Moverse al nodo actual
            TileNode currentNode = path[currentNodeIndex];
            DrawRemainingPath(path, currentNodeIndex, Color.cyan);

            moveToPoint(currentNode);

            if (arrive())
            {
                currentNodeIndex++;
            }

            return;
        }

        rb.linearVelocity = Vector2.zero;
    }

    public void moveTo(Transform newTarget)
    {
        Vector2Int posInCell = (Vector2Int)graphVisualizer.tilemap.WorldToCell(newTarget.position);
        TileNode node = graphVisualizer.GetNodeAtPosition(posInCell);

        target = node.gameObject.transform;
    }

    private void moveToPoint(TileNode node)
    {
        Vector3 targetPosition = node.gameObject.transform.position;

        // Dirección hacia el objetivo
        Vector3 direction = (targetPosition - character.position).normalized;

        // Calcula la aceleración deseada
        Vector3 desiredAcceleration = direction * maxAcceleration;

        // Limita la aceleración para no exceder el máximo permitido
        if (desiredAcceleration.magnitude > maxAcceleration)
        {
            desiredAcceleration = desiredAcceleration.normalized * maxAcceleration;
        }

        // Aplica la aceleración al Rigidbody2D
        if (rb != null)
        {
            rb.linearVelocity += (Vector2)(desiredAcceleration * Time.deltaTime);

            // Limita la velocidad máxima
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    private void CalculatePath(AgentType agentType = AgentType.None)
    {
        // Convertir las posiciones a celdas del Tilemap
        Vector2Int start = (Vector2Int)graphVisualizer.tilemap.WorldToCell(character.position);
        Vector2Int end = (Vector2Int)graphVisualizer.tilemap.WorldToCell(target.position);

        if (end == lastTargetPos)
        {
            // Debug.Log("No se puede calcular la ruta porque el objetivo es igual al anterior.");
            return;
        }

        // // Obtener los nodos de inicio y objetivo
        TileNode startNode = graphVisualizer.GetNodeAtPosition(start);
        TileNode goalNode = graphVisualizer.GetNodeAtPosition(end);

        lastTargetPos = end;

        if (startNode != null && goalNode != null)
        {   

            

            // Calcular la ruta usando A*
            path = aStar.FindPath(startNode, goalNode, agentType);
            if (gameObject.name == "Bird") {

            Debug.Log("Calculando Path para: "+ agentType + "--Hasta:" + target.name);
            printPath(path);
            }
          

            currentNodeIndex = 0; // Reiniciar el índice de nodo actual
            if (path == null)
            {
                // Debug.Log("No se encontró un camino.");
            }
          
        }
        else
        {
            // Debug.Log("Inicio o objetivo no válidos.");
        }
    }

    private void DrawRemainingPath(List<TileNode> path, int currentNodeIndex, Color color)
    {
        // Verifica que haya un camino válido
        if (path == null || currentNodeIndex >= path.Count - 1)
            return;

        for (int i = currentNodeIndex; i < path.Count - 1; i++)
        {
            TileNode currentNode = path[i];
            TileNode nextNode = path[i + 1];

            // Dibujar línea entre los nodos
            Debug.DrawLine(
                currentNode.gameObject.transform.position,
                nextNode.gameObject.transform.position,
                color,
                0f // Se dibuja en cada frame
            );
        }
    }

    private bool arrive()
    {
        if (
            Vector3.Distance(
                character.position,
                path[currentNodeIndex].gameObject.transform.position
            ) < targetRadius
        )
        {
            return true; // El personaje ha llegado al nodo actual
        }
        return false;
    }

    private void printPath(List<TileNode> path)
    {
        if (path==null) return;
        string result = "Camino:\n";
        foreach (TileNode node in path)
        {
            result += $"{node.gameObject.transform.position},";
        }

        Debug.Log(result);
    }
}
