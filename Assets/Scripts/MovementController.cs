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
    // Start is called before the first frame update
    void Start()
    {
        character = transform;
        graphVisualizer = FindObjectOfType<TileGraphVisualizer>();
        movement = GetComponent<KinematicArrive>();
        positionObject = new GameObject("PositionToMove");
        aStar = GetComponent<AStar>();
        tilemap = aStar.tilemap;


    }

    // Update is called once per frame
    void Update()
    {
        // if (target != lastTarget) {
        //     aStar.foundPath = false;
        // }

        // if (graphVisualizer != null && graphVisualizer.nodes != null && !aStar.foundPath && target != null  )
        // {
        //     lastTarget = target;
        //     Vector2Int start = (Vector2Int)tilemap.WorldToCell(character.position);
        //     Vector2Int end = (Vector2Int)tilemap.WorldToCell(target.position);

        //     TileNode startNode = graphVisualizer.GetNodeAtPosition(start);
        //     TileNode goalNode = graphVisualizer.GetNodeAtPosition(end);

        //     if (startNode != null && goalNode != null)
        //     {
        //         List<TileNode> path = aStar.FindPath(startNode, goalNode);

        //         if (path != null)
        //         {
        //             printPath(path);
        //             StartCoroutine(MoveByPath(path));
        //             aStar.foundPath = true;

        //         }
        //         else
        //         {
        //             Debug.Log("No se encontró un camino.");
        //         }
        //     }
        // }


    }

    public void moveTo(Transform target)
    {
        Vector2Int start = (Vector2Int)tilemap.WorldToCell(character.position);
        Vector2Int end = (Vector2Int)tilemap.WorldToCell(target.position);
        TileNode startNode = graphVisualizer.GetNodeAtPosition(start);
        TileNode goalNode = graphVisualizer.GetNodeAtPosition(end);

       if (aStar.foundPath == true) {
        return;
       }



        StartCoroutine(MoveByPath(aStar.FindPath(startNode, goalNode)));



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
            // LWYG.target = positionObject.transform;
            // Asignar el nuevo objetivo al movementArrive
            movement.target = positionObject.transform;
            movement.arrive = false;

            // Esperar hasta que el personaje llegue al nodo
            yield return StartCoroutine(WaitUntilArrived());
        }

        aStar.foundPath = false;

    }

    IEnumerator WaitUntilArrived()
    {
        // Esperar hasta que la distancia al objetivo sea lo suficientemente pequeña
        while (!movement.arrive)
        {
            yield return null;  // Espera un frame y vuelve a comprobar
        }

    }
}

