using UnityEngine;
using UnityEngine.UI;

public class OxygenController : MonoBehaviour
{
    public static bool areaOxigeno = false;
    public Text textoRecarga; // Referencia al texto de recarga
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AreaOxigeno"))
        {
            Oxigeno.consumoBase = -2f;
            Oxigeno.multiplicadorConsumo = 0f; // Detener el consumo de oxígeno
            areaOxigeno = true; // Activar el área de oxígeno
            textoRecarga.enabled = true; // Mostrar el texto de recarga
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AreaOxigeno"))
        {
            if (Oxigeno.umbralAlcanzado <= 1)
            {
                Oxigeno.consumoBase = 1f;
            }
            if (Oxigeno.umbralAlcanzado == 2)
            {
                Oxigeno.multiplicadorConsumo = -3f;
            }
            if (Oxigeno.umbralAlcanzado == 3)
            {
                Oxigeno.consumoBase = -5f;
            }


            areaOxigeno = false; // Desactivar el área de oxígeno
            textoRecarga.enabled = false; // Ocultar el texto de recarga
        }
    }
}
