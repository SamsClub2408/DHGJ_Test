using UnityEngine;

public class MusicaSacra : MonoBehaviour
{
    private CircleCollider2D MusicaMural;
    private AudioSource audioSource;
    private bool jugadorEnMural = false; // Variable para detectar que ha tocado el mural
    public static bool MuralActivo = false; // Variable para activar el mural

    private void Start()
    {
        MusicaMural = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1f; // Asegura el pitch estándar
    }

    private void Update()
    {
        if(CamaraPOV.Nivel == 3)
        {
            audioSource.enabled = true; // Activa el audio
            MusicaMural.enabled = true; // Activa el collider
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            if (jugadorEnMural)
            {
                audioSource.UnPause(); // Reanuda el audio
            }
            else
            {
                audioSource.Play(); // Reproduce el audio
            }

            MuralActivo = true; // Activa el mural
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera") && CamaraPOV.Nivel == 3)
        {
            audioSource.Pause(); // Pausa el audio
            jugadorEnMural = true;
        }

        MuralActivo = false; // Desactiva el mural
    }
}
