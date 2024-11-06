using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    private AStar aStar;
    private Transform character, lastTarget;
    public Transform target;

    private TileGraphVisualizer graphVisualizer;
    private KinematicArrive movement;
    private LookWhereYoureGoing LWYG;
    private GameObject positionObject;
    private Tilemap tilemap;

    private Coroutine currentMovementCoroutine;  // Para almacenar la corrutina actual

    [System.Obsolete]
    void Start()
    {
        character = transform;
        GameObject graph = GameObject.FindGameObjectWithTag("Map");

        graphVisualizer = graph.GetComponent<TileGraphVisualizer>();
        movement = GetComponent<KinematicArrive>();
        positionObject = new GameObject("PositionToMove");
        aStar = GetComponent<AStar>();
        tilemap = aStar.tilemap;
    }

    void Update()
    {
        // Si el target cambió y no es nulo, iniciar el movimiento
        if (target != lastTarget && target != null)
        {
            lastTarget = target;
            moveTo(target);
        }
    }

    public void moveTo(Transform target)
    {
        Vector2Int start = (Vector2Int)tilemap.WorldToCell(character.position);
        Vector2Int end = (Vector2Int)tilemap.WorldToCell(target.position);
        TileNode startNode = graphVisualizer.GetNodeAtPosition(start);
        TileNode goalNode = graphVisualizer.GetNodeAtPosition(end);

        if (aStar.foundPath == true)
        {
            return;  // Si ya encontró un camino, no hacer nada
        }

        // Cancelar la corrutina de movimiento anterior si está activa
        if (currentMovementCoroutine != null)
        {
            StopCoroutine(currentMovementCoroutine);
        }

        // Iniciar la nueva corrutina para mover al personaje
        currentMovementCoroutine = StartCoroutine(MoveByPath(aStar.FindPath(startNode, goalNode)));
    }

    void printPath(List<TileNode> path)
    {
        string result = "Camino encontrado: \n";

        foreach (TileNode node in path)
        {
            result += node.position.ToString() + ",";
        }

        result += "\n";
        Debug.Log(result);
    }

    IEnumerator MoveByPath(List<TileNode> path)
    {
        foreach (TileNode node in path)
        {
            // Obtener la posición del centro del TileNode en el mundo
            Vector3 posToMove = tilemap.GetCellCenterWorld((Vector3Int)node.position);
            positionObject.transform.position = posToMove;
            movement.target = positionObject.transform;
            movement.arrive = false;

            // Esperar hasta que el personaje llegue al nodo
            yield return StartCoroutine(WaitUntilArrived());
        }

        aStar.foundPath = false;
        currentMovementCoroutine = null;  // Liberar la referencia de la corrutina actual
    }

    IEnumerator WaitUntilArrived()
    {
        while (!movement.arrive )
        {
            yield return null;
        }
    }
}
