using System.Collections.Generic;
using UnityEngine;

public class FootprintGenerator : MonoBehaviour
{
    public GameObject footprintPrefab; // Prefab de la huella
    public GameObject trapPrefab; // Prefab de la trampa que reemplazará la huella
    public float movementThreshold = 0.1f; // Umbral de movimiento para generar huellas
    public float footprintProbability = 0.3f; // Probabilidad de generar una huella (0 a 1)
    private Vector3 lastPosition;
    private TileGraphVisualizer graphVisualizer;

    // Para evitar huellas duplicadas en los mismos nodos
    private Dictionary<Vector2Int, GameObject> generatedFootprints = new();

    private GameObject bird;

    void Start()
    {
        lastPosition = transform.position; // Guarda la posición inicial del personaje
        GameObject graph = GameObject.FindGameObjectWithTag("Map");
        bird = GameObject.FindGameObjectWithTag("Bird");
        graphVisualizer = graph.GetComponent<TileGraphVisualizer>(); // Encuentra el grafo en la escena
    }

    void Update()
    {
        // Si el personaje se mueve más que el umbral, generamos huellas
        if (Vector3.Distance(lastPosition, transform.position) > movementThreshold)
        {
            TryGenerateFootprint();
            lastPosition = transform.position; // Actualiza la posición
        }
    }

    // Intentar generar una huella con cierta probabilidad
    void TryGenerateFootprint()
    {
        if (Random.value < footprintProbability)
        {
            GenerateFootprint();
        }
    }

    // Generar la huella en la posición del personaje
    void GenerateFootprint()
    {
        Vector2Int tilePosition = (Vector2Int)
            graphVisualizer.tilemap.WorldToCell(transform.position);

        // Verificar si ya se ha generado una huella en este nodo
        if (generatedFootprints.ContainsKey(tilePosition))
        {
            return; // Ya hay una huella en este nodo, no generes otra
        }
        TileNode node = graphVisualizer.GetNodeAtPosition(tilePosition);

        // Instancia el prefab de la huella en la posición del personaje
        GameObject footprint = Instantiate(
            footprintPrefab,
            node.gameObject.transform.position,
            Quaternion.identity
        );

        // Generar un TacticalPoint ventajoso para el Farmer en el nodo correspondiente

        if (node != null)
        {
            node.SetTacticalCost(AgentType.Farmer, -10); // Establece el costo táctico para el Farmer
        }

        // Destruir la huella después de 'footprintLifeTime' segundos
        // Destroy(footprint, footprintLifeTime);

        // Registrar que se generó una huella en este nodo
        generatedFootprints[tilePosition] = footprint;
    }

    // Método que reemplaza la huella con una trampa cuando el granjero pasa por ella
    public void ReplaceFootprintWithTrap(Vector2Int tilePosition)
    {
        // Verificar si una huella ya fue generada en este nodo
        if (generatedFootprints.ContainsKey(tilePosition))
        {
            // Obtener la huella existente y destruirla
            GameObject footprint = generatedFootprints[tilePosition];
            Destroy(footprint); // Eliminar la huella original

            // Instanciar la trampa en la misma posición donde estaba la huella
            TileNode node = graphVisualizer.GetNodeAtPosition(tilePosition);
            if (node != null)
            {
                Vector3 pos = node.gameObject.transform.position;
                GameObject trap = Instantiate(trapPrefab, pos, Quaternion.identity); // Colocar la trampa

                // Eliminar el punto ventajoso para el Farmer y agregar un punto desventajoso para el Bird
                node.SetTacticalCost(AgentType.Farmer, 0); // Eliminar ventaja para el Farmer (costo neutro)
                node.SetTacticalCost(AgentType.Bird, 10); // Hacerlo desventajoso para el Bird (por ejemplo, costo alto)

                // Eliminar la huella del registro
                generatedFootprints.Remove(tilePosition);

                // Destroy(trap, 10f);
            }
        }
    }
}
