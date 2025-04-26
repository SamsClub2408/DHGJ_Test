using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource,audioSource1,audioSource2, audioSource3,audioSource4;
    public AudioClip L_click, L_Sound, C_Attack, BgLvl1, BgLvl2, OpenInv, CloseInv;
    public AudioClip OxygenRecharge, LowOxygen;

    private bool inventarioAbierto = true;

    private void Update()
    {
        if(AudioTrigger.Atacado)
        {
            StartCoroutine(GritoLamprea());
            Debug.Log("Atacado");
        }

        if(BossBehavior.BossKilledYou)
        {
            audioSource.pitch = 0.45f; // Asegura el pitch estándar
            AtaqueLamprea();
        }

        if(Inventario.inventarioActivo && inventarioAbierto)
        {
            AbrirInventario();
            inventarioAbierto = false;
        }
        if (!Inventario.inventarioActivo && !inventarioAbierto)
        {
            CerrarInventario();
            inventarioAbierto = true;
        }

        //Umbral 1
        if(Oxigeno.oxigenoActual < 8f && Oxigeno.umbralAlcanzado <=1)
        {
            OxigenoBajo();
            if (Inventario.Pausa)
            {
                audioSource4.Pause(); // Pausa el clip de audio
            }
            else if (!Inventario.Pausa)
            {
                audioSource4.UnPause(); // Reanuda el clip de audio
            }
        }

        //Umbral 2
        if (Oxigeno.oxigenoActual < 24f && Oxigeno.umbralAlcanzado == 2)
        {
            OxigenoBajo();
            if (Inventario.Pausa)
            {
                audioSource4.Pause(); // Pausa el clip de audio
            }
            else if (!Inventario.Pausa)
            {
                audioSource4.UnPause(); // Reanuda el clip de audio
            }
        }

        //Umbral 3
        if (Oxigeno.oxigenoActual < 40f && Oxigeno.umbralAlcanzado == 3)
        {
            OxigenoBajo();
            if (Inventario.Pausa)
            {
                audioSource4.Pause(); // Pausa el clip de audio
            }
            else if (!Inventario.Pausa)
            {
                audioSource4.UnPause(); // Reanuda el clip de audio
            }
        }

        if (OxygenController.areaOxigeno)
        {
            RecargarOxigeno();
        }
        else
        {
            audioSource3.Stop();
        }

        if (CamaraPOV.Nivel == 1)
        {
            //Musica
            if (!audioSource1.isPlaying)
            {
                audioSource1.clip = BgLvl1;
                audioSource1.Play();
            }
        }

        if (CamaraPOV.Nivel == 2)
        {
            //Musica
            if (!audioSource1.isPlaying)
            {
                audioSource1.clip = BgLvl2;
                audioSource1.Play();
            }
        }

        if (CamaraPOV.Nivel == 3 && !MusicaSacra.MuralActivo)
        {
            //Musica
            if (!audioSource1.isPlaying)
            {
                audioSource.volume = 1f; // Asegura que el volumen esté al máximo
                audioSource1.clip = BgLvl2;
                audioSource1.Play();
            }
        }
        else if (CamaraPOV.Nivel == 3 && MusicaSacra.MuralActivo)
        {
            //Bajar el volumen a la mitad
            audioSource1.volume = 0.5f;
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

    public void AbrirInventario()
    {
        audioSource2.PlayOneShot(OpenInv); // Reproduce el clip de audio una vez 
    }

    public void CerrarInventario()
    {
        audioSource2.PlayOneShot(CloseInv); // Reproduce el clip de audio una vez 
    }

    public void RecargarOxigeno()
    {
        if (!audioSource3.isPlaying)
        {
            audioSource3.clip = OxygenRecharge;
            audioSource3.loop = false;
            audioSource3.Play(); // Reproduce el clip de audio una vez
        }
    }

    public void OxigenoBajo()
    {
        if (!audioSource4.isPlaying)
        {
            audioSource4.clip = LowOxygen;
            audioSource4.Play();
        }
    }

    IEnumerator GritoLamprea()
    {
        yield return new WaitForSeconds(1f); // Espera 0.5 segundos antes de reproducir el sonido
        AtaqueLamprea();
    }
}
