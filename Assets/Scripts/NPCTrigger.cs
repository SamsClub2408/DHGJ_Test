using UnityEngine;
using System.Collections;

public class NPCTrigger : MonoBehaviour
{
    public GameObject dialogoImagen; // Imagen del di�logo
    public Animator personajeAnimator; // Animator del NPC
    public float tiempoEspera = 5f; // Tiempo de espera antes de alejarse


    void Start()
    {
        personajeAnimator.Play("LesbianaIdle"); // Inicia con la animaci�n Idle
        dialogoImagen.SetActive(false); // Asegura que el di�logo est� oculto al inicio
        if (GestorEstado.instancia.regresoDeBarco)
        {
            gameObject.SetActive(false); // Desactiva el NPC
            Debug.Log(gameObject.name + " desactivado al regresar de Barco.");
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisi�n detectada con: " + other.gameObject.name);
        if (other.CompareTag("MainCamera")) // Si la c�mara entra en el trigger del NPC
        {
            dialogoImagen.SetActive(true); // Activa el di�logo
            StartCoroutine(IniciarSalidaNPC());
        }
    }

    IEnumerator IniciarSalidaNPC()
    {
        yield return new WaitForSeconds(tiempoEspera); // Espera 8 segundos en Idle
        personajeAnimator.Play("Lesbiana"); // Reproduce la animaci�n de alejamiento
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera")) // Si el NPC se aleja, el di�logo desaparece
        {
            dialogoImagen.SetActive(false); // Oculta el di�logo autom�ticamente
        }
    }
}