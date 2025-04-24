using UnityEngine;
using System.Collections.Generic;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;
    public GameObject inventarioPanel; // Referencia al panel del inventario
    public List<string> itemsRecogidos = new List<string>(); // Lista de �tems recogidos
    public List<GameObject> imagenesInventario; // Lista de im�genes asociadas a los �tems

    private bool inventarioActivo = false;

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