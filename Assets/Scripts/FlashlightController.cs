using UnityEngine;
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
        if (isFlashlightOn)
        {
            currentEnergy -= drainRate * Time.deltaTime; // Drenar energia
            currentEnergy = Mathf.Max(currentEnergy, 0f); // Limitar la energía a un mínimo de 0
            UpdateEnergy(); // Actualizar la energía

            spriteMask.enabled = true; // Activar el SpriteMask cuando la linterna está encendida

            if (currentEnergy <= 0)
            {
                ToggleFlashlight(); // Apagar la linterna si la energía se agota
                spriteMask.enabled = false; // Desactivar el SpriteMask si la energía se agota
            }
        }
        else
        {
            spriteMask.enabled = false; // Desactivar el SpriteMask cuando la linterna está apagada
        }

        // Comprobar si se presiona la tecla "SPACE"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleFlashlight();
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

            UpdateEnergy(); // Actualizar la energía
        }
    }

    void UpdateEnergy()
    {
        if (energySlider != null)
        {
            energySlider.value = currentEnergy / maxEnergy; // Actualizar el valor del slider
        }
    }
}
