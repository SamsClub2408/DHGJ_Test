using UnityEngine;
using UnityEngine.Rendering.Universal; // Libreria para el sistema de renderizado Universal Render Pipeline (Luces xD)
using UnityEngine.UI; // Libreria para la UI (Interfaz de Usuario)

public class FlashlightController : MonoBehaviour
{
    //Variables para la linterna
    public Light2D flashlight; // Variable para la linterna
    private bool isFlashlightOn = false; // Estado de la linterna

    public float maxEnergy = 100f; // Energ�a m�xima de la linterna
    public float drainRate = 10f; // Tasa de drenaje de energ�a
    private float currentEnergy; // Energ�a actual de la linterna
    public Slider energySlider; // Referencia al slider de energ�a

    public SpriteMask spriteMask; // Referencia al SpriteMask

    void Start()
    {
        if (flashlight == null)
        {
            Debug.LogError("No se encontr� el componente Light2D en el objeto.");
        }

        currentEnergy = maxEnergy; // Inicializar la energ�a actual
        UpdateEnergy(); // Actualizar la energ�a al inicio
    }

    private void Update()
    {
        //Control de consumo de energia
        if (isFlashlightOn)
        {
            currentEnergy -= drainRate * Time.deltaTime; // Drenar energia
            currentEnergy = Mathf.Max(currentEnergy, 0f); // Limitar la energ�a a un m�nimo de 0
            UpdateEnergy(); // Actualizar la energ�a

            spriteMask.enabled = true; // Activar el SpriteMask cuando la linterna est� encendida

            if (currentEnergy <= 0)
            {
                ToggleFlashlight(); // Apagar la linterna si la energ�a se agota
                spriteMask.enabled = false; // Desactivar el SpriteMask si la energ�a se agota
            }
        }
        else
        {
            spriteMask.enabled = false; // Desactivar el SpriteMask cuando la linterna est� apagada
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
                Debug.Log("No hay energ�a suficiente para encender la linterna.");
                return; // No hacer nada si no hay energ�a
            }

            isFlashlightOn = !isFlashlightOn; // Cambiar el estado de la linterna
            flashlight.enabled = isFlashlightOn; // Activar o desactivar la linterna

            UpdateEnergy(); // Actualizar la energ�a
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
