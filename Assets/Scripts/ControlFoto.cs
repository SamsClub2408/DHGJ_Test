using UnityEngine;
using System.Collections;

public class ControlFoto : MonoBehaviour
{
    private Animator animator;
    private bool fotoMostrada = false;
    private bool puedePresionarE = false; // Estado que bloquea la tecla E

    public GameObject txtObjeto; // Referencia al texto
    public Animator instruccionesAnimator; // Referencia al Animator de las instrucciones

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(EsperarFinAnimacion()); // Espera a que termine la animación inicial
    }

    IEnumerator EsperarFinAnimacion()
    {
        yield return new WaitForSeconds(15f); // Espera la duración de la animación `Fade`
        puedePresionarE = true; // Ahora se puede presionar E
    }

    void Update()
    {
        if (puedePresionarE && Input.GetKeyDown(KeyCode.E))
        {
            fotoMostrada = !fotoMostrada;
            animator.SetBool("fotoMostrada", fotoMostrada);
            txtObjeto.SetActive(!fotoMostrada);
        }
    }
}