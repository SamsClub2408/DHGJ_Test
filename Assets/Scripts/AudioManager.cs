using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip L_click, L_Sound, C_Attack;

    private void Update()
    {
        if(AudioTrigger.Atacado)
        {
            AtaqueLamprea();
            Debug.Log("Atacado");
        }
    }

    public void Pausar()
    {
        //Pausar el audio
        audioSource.Stop();
        audioSource.loop = false;
    }

    public void LinternaClick()
    {
        //Reproducir una vez el sonido de la linterna
        audioSource.PlayOneShot(L_click);
    }

    public void Linterna()
    {
        //Reproducir el sonido de la linterna en bucle
        if (!audioSource.isPlaying)
        {
            audioSource.clip = L_Sound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void AtaqueLamprea()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(C_Attack);
        }
    }
}
