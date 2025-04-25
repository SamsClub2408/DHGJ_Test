using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string nombreItem; // Nombre del ítem
    private bool jugadorHaTocado = false; // Variable para detectar la colisión

    public static bool objetoRecogido01 = false; // Variable para indicar si el objeto clave ha sido recogido
    public static bool objetoRecogido02 = false;

    private Animator lesbianaAnimator; // Asigna el animator de la lesbiana en el inspector

    private void Start()
    {
        if (CamaraPOV.Nivel == 1)
        {
            //Obtiene el animator de un objeto llamado "Lesbiana"
            lesbianaAnimator = GameObject.Find("Lesbiana").GetComponent<Animator>();
        }
        else
        {
            lesbianaAnimator = null; // Si no está en el nivel 1, no asigna el animator
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
                lesbianaAnimator.SetTrigger("Vasija1Activado"); // Activa el trigger del animator
                objetoRecogido02 = true; // Cambia el estado de recogido
            }
        }
    }

    void AgregarAInventario()
    {
        Inventario.instance.AñadirItem(nombreItem); // Guarda el ítem en el inventario
        Debug.Log("Objeto recogido y guardado en el inventario.");
        Destroy(gameObject); // Elimina el ítem de la escena
    }
}