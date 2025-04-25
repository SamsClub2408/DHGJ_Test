using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Necesario para cambiar escenas

public class CambioDeEscena : MonoBehaviour
{
    private bool puedePresionar1 = false; // Estado que bloquea la tecla 1
    public string escenaTecla1; // Nombre de la primera escena
    public KeyCode tecla1 = KeyCode.M; // Tecla para la primera escena
    public Animator instruccionesAnimator; // Referencia al Animator de las instrucciones

    private bool puedePresionar2 = false; // Estado que bloquea la tecla 2
    public string escenaTecla2; // Nombre de la segunda escena
    public KeyCode tecla2 = KeyCode.S; // Tecla para la segunda escena
    public Animator FotoAnimator; // Referencia al Animator  

    public Animator muerteBarcoAnimator; // ✅ Referencia al Animator de MuerteBarco
    public string nombreAnimacionMuerte = "MuerteBarco"; // ✅ Nombre de la animación que queremos detectar

    private void Start()
    {
        StartCoroutine(EsperarFinAnimacion1()); // Espera a que termine la animación inicial
        StartCoroutine(EsperarFinAnimacion2()); // Espera a que termine la animación inicial
        StartCoroutine(EsperarFinAnimacionMuerte()); // ✅ Nueva función para detectar la animación MuerteBarco
    }

    void Update()
    {
        if (puedePresionar1 && Input.GetKeyDown(tecla1)) // Si el jugador presiona la primera tecla
        {
            SceneManager.LoadScene(escenaTecla1); // Cambia a la primera escena
        }

        if (puedePresionar2 && Input.GetKeyDown(tecla2)) // Si el jugador presiona la segunda tecla
        {
            SceneManager.LoadScene(escenaTecla2); // Cambia a la segunda escena
        }
    }

    IEnumerator EsperarFinAnimacion1()
    {
        yield return new WaitForSeconds(instruccionesAnimator.GetCurrentAnimatorStateInfo(0).length); // Espera la duración de la animación `Fade`
        puedePresionar1 = true; // Ahora se puede presionar  
    }

    IEnumerator EsperarFinAnimacion2()
    {
        yield return new WaitForSeconds(18f); // Duración de `FotoG`
        puedePresionar2 = true; // Ahora se puede presionar  
    }

    IEnumerator EsperarFinAnimacionMuerte()
    {
        // ✅ Esperar hasta que `muerte` sea TRUE antes de comenzar
        while (!ActivaMuerte.muerte)
        {
            yield return null; // Esperar hasta que muerte sea verdadera
        }

        Debug.Log("Muerte ahora es TRUE, esperando a que la animación 'MuerteBarco' comience...");

        // ✅ Esperar hasta que la animación realmente se inicie
        while (!muerteBarcoAnimator.GetCurrentAnimatorStateInfo(0).IsName(nombreAnimacionMuerte))
        {
            yield return null; // Esperar hasta que la animación comience
        }

        Debug.Log("¡Animación 'MuerteBarco' iniciada, esperando a que termine!");

        // ✅ Esperar hasta que la animación termine
        while (muerteBarcoAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null; // Esperar hasta que termine
        }

        // ✅ Solo cambiar de escena si muerte sigue siendo true
        if (ActivaMuerte.muerte)
        {
            CamaraPOV.Nivel = 3; // ✅ Cambiar la variable global Nivel a 3
            Debug.Log("Animación 'MuerteBarco' terminada y muerte activa. Nivel ahora es 3.");
            SceneManager.LoadScene("Layout"); // ✅ Cambiar a la escena "Layout"
        }
    }
}