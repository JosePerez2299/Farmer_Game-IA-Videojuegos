using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RespawnableObject : MonoBehaviour
{
    public bool IsRespawned { get; private set; }  // Estado que indica si la comida ha reaparecido

    private Renderer rend;

    private Collider2D col;


    void Start()
    {

        rend = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Disparo el trigger");
        if (other.CompareTag("Bird"))
        {
            Respawn(false);

        }
        else

        if (other.CompareTag("Farmer"))
        {
            Respawn(true);
        }
    }

    void Respawn(bool state)
    {
        IsRespawned = state;
        rend.enabled = state;

    }
}
