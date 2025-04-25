using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string nombreItem; // Nombre del ítem
    private bool jugadorHaTocado = false; // Variable para detectar la colisión

    public static bool objetoRecogido01 = false; // Variable para indicar si el objeto clave ha sido recogido
    public static bool objetoRecogido02 = false;

    void Start()
    {
        if (GestorEstado.instancia.ObjetoFueRecogido(nombreItem))
        {
            Debug.Log("Este objeto ya fue recogido, eliminándolo de la escena.");
            Destroy(gameObject); // Si el objeto fue recogido antes, lo eliminamos.
        }
    }
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("El jugador ha tocado el objeto, presiona X para recogerlo.");
            jugadorHaTocado = true;
        }
    }

    private void Update()
    {
        if (jugadorHaTocado && Input.GetKeyDown(KeyCode.Tab))
        {
            AgregarAInventario();
            if(nombreItem == "Aleta")
            {
                Debug.Log("Item Trigger");
                objetoRecogido01 = true; // Cambia el estado de recogido
            }
            if(nombreItem == "Vasija1")
            {
                Debug.Log("Item Trigger");
                objetoRecogido02 = true; // Cambia el estado de recogido
            }
        }
    }

    void AgregarAInventario()
    {
        GestorEstado.instancia.AñadirItem(nombreItem); // Guarda el ítem en GestorEstado
        Debug.Log("Objeto recogido y guardado en el inventario.");
        Destroy(gameObject); // Elimina el ítem de la escena
    }
}