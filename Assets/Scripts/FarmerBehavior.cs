using UnityEngine;

public class FarmerBehavior : MonoBehaviour
{
    private TileGraphVisualizer graphVisualizer; // Referencia al grafo
    private FootprintGenerator footprintGenerator;


    void Start()
    {
        GameObject graph = GameObject.FindGameObjectWithTag("Map");
        graphVisualizer = graph.GetComponent<TileGraphVisualizer>();

        GameObject bird = GameObject.FindGameObjectWithTag("Bird");
        footprintGenerator = bird.GetComponent<FootprintGenerator>();

        // Encuentra el generador de huellas
    }

    void Update()
    {
        DetectAndModifyTacticalPoints();
    }

    // Detecta puntos ventajosos para el granjero y cambia el costo táctico
    void DetectAndModifyTacticalPoints()
    {
        // Obtiene la posición del granjero en la grilla
        Vector2Int farmerPosition = (Vector2Int)
            graphVisualizer.tilemap.WorldToCell(transform.position);

        // Llamar a la función de reemplazo de huella por trampa
        footprintGenerator.ReplaceFootprintWithTrap(farmerPosition);
    }
}
