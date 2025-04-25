using UnityEngine;
using System.Collections.Generic;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;
    public GameObject inventarioPanel; // Referencia al panel del inventario
    public List<string> itemsRecogidos = new List<string>(); // Lista de �tems recogidos
    public List<GameObject> imagenesInventario; // Lista de im�genes asociadas a los �tems

    public static bool inventarioActivo = false;

    public static bool Pausa = false; // Variable para pausar el juego

    private void Awake()
    {
        instance = this; // Singleton para acceder desde `Item`
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Alterna el inventario con "E"
        {
            inventarioActivo = !inventarioActivo;
            inventarioPanel.SetActive(inventarioActivo);

            if (inventarioActivo)
            {
                ActualizarInventario();
            }
        }

        if(inventarioActivo)
        {
            //Pausa el juego si el inventario est� activo
            Time.timeScale = 0f;
            Pausa = true; // Cambia el estado de pausa
        }
        else
        {
            //Reanuda el juego si el inventario est� cerrado
            Time.timeScale = 1f;
            Pausa = false; // Cambia el estado de pausa
        }
    }

    public void A�adirItem(string nombreItem)
    {
        itemsRecogidos.Add(nombreItem); // Agrega el nombre del �tem a la lista
    }

    void ActualizarInventario()
    {
        foreach (GameObject imagen in imagenesInventario)
        {
            imagen.SetActive(false); // Desactiva todas las im�genes primero
        }

        foreach (string item in itemsRecogidos)
        {
            foreach (GameObject imagen in imagenesInventario)
            {
                if (imagen.name == item) // Activa solo las im�genes de los �tems recogidos
                {
                    imagen.SetActive(true);
                }
            }
        }
    }
}