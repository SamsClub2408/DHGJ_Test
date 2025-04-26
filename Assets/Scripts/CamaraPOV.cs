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

    public static int Nivel = 1;
    public Transform PosicionNivel2;
    public Transform PosicionNivel3;


    //Manos Nivel 1
    public SpriteRenderer manoIzquierda1;
    public SpriteRenderer manoDerecha1;

    //Manos Nivel 2
    public SpriteRenderer manoIzquierda2;
    public SpriteRenderer manoDerecha2;

    //Manos Nivel 3
    public SpriteRenderer manoIzquierda3;
    public SpriteRenderer manoDerecha3;

    //Colliders Nivel 1
    public EdgeCollider2D colliderNv1;
    public CircleCollider2D Mural1;

    public string etiquetaNivel1 = "L1"; 
    public string etiquetaNivel2 = "L2";

    private void Start()
    {
        if(Nivel == 1)
        {
            manoDerecha1.enabled = true; // Activa la mano derecha del nivel 1
            manoIzquierda1.enabled = true; // Activa la mano izquierda del nivel 1
            colliderNv1.enabled = true; // Activa el collider del nivel 1
            DesactivarObjetosPorEtiqueta(etiquetaNivel2);
        }

        if (Nivel == 2)
        {
            transform.position = PosicionNivel2.position; // Cambia la posición de la cámara al inicio del nivel 2
            manoDerecha1.enabled = false; // Desactiva la mano derecha del nivel 1
            manoIzquierda1.enabled = false; // Desactiva la mano izquierda del nivel 1
            manoDerecha2.enabled = true; // Activa la mano derecha del nivel 2
            manoIzquierda2.enabled = true; // Activa la mano izquierda del nivel 2
            colliderNv1.enabled = false; // Desactiva el collider del nivel 1
            DesactivarObjetosPorEtiqueta(etiquetaNivel1);
        }
        if (Nivel == 3)
        {
            transform.position = PosicionNivel3.position; // Cambia la posición de la cámara al inicio del nivel 2
            manoDerecha1.enabled = false; // Desactiva la mano derecha del nivel 1
            manoIzquierda1.enabled = false; // Desactiva la mano izquierda del nivel 1
            manoDerecha2.enabled = false; // Activa la mano derecha del nivel 2
            manoIzquierda2.enabled = false; // Activa la mano izquierda del nivel 2
            manoDerecha3.enabled = true; // Activa la mano derecha del nivel 2
            manoIzquierda3.enabled = true; // Activa la mano izquierda del nivel 2
            DesactivarObjetosPorEtiqueta(etiquetaNivel1);
            DesactivarObjetosPorEtiqueta(etiquetaNivel2);
        }
    }

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

    void DesactivarObjetosPorEtiqueta(string etiqueta)
    {
        //Encontrar objetos con el nombre del inspector
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(etiqueta);
        foreach (GameObject obj in objetos)
        {
            obj.SetActive(false);
        }

        Debug.Log("Todos los objetos con etiqueta '" + etiqueta + "' han sido desactivados.");
    }

    // Opcional: Validación para el editor de Unity
    private void OnValidate()
    {
        // Asegura que el límite esté dentro del rango permitido
        darknessLimitY = Mathf.Clamp(darknessLimitY, minY, maxY);
    }
}