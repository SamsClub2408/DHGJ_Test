using UnityEngine;

public class Item : MonoBehaviour
{
    private bool jugadorHaTocado = false; // Variable para detectar la colisión

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player")) // Verifica que el objeto que colisionó es el jugador
        {
            Debug.Log("El jugador ha tocado el objeto, presiona X para recogerlo.");
            jugadorHaTocado = true; // Marca que el jugador ha tocado el ítem
        }
    }

    private void Update()
    {
        if (jugadorHaTocado && Input.GetKeyDown(KeyCode.X))
        {
            Desaparecer();
        }
    }

    void Desaparecer()
    {
        Debug.Log("Objeto recogido y eliminado.");
        Destroy(gameObject);
    }
}