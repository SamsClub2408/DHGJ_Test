using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CamaraPOV : MonoBehaviour
{
    public float speed = 5f;
    public float minX, maxX;
    public float minY, maxY;
    public Light2D darkness;
    public float minIntensity = 0.1f;
    public float maxIntensity = 1f;

    void Update()
    {
        // Movimiento básico en 2D sin rotación
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Calcular nueva posición
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        // Aplicar límites
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Actualizar posición
        transform.position = newPosition;

        // Ajustar intensidad de oscuridad
        AjustarIntensidad();
    }

    void AjustarIntensidad()
    {
        if (darkness == null) return;

        // Calcular intensidad basada en posición Y
        float distanceToMinY = Mathf.Abs(transform.position.y - minY);
        float normalizedDistanceY = Mathf.InverseLerp(0, maxY - minY, distanceToMinY);
        darkness.intensity = Mathf.Lerp(minIntensity, maxIntensity, normalizedDistanceY);
    }
}