using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RespawnableObject : MonoBehaviour
{
    public bool IsRespawned { get; private set; }  // Estado que indica si la comida ha reaparecido

    private Renderer rend;


    void Start()
    {

        rend = GetComponent<Renderer>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
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

        if (state) transform.tag = "Rice"; else transform.tag = "NoRice";

    }
}
