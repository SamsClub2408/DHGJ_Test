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

    private void Start()
    {
        StartCoroutine(EsperarFinAnimacion1()); // Espera a que termine la animación inicial
        StartCoroutine(EsperarFinAnimacion2()); // Espera a que termine la animación inicial
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
}