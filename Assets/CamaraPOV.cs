using UnityEngine;

public class CamaraPOV : MonoBehaviour
{
    public float speed = 5f; // Velocidad del movimiento

    void Update()
    {
        // Obtener el movimiento basado en las teclas presionadas
        float moveX = Input.GetAxis("Horizontal"); // A y D para izquierda y derecha
        float moveY = Input.GetAxis("Vertical");   // W y S para arriba y abajo

        // Modificar la posición de la cámara
        transform.position += new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
    }
}
