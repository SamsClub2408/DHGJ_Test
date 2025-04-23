using UnityEngine;
using UnityEngine.UI;

public class Oxigeno : MonoBehaviour
{
    // Referencia al slider de UI
    public Slider oxigenoSlider;

    // Configuración de oxígeno
    public float oxigenoMaximo = 100f;
    private float oxigenoActual;
    public float consumoBase = 1f; // Consumo por segundo
    public float multiplicadorConsumo = 2f; // Aumento por umbral

    // Umbrales en Y (de mayor a menor profundidad)
    public float umbral1 = 5f; // Umbral más alto
    public float umbral2 = 3f;
    public float umbral3 = 1f; // Umbral más bajo

    void Start()
    {
        oxigenoActual = oxigenoMaximo;
        oxigenoSlider.maxValue = oxigenoMaximo;
        oxigenoSlider.value = oxigenoActual;
    }

    void Update()
    {
        // Calcular consumo según umbrales
        float consumoTotal = consumoBase;

        if (transform.position.y < umbral1)
        {
            Debug.Log("Umbral 1 alcanzado: " + transform.position.y);
            consumoTotal += multiplicadorConsumo;
        }
        if (transform.position.y < umbral2)
        {
            Debug.Log("Umbral 2 alcanzado: " + transform.position.y);
            consumoTotal += multiplicadorConsumo;
        }
        if (transform.position.y < umbral3)
        {
            Debug.Log("Umbral 3 alcanzado: " + transform.position.y);
            consumoTotal += multiplicadorConsumo;
        }

        // Reducir oxígeno
        oxigenoActual -= consumoTotal * Time.deltaTime;
        oxigenoActual = Mathf.Clamp(oxigenoActual, 0f, oxigenoMaximo);

        // Actualizar slider
        oxigenoSlider.value = oxigenoActual;
    }

    // Validación de umbrales en el editor
    void OnValidate()
    {
        // Ordenar umbrales automáticamente
        float[] umbrales = { umbral1, umbral2, umbral3 };
        System.Array.Sort(umbrales);
        System.Array.Reverse(umbrales);

        umbral1 = umbrales[0];
        umbral2 = umbrales[1];
        umbral3 = umbrales[2];
    }
}
