using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapDestroy : MonoBehaviour
{
    private TileGraphVisualizer graphVisualizer;

    void Start()
    {
        GameObject graph = GameObject.FindGameObjectWithTag("Map");;
        graphVisualizer = graph.GetComponent<TileGraphVisualizer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Contact : " + other.name);
        if (other.CompareTag("Bird"))
        {
            Destroy(gameObject);

            other.GetComponent<HealthDisplay>().TakeDamage(10);


            Vector2Int cell = (Vector2Int)graphVisualizer.tilemap.WorldToCell(other.transform.position);
            TileNode node = graphVisualizer.GetNodeAtPosition(cell);

            node.SetTacticalCost(AgentType.Bird, 0); // Eliminar desventaja para el bird (costo neutro)

        }
    }

}
