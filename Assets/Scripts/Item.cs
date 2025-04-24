using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string nombreItem; // Nombre del �tem
    private bool jugadorHaTocado = false; // Variable para detectar la colisi�n

    public static bool objetoRecogido01 = false; // Variable para indicar si el objeto clave ha sido recogido
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
        }
    }

    void AgregarAInventario()
    {
        Inventario.instance.A�adirItem(nombreItem); // Guarda el �tem en el inventario
        Debug.Log("Objeto recogido y guardado en el inventario.");
        Destroy(gameObject); // Elimina el �tem de la escena
    }
}