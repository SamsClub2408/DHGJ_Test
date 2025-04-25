using UnityEngine;
using UnityEngine.SceneManagement;

public class Ancla : MonoBehaviour
{
    private bool jugadorDentro = false; // Variable para detectar si el Player est� en el �rea

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true; // El Player est� dentro del trigger
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false; // El Player sali� del trigger
        }
    }

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.Tab))
        {
            GestorEstado.instancia.nivelActual = CamaraPOV.Nivel; // Guarda el nivel actual
            GestorEstado.instancia.regresoDeBarco = true; // Marca que el jugador viene de Barco
            Debug.Log("Jugador subi� al Barco desde Nivel " + GestorEstado.instancia.nivelActual);
            SceneManager.LoadScene("Barco"); // Carga la escena
        }
    }
}
