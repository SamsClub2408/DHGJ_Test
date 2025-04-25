using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Libreria para cargar escenas

public class Oxigeno : MonoBehaviour
{
    // Referencia al slider de UI
    public Slider oxigenoSlider;

    // Configuración de oxígeno
    public float oxigenoMaximo = 100f;
    public static float oxigenoActual;
    public static float consumoBase = 1f; // Consumo por segundo
    public static float multiplicadorConsumo = 2f; // Aumento por umbral

    // Umbrales en Y (de mayor a menor profundidad)
    public float umbral1 = 5f; // Umbral más alto
    public float umbral2 = 3f;
    public float umbral3 = 1f; // Umbral más bajo

    public static int umbralAlcanzado = 0; // Contador de umbrales alcanzados

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

        if (transform.position.y < umbral1 && !OxygenController.areaOxigeno)
        {
            //Debug.Log("Umbral 1 alcanzado: " + consumoTotal);
            consumoTotal += multiplicadorConsumo;
            umbralAlcanzado = 1; // Aumentar el contador de umbrales alcanzados
        }
        if (transform.position.y < umbral2 && !OxygenController.areaOxigeno)
        {
            //Debug.Log("Umbral 2 alcanzado: " + consumoTotal);
            consumoTotal += multiplicadorConsumo;
            umbralAlcanzado = 2; // Aumentar el contador de umbrales alcanzados
        }
        if (transform.position.y < umbral3 && !OxygenController.areaOxigeno)
        {
            //Debug.Log("Umbral 3 alcanzado: " + consumoTotal);
            consumoTotal += multiplicadorConsumo;
            umbralAlcanzado = 3; // Aumentar el contador de umbrales alcanzados
        }

        // Reducir oxígeno
        oxigenoActual -= consumoTotal * Time.deltaTime;
        oxigenoActual = Mathf.Clamp(oxigenoActual, 0f, oxigenoMaximo);

        //Muerte
        if(oxigenoActual <= 0)
        {
            //Debug.Log("Te has ahogado");
            SceneManager.LoadScene("Muerte");
        }

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
