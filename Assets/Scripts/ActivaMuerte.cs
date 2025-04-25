using UnityEngine;

public class ActivaMuerte : MonoBehaviour
{
    public static bool muerte = false; // ✅ Variable global de muerte

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player")) // Verificar si el objeto en colisión es el jugador
        {
            muerte = true; // Activar la variable global de muerte
            Debug.Log("¡El jugador ha activado la muerte!");
        }
    }
}