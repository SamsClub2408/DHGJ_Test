using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public Animator bossAnimator; // ✅ Referencia al Animator
    public GameObject bossSprite; // ✅ Sprite del Boss (centrado en la cámara)
    public GameObject objetoActivar; // ✅ Objeto oculto que se activará después de la animación
    public string escenaFinal = "Creditos"; // ✅ Escena final

    private bool bossActivado = false;

    private void Start()
    {
        bossSprite.SetActive(false); // ✅ Mantener el Boss desactivado al inicio
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !bossActivado)
        {
            bossActivado = true; // ✅ Evitar múltiples activaciones
            bossSprite.SetActive(true); // ✅ Activar el Boss

            int objetosRecogidos = Inventario.instance.itemsRecogidos.Count; // ✅ Obtener número de objetos del inventario

            // ✅ Elegir la fase de animación según los objetos recogidos
            if (objetosRecogidos >= 3)
            {
                bossAnimator.Play("Fase3");
                Debug.Log("Reproduciendo Fase3");
            }
            else if (objetosRecogidos == 2)
            {
                bossAnimator.Play("Fase2");
                Debug.Log("Reproduciendo Fase2");
            }
            else
            {
                bossAnimator.Play("Fase3"); // ✅ Fase por defecto
                Debug.Log("Reproduciendo Fase3");
            }

            StartCoroutine(EsperarAnimacionYCambiar());
        }
    }

    IEnumerator EsperarAnimacionYCambiar()
    {
        yield return new WaitUntil(() => bossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        Debug.Log("Animación finalizada. Activando objeto oculto...");
        objetoActivar.SetActive(true); // ✅ Mostrar objeto oculto
        ActivaMuerte.muerte = false;

        yield return new WaitForSeconds(6f);
        Debug.Log("Cambiando a escena: " + escenaFinal);
        UnityEngine.SceneManagement.SceneManager.LoadScene(escenaFinal);
    }
}