using UnityEngine;
using UnityEngine.UI;

public class Ancla : MonoBehaviour
{
    public GameObject barco; // Objeto que se activará
    public Text textoRecarga; // Texto de recarga
    private bool jugadorHaTocado = false; // Detecta si el jugador está en el ancla
    public GameObject jugador; // Referencia al jugador
    private CamaraPOV camaraPOV; // Referencia al script CamaraPOV

    private void Start()
    {
        barco.SetActive(false); // Ocultar barco al inicio
        textoRecarga.enabled = false; // Ocultar texto de recarga
        camaraPOV = jugador.GetComponent<CamaraPOV>(); // Obtener script CamaraPOV
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("El jugador ha tocado el ancla. Presiona 'Tab' para activarlo.");
            jugadorHaTocado = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            jugadorHaTocado = false; // Resetea la condición cuando el jugador sale del área
        }
    }

    private void Update()
    {
        if (jugadorHaTocado && Input.GetKeyDown(KeyCode.Tab))
        {
            ActivarBarco();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DesactivarBarco();
        }
    }

    void ActivarBarco()
    {
        barco.SetActive(true); // Activar barco
        Oxigeno.consumoBase = -2f; // Detener consumo de oxígeno
        Oxigeno.multiplicadorConsumo = 0f;
        OxygenController.areaOxigeno = true;
        textoRecarga.enabled = true; // Mostrar texto de recarga

        // **Desactivar el script CamaraPOV solo si estamos en el ancla**
            camaraPOV.enabled = false;
            Debug.Log("¡Barco activado! La cámara está desactivada.");
    }

    void DesactivarBarco()
    {
        barco.SetActive(false); // Ocultar barco
        Oxigeno.consumoBase = 1f; // Restaurar consumo de oxígeno normal
        OxygenController.areaOxigeno = false;
        textoRecarga.enabled = false; // Ocultar texto de recarga

        // **Reactivar el script CamaraPOV solo si el jugador estaba en el ancla**
            camaraPOV.enabled = true;
            Debug.Log("El barco se ha desactivado y la cámara vuelve a funcionar.");
    }
}