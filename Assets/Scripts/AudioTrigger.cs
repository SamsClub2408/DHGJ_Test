using UnityEngine; 
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;
    public CircleCollider2D activationArea1; // Asigna el collider en el inspector
    private bool jugadorHaTocado = false; // Variable para detectar que ha recogido el objeto

    public AudioClip objetoRecogidoClip, objetoRecogidoClip1, objetoRecogidoClip2,
        objetoRecogidoClip3; // Asigna el clip de audio en el inspector
    public Animator lampreaAnimator; // Asigna el animator de la lamprea en el inspector
    public GameObject aleta;

    public static bool Atacado = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        activationArea1.enabled = false; // Desactiva el collider al inicio
        audioSource.pitch = 1f; // Asegura el pitch estándar
        aleta.SetActive(false);
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
        if(Item.objetoRecogido02 && !audioSource1.isPlaying)
        {
            audioSource1.PlayOneShot(objetoRecogidoClip1); // Reproduce el clip de audio una vez
            Item.objetoRecogido02 = false; // Reinicia la variable al terminar el audio
            aleta.SetActive(true);
        }

        //Vasija 2
        if(Item.objetoRecogido03 && !audioSource2.isPlaying)
        {
            audioSource2.PlayOneShot(objetoRecogidoClip2);
            Item.objetoRecogido03 = false;
            Debug.Log("DERRUMBEEE");
        }

        //Caliz
        if (Item.objetoRecogido04 && !audioSource3.isPlaying)
        {
            audioSource3.PlayOneShot(objetoRecogidoClip3);
            Item.objetoRecogido04 = false;
        }

        //Inicio de nivel 2
        if(CamaraPOV.Nivel == 2)
        {
            audioSource4.enabled = true; // Activa el audio
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
