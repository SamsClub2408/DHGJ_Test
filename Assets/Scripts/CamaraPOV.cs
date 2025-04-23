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
    [Tooltip("Posici�n Y por debajo de la cual la luz se mantiene en su m�nimo")]
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

        // Si est� por debajo del l�mite, intensidad m�nima
        if (currentY <= darknessLimitY)
        {
            darkness.intensity = minIntensity;
        }
        else
        {
            // Interpola entre el l�mite y el m�ximo Y permitido
            float t = Mathf.InverseLerp(darknessLimitY, maxY, currentY);
            darkness.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        }
    }

    // Opcional: Validaci�n para el editor de Unity
    private void OnValidate()
    {
        // Asegura que el l�mite est� dentro del rango permitido
        darknessLimitY = Mathf.Clamp(darknessLimitY, minY, maxY);
    }
}