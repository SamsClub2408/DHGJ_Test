using UnityEngine;

public class ControlMuerteBarco : MonoBehaviour
{
    public GameObject[] objetosOcultar; // Lista de objetos que se ocultarán
    public GameObject[] objetosMostrar; // Lista de objetos que aparecerán
    public Animator barcoAnimator; // ✅ Referencia al Animator del barco
    public string nombreAnimacionMuerte = "MuerteBarco"; // ✅ Nombre de la animación que queremos activar

    private void Start()
    {
        VerificarEstadoMuerte();
    }

    private void Update()
    {
        VerificarEstadoMuerte();
    }

    void VerificarEstadoMuerte()
    {
        if (ActivaMuerte.muerte) // ✅ Si la variable global `muerte` es true
        {
            OcultarObjetos();
            MostrarObjetos();
            ActivarAnimacionMuerte();
            Debug.Log("Estado de muerte activo. Objetos ajustados en Barco.");
        }
    }

    void OcultarObjetos()
    {
        foreach (GameObject obj in objetosOcultar)
        {
            obj.SetActive(false); // Oculta cada objeto de la lista
        }
    }

    void MostrarObjetos()
    {
        foreach (GameObject obj in objetosMostrar)
        {
            obj.SetActive(true); // Muestra cada objeto de la lista
        }
    }

    void ActivarAnimacionMuerte()
    {
        if (!barcoAnimator.GetCurrentAnimatorStateInfo(0).IsName(nombreAnimacionMuerte))
        {
            barcoAnimator.Play(nombreAnimacionMuerte); // ✅ Reproduce la animación si aún no ha comenzado
            Debug.Log("¡Animación 'MuerteBarco' activada!");
        }
    }
}