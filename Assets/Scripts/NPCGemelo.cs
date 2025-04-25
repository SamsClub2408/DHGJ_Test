using UnityEngine;
using System.Collections;

public class NPCGemelo : MonoBehaviour
{
    public GameObject dialogoImagen1; // Imagen del primer diálogo
    public GameObject dialogoImagen2; // Imagen del segundo diálogo
    public Animator gemeloAnimator; // Animator del NPC
    public float tiempoEsperaDialogo = 3f; // Tiempo antes de ocultar el primer diálogo
    public float tiempoDuracionGemeloCad = 4f; // Duración de la animación GemeloCad
    public float tiempoGemeloCadIdle = 5f; // Tiempo antes de activar GemeloBarco

    void Start()
    {
        gemeloAnimator.SetInteger("Estado", 0); // Inicia en GemeloIdle
        dialogoImagen1.SetActive(false);
        dialogoImagen2.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            dialogoImagen1.SetActive(true);
            StartCoroutine(FlujoAnimaciones());
        }
    }

    IEnumerator FlujoAnimaciones()
    {
        yield return new WaitForSeconds(tiempoEsperaDialogo); // Espera 3 segundos
        dialogoImagen1.SetActive(false);
        gemeloAnimator.SetInteger("Estado", 1);
        gemeloAnimator.Update(0);
        Debug.Log("Estado cambiado a 1 y forzado en el Animator.");


        yield return new WaitForSeconds(tiempoDuracionGemeloCad); // Espera exactamente la duración de GemeloCad
        gemeloAnimator.SetInteger("Estado", 2); // Cambia a GemeloCadIdle
        dialogoImagen1.SetActive(false)
        dialogoImagen2.SetActive(true);


        yield return new WaitForSeconds(tiempoGemeloCadIdle);
        dialogoImagen2.SetActive(false);
        gemeloAnimator.SetInteger("Estado", 3); // Cambia a GemeloBarco
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (dialogoImagen1 != null) dialogoImagen1.SetActive(false);
            if (dialogoImagen2 != null) dialogoImagen2.SetActive(false);
        }
    }
}