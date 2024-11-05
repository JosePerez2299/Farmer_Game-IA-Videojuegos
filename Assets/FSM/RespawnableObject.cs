using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RespawnableObject : MonoBehaviour
{
    public GameObject character;
    public Vector3[] respawnPoints;  // Arreglo de TileNodes para el respawn
    public float respawnTime = 5f;    // Tiempo que el objeto estará desactivado
    public bool IsRespawned { get; private set; }  // Estado que indica si la comida ha reaparecido

    private TileGraphVisualizer tileGraph;

    private AStar aStar;
    private KinematicArrive kinematic;

    private Renderer rend;

    private Collider2D col;

    void Start()
    {


        character = GameObject.FindGameObjectWithTag("Player");
        tileGraph = FindAnyObjectByType<TileGraphVisualizer>();
        aStar = character.GetComponent<AStar>();
        kinematic = character.GetComponent<KinematicArrive>();
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();
        transform.position = new Vector3(20.5f, 4.5f, 0);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Disparo el trigger");
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        // Desactivar el objeto
        IsRespawned = false;
        rend.enabled = false;  // Desactiva la visibilidad
        col.enabled = false;  // Desactiva el col
        Debug.Log("Entro");
        // Esperar un tiempo antes del respawn
        yield return new WaitForSeconds(respawnTime);

        Debug.Log("Ya paso el time");

        Vector2Int randomPos = new Vector2Int(Random.Range(1, 24), Random.Range(1, 13));


        while (transform.position == (Vector3Int)randomPos || !tileGraph.nodes.ContainsKey(randomPos))
        {
            randomPos = new Vector2Int(Random.Range(1, 24), Random.Range(1, 13));
            Debug.Log("La posicion generada:    " + randomPos);


        }

        transform.position = (Vector3Int)randomPos;

        aStar.target = transform;
        gameObject.SetActive(true);
        IsRespawned = true;
        aStar.foundPath = false;
        kinematic.arrive = false;

        rend.enabled = true;  // Desactiva la visibilidad
        col.enabled = true;  // Desactiva el collider

        // Reaparecer el objeto
        // Notificar que el objeto ha respawneado
    }
}
