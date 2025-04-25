using UnityEngine;

public class ConfigurarEscenaBarco : MonoBehaviour
{
    public GameObject[] objetosNivel1; // Objetos a desactivar si vienes del Nivel 1
    public GameObject[] objetosNivel2; // Objetos a desactivar si vienes del Nivel 2
    public GameObject objetoMostrarNivel1; // Objeto que se activa si vienes de Nivel 1
    public GameObject objetoMostrarNivel2; // Objeto que se activa si vienes de Nivel 1
    public GameObject[] objetosMuerteDesactivar; // Objetos que se desactivan cuando "Muerte" es true
    public GameObject[] objetosMuerteActivar; // Objetos que se activan cuando "Muerte" es true

    void Start()
    {
        Debug.Log("Nivel actual: " + CamaraPOV.Nivel); // Para verificar en consola

        if (CamaraPOV.Nivel == 1)
        {
            DesactivarObjetos(objetosNivel1);
            ActivarObjetoNivel1();

        }
        else if (CamaraPOV.Nivel == 2)
        {
            DesactivarObjetos(objetosNivel2);
            ActivarObjetoNivel2();
        }
    }

    void Update()
    {
        if (CamaraPOV.Muerte) // Si Muerte es true
        {
            DesactivarObjetos(objetosMuerteDesactivar);
            ActivarObjetos(objetosMuerteActivar);
        }
    }

    void DesactivarObjetos(GameObject[] objetos)
    {
        foreach (GameObject obj in objetos)
        {
            if (obj != null) obj.SetActive(false);
        }
        Debug.Log("Objetos desactivados.");
    }

    void ActivarObjetos(GameObject[] objetos)
    {
        foreach (GameObject obj in objetos)
        {
            if (obj != null) obj.SetActive(true);
        }
        Debug.Log("Objetos activados.");
    }

    void ActivarObjetoNivel1()
    {
        if (objetoMostrarNivel1 != null)
        {
            objetoMostrarNivel1.SetActive(true);
            Debug.Log("Objeto activado porque el jugador llegó desde el Nivel 1: " + objetoMostrarNivel1.name);
        }
    }
    void ActivarObjetoNivel2()
    {
        if (objetoMostrarNivel1 != null)
        {
            objetoMostrarNivel1.SetActive(true);
            Debug.Log("Objeto activado porque el jugador llegó desde el Nivel 1: " + objetoMostrarNivel1.name);
        }
    }
}