using UnityEngine;
using UnityEngine.UI; // Importar UI para usar Text.

public class HealthDisplay : MonoBehaviour
{
    private Transform character; // El GameObject al que seguirá el texto.
    public Text hpText; // Referencia al componente Text.
    public Vector3 offset;

    // Offset para ajustar la posición del texto sobre el objeto.

    private int currentHp = 100;

    private int maxHp = 100;

    void Start()
    {
        character = transform;
    }

    void Update()
    {
        // Asegura que el texto siga al GameObject con el offset especificado.
        if (character != null)
        {
            Vector3 worldPosition = character.position + offset;
            hpText.text = "HP: " + currentHp.ToString();

            hpText.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
        }
    }

    // Método para recibir daño cuando pasa por una trampa
    public void TakeDamage(int damage)
    {
        currentHp -= damage; // Reducir la salud por el daño recibido

        // Asegúrate de que la salud no sea menor que cero
        currentHp = Mathf.Max(currentHp, 0);

        Debug.Log("Bird Health: " + currentHp);

        // Si la salud llega a 0, el Bird muere
        if (currentHp == 0)
        {
            Die();
        }
    }

    // Método cuando el Bird muere
    void Die()
    {
        Debug.Log("Bird has died!");
        // Aquí podrías hacer que el Bird sea destruido o realizar alguna otra acción
        Destroy(gameObject); // Destruir al Bird (puedes cambiar esto si deseas otro comportamiento)
    }

    // Método para actualizar el HP.
    public void TakeHp(int hp)
    {
        currentHp += hp; // Reducir la salud por el daño recibido

        // Asegúrate de que la salud no sea menor que cero
        currentHp = Mathf.Max(currentHp, maxHp);
    }
}
