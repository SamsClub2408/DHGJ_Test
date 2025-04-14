using UnityEngine;
using UnityEngine.Rendering.Universal; // Libreria para el sistema de renderizado Universal Render Pipeline (Luces xD)

public class FlashlightController : MonoBehaviour
{
    public Light2D flashlight; // Variable para la linterna
    private bool isFlashlightOn = false; // Estado de la linterna

    void Start()
    {
        flashlight.enabled = false; // Asegurarse de que la linterna esté apagada al inicio

        if (flashlight == null)
        {
            Debug.LogError("No se encontró el componente Light2D en el objeto.");
        }
    }

    private void Update()
    {
        // Comprobar si se presiona la tecla "F"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        if (flashlight != null)
        {
            isFlashlightOn = !isFlashlightOn; // Cambiar el estado de la linterna
            flashlight.enabled = isFlashlightOn; // Activar o desactivar la linterna
        }
    }
}
