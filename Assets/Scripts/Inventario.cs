using UnityEngine;

public class Inventario : MonoBehaviour
{
    public GameObject inventarioPanel; // Referencia al panel del inventario
    private bool inventarioActivo = false; // Estado del inventario

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Si presiona "E"
        {
            inventarioActivo = !inventarioActivo; // Alterna entre activo e inactivo
            inventarioPanel.SetActive(inventarioActivo);
        }
    }
}
