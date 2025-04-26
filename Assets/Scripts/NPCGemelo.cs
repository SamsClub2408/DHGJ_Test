using UnityEngine;
using System.Collections;

public class NPCGemelo : MonoBehaviour
{
    public Animator gemeloAnimator;
    public GameObject dialogoImagen1, dialogoImagen2, objetoCaliz, objetoCad1;

    private int estadoActual = 0;

    void Start()
    {
        dialogoImagen1.SetActive(false);
        dialogoImagen2.SetActive(false);
        objetoCaliz.SetActive(false);

        gemeloAnimator.SetInteger("Estado", 0);

        if (CamaraPOV.Nivel == 2)
        {
            StartCoroutine(FlujoAnimaciones()); // ✅ Solo inicia la animación si estamos en el nivel 2
        }
    }

    IEnumerator FlujoAnimaciones()
    {
        yield return new WaitForSeconds(4f);
        CambiarEstado(1);

        yield return new WaitForSeconds(4f);
        CambiarEstado(2);

        yield return new WaitForSeconds(4f);
        CambiarEstado(3);
        objetoCad1.SetActive(false );
        yield return new WaitForSeconds(5.5f);
        objetoCaliz.SetActive(true);
    }

    void CambiarEstado(int nuevoEstado)
    {
        if (CamaraPOV.Nivel != 2) return; // ✅ Si el nivel no es 2, no cambiar el estado

        estadoActual = nuevoEstado;
        gemeloAnimator.SetInteger("Estado", nuevoEstado);
        Debug.Log("Cambiando a estado: " + nuevoEstado);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (estadoActual == 0)
            {
                dialogoImagen1.SetActive(true);
            }
            else if (estadoActual == 2)
            {
                dialogoImagen1.SetActive(false);
                dialogoImagen2.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogoImagen1.SetActive(false);
            dialogoImagen2.SetActive(false);
        }
    }
}