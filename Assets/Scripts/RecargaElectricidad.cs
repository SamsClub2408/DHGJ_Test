using UnityEngine;
using UnityEngine.UI;

public class RecargaElectricidad : MonoBehaviour
{
    public FlashlightController flashlightController; // Referencia al controlador de la linterna
    public bool isCharging = false; // Estado de carga de energía

    //Temporal
    public Text textoRecarga; // Referencia al texto de recarga

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LinternaArea"))
        {
            isCharging = true;
            flashlightController.drainRate = 0f; // Detener el drenaje de energía
            textoRecarga.enabled = true; // Mostrar el texto de recarga
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LinternaArea"))
        {
            isCharging = false;
            flashlightController.drainRate = 10f; // Reiniciar el drenaje de energía
            textoRecarga.enabled = false; // Ocultar el texto de recarga
        }
    }
}
