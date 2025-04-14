using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CamaraPOV : MonoBehaviour
{
    public float speed = 5f; // Velocidad del movimiento
    public float minX, maxX; // L�mites en el eje X
    public float minY, maxY; // L�mites en el eje Y
    public Light2D darkness; // Oscuridad Global
    public float minIntensity = 0.1f; // Intensidad m�nima de la oscuridad
    public float maxIntensity = 1f; // Intensidad m�xima de la oscuridad

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

        AjustarIntensidad(); // Llamar a la funci�n para ajustar la intensidad de la luz
    }

    void AjustarIntensidad()
    {
        if(darkness == null)
        {
            Debug.LogError("No se encontr� el componente Light2D en el objeto.");
            return;
        }

        // Calcular la distancia relativa del limite en Y
        float distanceToMinY = Mathf.Abs(transform.position.y - minY);

        // Normalizar la distancia en un rango de 0 a 1
        float normalizedDistanceY = Mathf.InverseLerp(0, maxY - minY, distanceToMinY);

        // Ajustar la intensidad de la luz basada en la distancia en Y
        darkness.intensity = Mathf.Lerp(minIntensity, maxIntensity, normalizedDistanceY);
    }
}
