using UnityEngine;
using UnityEngine.Rendering.Universal; // Libreria para el sistema de renderizado Universal Render Pipeline (Luces xD)

public class ActivarBoss : MonoBehaviour
{
    public static bool BossActivado = false;
    public GameObject Boss; // Prefab del boss
    public Light2D flashlight; // Linterna
    private bool primeraVez = false; // Variable para controlar la activación del boss
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera") && !primeraVez)
        {
            BossActivado = true; // Cambia el estado a activado
            //FlashlightController.isFlashlightOn = false; // Desactiva la linterna
            //flashlight.enabled = false; // Desactiva la linterna
            primeraVez = true; // Cambia el estado a activado
        }
    }

    private void Update()
    {
        if (BossActivado)
        {
            Boss.SetActive(true); // Activa el boss
            BossActivado = false; // Reinicia el estado
        }
    }
}
