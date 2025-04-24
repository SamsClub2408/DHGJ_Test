using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal; // Libreria para el sistema de renderizado Universal Render Pipeline (Luces xD)
using UnityEngine.UI; // Libreria para la UI (Interfaz de Usuario)

public class FlashlightController : MonoBehaviour
{
    //Variables para la linterna
    public Light2D flashlight; // Variable para la linterna
    private bool isFlashlightOn = false; // Estado de la linterna

    public float maxEnergy = 100f; // Energía máxima de la linterna
    public float drainRate = 10f; // Tasa de drenaje de energía
    private float currentEnergy; // Energía actual de la linterna
    public Slider energySlider; // Referencia al slider de energía

    public SpriteMask spriteMask; // Referencia al SpriteMask

    public RecargaElectricidad recargaElectricidad; // Referencia al script de recarga de energía

    //Referencia a AudioManager
    public AudioManager audioManager; // Referencia al AudioManager

    void Start()
    {
        if (flashlight == null)
        {
            Debug.LogError("No se encontró el componente Light2D en el objeto.");
        }

        currentEnergy = maxEnergy; // Inicializar la energía actual
        UpdateEnergy(); // Actualizar la energía al inicio
    }

    private void Update()
    {
        //Control de consumo de energia
        if (isFlashlightOn && recargaElectricidad.isCharging == false)
        {
            drainRate = 10f; // Reiniciar el drenaje de energía
            currentEnergy -= drainRate * Time.deltaTime; // Drenar energia
            currentEnergy = Mathf.Max(currentEnergy, 0f); // Limitar la energía a un mínimo de 0
            UpdateEnergy(); // Actualizar la energía

            spriteMask.enabled = true; // Activar el SpriteMask cuando la linterna está encendida

            //Sonido de la linterna
            audioManager.Linterna(); // Reproducir el sonido de la linterna

            if (currentEnergy <= 0)
            {
                ToggleFlashlight(); // Apagar la linterna si la energía se agota
                spriteMask.enabled = false; // Desactivar el SpriteMask si la energía se agota
                audioManager.LinternaClick(); // Reproducir el sonido de la linterna

                //llamar a la corrutina Energia Acabada
                StartCoroutine(EnergiaAcabada());
            }
        }
        else
        {
            spriteMask.enabled = false; // Desactivar el SpriteMask cuando la linterna está apagada
            //audioManager.Pausar(); // Pausar el sonido de la linterna
        }

        //Sonido de linterna si esta en zona de recarga
        if(isFlashlightOn && recargaElectricidad.isCharging)
        {
            audioManager.Linterna(); // Reproducir el sonido de la linterna
        }

        // Recarga de Energia
        if (recargaElectricidad != null && recargaElectricidad.isCharging && !isFlashlightOn)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentEnergy += 5f; // Cantidad personalizable
                currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
                UpdateEnergy();
            }
        }

        // Comprobar si se presiona la tecla "SPACE"
        if (Input.GetKeyDown(KeyCode.Space) && !Inventario.Pausa)
        {
            ToggleFlashlight();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && Inventario.Pausa)
        {
            //No hace nada
        }
    }

    void ToggleFlashlight()
    {
        if (flashlight != null)
        {
            if(!isFlashlightOn && currentEnergy <=0)
            {
                Debug.Log("No hay energía suficiente para encender la linterna.");
                return; // No hacer nada si no hay energía
            }

            isFlashlightOn = !isFlashlightOn; // Cambiar el estado de la linterna
            flashlight.enabled = isFlashlightOn; // Activar o desactivar la linterna

            audioManager.LinternaClick(); // Reproducir el sonido de la linterna
            UpdateEnergy(); // Actualizar la energía

            if(!isFlashlightOn && currentEnergy>=0)
            {
                audioManager.LinternaClick(); // Reproducir el sonido de la linterna

                StartCoroutine(EnergiaAcabada()); // Llamar a la corrutina EnergiaAcabada
            }

        }
    }

    void UpdateEnergy()
    {
        if (energySlider != null)
        {
            energySlider.value = currentEnergy / maxEnergy; // Actualizar el valor del slider
        }
    }

    private IEnumerator EnergiaAcabada()
    {
        yield return new WaitForSeconds(0.5f); // Esperar a
        audioManager.Pausar(); // Pausar el sonido de la linterna
    }
}
