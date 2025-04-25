using UnityEngine;
using UnityEngine.UI;

public class Ancla : MonoBehaviour
{
    public GameObject imagenBarco; // Imagen que se activar�
    public Slider oxigenoSlider; // Referencia al slider de ox�geno
    private bool jugadorHaTocado = false; // Detectar si el jugador toc� el ancla
    public Text textoRecarga; // Texto que muestra la recarga
    public GameObject jugador; // Referencia al jugador

    public MonoBehaviour scriptMovimiento; // Referencia al script de movimiento del jugador

    private void Start()
    {
        imagenBarco.SetActive(false); // Ocultar la imagen al inicio
        scriptMovimiento = jugador.GetComponent<MonoBehaviour>(); // Obtener el script de movimiento
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("El jugador ha tocado el ancla, presiona Tab para recargar ox�geno.");
            jugadorHaTocado = true;
        }
    }

    private void Update()
    {
        if (jugadorHaTocado && Input.GetKeyDown(KeyCode.Tab))
        {
            ActivarRecargaOxigeno();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DesactivarRecargaOxigeno();
        }
    }

    void ActivarRecargaOxigeno()
    {
        imagenBarco.SetActive(true); // Mostrar la imagen de recarga
        Oxigeno.consumoBase = -2f; // Detener consumo de ox�geno
        Oxigeno.multiplicadorConsumo = 0f;
        OxygenController.areaOxigeno = true;
        textoRecarga.enabled = true; // Mostrar el texto de recarga

        // Restaurar ox�geno al m�ximo
        oxigenoSlider.value = oxigenoSlider.maxValue;

        // **Desactivar el script de movimiento para impedir que el jugador se mueva**
        scriptMovimiento.enabled = false;

        Debug.Log("Ox�geno recargado y movimiento bloqueado hasta que presiones 'S'.");
    }

    void DesactivarRecargaOxigeno()
    {
        imagenBarco.SetActive(false); // Ocultar la imagen de recarga
        Oxigeno.consumoBase = 1f; // Restaurar consumo de ox�geno normal
        OxygenController.areaOxigeno = false;
        textoRecarga.enabled = false; // Ocultar el texto de recarga

        // **Reactivar el script de movimiento para que el jugador pueda moverse nuevamente**
        scriptMovimiento.enabled = true;

        Debug.Log("Ox�geno vuelve a consumirse y movimiento restaurado.");
    }
}