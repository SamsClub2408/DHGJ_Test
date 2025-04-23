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
    [Tooltip("Posición Y por debajo de la cual la luz se mantiene en su mínimo")]
    public float darknessLimitY;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
        AjustarIntensidad();
    }

    void AjustarIntensidad()
    {
        if (darkness == null) return;

        float currentY = transform.position.y;

        // Si está por debajo del límite, intensidad mínima
        if (currentY <= darknessLimitY)
        {
            darkness.intensity = minIntensity;
        }
        else
        {
            // Interpola entre el límite y el máximo Y permitido
            float t = Mathf.InverseLerp(darknessLimitY, maxY, currentY);
            darkness.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        }
    }

    // Opcional: Validación para el editor de Unity
    private void OnValidate()
    {
        // Asegura que el límite esté dentro del rango permitido
        darknessLimitY = Mathf.Clamp(darknessLimitY, minY, maxY);
    }
}