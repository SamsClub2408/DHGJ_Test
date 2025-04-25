using UnityEngine; 
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource audioSource1;
    public CircleCollider2D activationArea1; // Asigna el collider en el inspector
    private bool jugadorHaTocado = false; // Variable para detectar que ha recogido el objeto

    public AudioClip objetoRecogidoClip, objetoRecogidoClip1; // Asigna el clip de audio en el inspector
    public Animator lampreaAnimator; // Asigna el animator de la lamprea en el inspector
    public GameObject aleta;

    public static bool Atacado = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        activationArea1.enabled = false; // Desactiva el collider al inicio
        audioSource.pitch = 1f; // Asegura el pitch estándar
        aleta.SetActive(false);
        if (GestorEstado.instancia.ObtenerEstadoAleta())
        {
            aleta.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        //Aleta
        if(Item.objetoRecogido01 && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(objetoRecogidoClip); // Reproduce el clip de audio una vez
            Item.objetoRecogido01 = false; // Reinicia la variable al terminar el audio
            jugadorHaTocado = true; // Marca que el jugador ha recogido el objeto
        }

        // Si el jugador ha recogido el objeto y está en el nivel 1, activa el collider
        if (jugadorHaTocado && CamaraPOV.Nivel == 1)
        {
            activationArea1.enabled = true; // Activa el collider
        }

        //Vasija
        if (Item.objetoRecogido02 && !audioSource1.isPlaying)
        {
            audioSource1.PlayOneShot(objetoRecogidoClip1);
            Item.objetoRecogido02 = false;

            aleta.SetActive(true);
            GestorEstado.instancia.ActivarAleta(); // Guarda el estado de la aleta
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            //Activar trigger del animator
            lampreaAnimator.SetTrigger("LampreaInvocacion");
            Atacado = true;
            StartCoroutine(CambiarEscena()); // Llama a la corrutina para cambiar de escena
        }
    }

    //Corrutina para cambiar de escena
    private IEnumerator CambiarEscena()
    {
        // Espera un segundo antes de cambiar de escena
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Layout"); // Cambia a la escena deseada
        CamaraPOV.Nivel = 2; // Cambia el nivel de la cámara
        Atacado = false; // Reinicia la variable de ataque
    }
}
