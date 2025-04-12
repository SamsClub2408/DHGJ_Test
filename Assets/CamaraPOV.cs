using UnityEngine;

public class CamaraPOV : MonoBehaviour
{
    public float speed = 5f; // Velocidad del movimiento
    public float minX, maxX; // L�mites en el eje X
    public float minY, maxY; // L�mites en el eje Y

    void Update()
    {
        // Obtener el movimiento basado en las teclas presionadas
        float moveX = Input.GetAxis("Horizontal"); // A y D para izquierda y derecha
        float moveY = Input.GetAxis("Vertical");   // W y S para arriba y abajo

        // Calcular nueva posici�n
        Vector3 move = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + move;

        // Aplicar l�mites
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Actualizar posici�n de la c�mara
        transform.position = newPosition;
    }
}
