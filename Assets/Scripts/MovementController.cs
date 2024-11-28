using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Transform target; // El objetivo actual
    private Dijkstra dijkstra; // Referencia al script de A*
    private DynamicArrive kinematicArrive; // Referencia al script de DynamicArrive
    private TileGraphVisualizer graphVisualizer; // Referencia al grafo
    private Transform character; // El objeto que se moverá
    private Transform lastTarget;

    private Vector2Int lastTargetPos;
    private List<TileNode> path; // Ruta calculada
    private int currentNodeIndex = 0; // Índice del nodo actual
    private Vector2Int lastTargetPosition; // Posición previa del objetivo para detección de cambios

    void Start()
    {
        dijkstra =gameObject.AddComponent<Dijkstra>();
        kinematicArrive = GetComponent<DynamicArrive>();
        GameObject graph = GameObject.FindGameObjectWithTag("Map");
        graphVisualizer = graph.GetComponent<TileGraphVisualizer>();
        character = transform;
    }

    void Update()
    {
        if (target == null)
            return;
        else
            CalculatePath();

        // Verificar si hay una ruta válida
        if (path != null && currentNodeIndex < path.Count)
        {
            // Moverse al nodo actual
            TileNode currentNode = path[currentNodeIndex];
            moveToPoint(currentNode);

            if (arrive())
                currentNodeIndex++;
        }
    }

    public void moveTo(Transform newTarget)
    {
        target = newTarget;
    }

    private void moveToPoint(TileNode node)
    {
        
        kinematicArrive.target = node.gameObject.transform;
    }

    private void CalculatePath()
    {

        // Convertir las posiciones a celdas del Tilemap
        Vector2Int start = (Vector2Int)graphVisualizer.tilemap.WorldToCell(character.position);
        Vector2Int end = (Vector2Int)graphVisualizer.tilemap.WorldToCell(target.position);

        Debug.Log(end == lastTargetPos);
        // Verificar si el objetivo es válido
        if ( end == lastTargetPos)
        {
            Debug.Log("No se puede calcular la ruta porque el objetivo es igual al anterior.");
            return;
        }


        // // Obtener los nodos de inicio y objetivo
        TileNode startNode = graphVisualizer.GetNodeAtPosition(start);
        TileNode goalNode = graphVisualizer.GetNodeAtPosition(end);

        lastTargetPos = end;

        if (startNode != null && goalNode != null)
        {
            // Calcular la ruta usando A*
            path = dijkstra.FindPath(startNode, goalNode);
            currentNodeIndex = 0; // Reiniciar el índice de nodo actual
            if (path == null)
            {
                Debug.Log("No se encontró un camino.");
            }
            else
                printPath(path);
        }
        else
        {
            Debug.Log("Inicio o objetivo no válidos.");
        }
    }

    private bool arrive()
    {
        // Verificar si la distancia entre el personaje y el objetivo es menor que el umbral de llegada
        if (
            Vector3.Distance(character.position, kinematicArrive.target.position)
            < kinematicArrive.targetRadius
        )
        {
            
            return true; // El personaje ha llegado
        }
        return false; // El personaje no ha llegado
    }

    private void printPath(List<TileNode> path)
    {
        string result = "Camino:\n";
        foreach (TileNode node in path)
        {
            result += $"{node.gameObject.transform.position},";
        }

        Debug.Log(result);
    }
}
