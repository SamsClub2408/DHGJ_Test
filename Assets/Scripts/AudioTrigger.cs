using UnityEngine; 
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private CircleCollider2D activationArea1; // Asigna el collider en el inspector
    private bool jugadorHaTocado = false; // Variable para detectar que ha recogido el objeto

    public AudioClip objetoRecogidoClip; // Asigna el clip de audio en el inspector
    public Animator lampreaAnimator; // Asigna el animator de la lamprea en el inspector

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        activationArea1 = GetComponent<CircleCollider2D>();
        activationArea1.enabled = false; // Desactiva el collider al inicio
        audioSource.pitch = 1f; // Asegura el pitch estándar
    }

    private void FixedUpdate()
    {
        if(Item.objetoRecogido01 && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(objetoRecogidoClip); // Reproduce el clip de audio una vez
            Item.objetoRecogido01 = false; // Reinicia la variable al terminar el audio
            jugadorHaTocado = true; // Marca que el jugador ha recogido el objeto
        }

        if(jugadorHaTocado && CamaraPOV.Nivel == 1)
        {
            activationArea1.enabled = true; // Activa el collider
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            //Activar trigger del animator
            lampreaAnimator.SetTrigger("LampreaInvocacion");
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
    }
}
