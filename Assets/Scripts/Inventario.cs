using UnityEngine;
using System.Collections.Generic;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;
    public GameObject inventarioPanel; // Referencia al panel del inventario
    public List<string> itemsRecogidos = new List<string>(); // Lista de ítems recogidos
    public List<GameObject> imagenesInventario; // Lista de imágenes asociadas a los ítems

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

    public void AñadirItem(string nombreItem)
    {
        itemsRecogidos.Add(nombreItem); // Agrega el nombre del ítem a la lista
    }

    void ActualizarInventario()
    {
        foreach (GameObject imagen in imagenesInventario)
        {
            imagen.SetActive(false); // Desactiva todas las imágenes primero
        }

        foreach (string item in itemsRecogidos)
        {
            foreach (GameObject imagen in imagenesInventario)
            {
                if (imagen.name == item) // Activa solo las imágenes de los ítems recogidos
                {
                    imagen.SetActive(true);
                }
            }
        }
    }
}