using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Libreria para cargar escenas

public class BossBehavior : MonoBehaviour
{
    public float fadeDuration = 4f;
    public Animator bossAnimator; // Asigna el animator del boss en el inspector

    private SpriteRenderer spriteRenderer;
    private float currentAlpha;
    private float targetAlpha;

    public static bool BossKilledYou = false; // Variable para activar el boss
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró un SpriteRenderer en este objeto");
            enabled = false;
            return;
        }

        // Forzar alpha inicial a 0
        currentAlpha = 0f;
        targetAlpha = FlashlightController.isFlashlightOn ? 0f : 1f;
        UpdateSpriteAlpha();

        //Si la opacidad alcanza su maximo
        if (currentAlpha >= 1)
        {
            Debug.Log("MUERTEEEEEE Sin hacer nada");
        }
    }

    void Update()
    {
        // Actualizar el alpha objetivo según el estado de la linterna
        targetAlpha = FlashlightController.isFlashlightOn ? 0f : 1f;

        // Interpolar suavemente hacia el alpha objetivo
        if (Mathf.Abs(currentAlpha - targetAlpha) > 0.001f)
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime / fadeDuration);
            UpdateSpriteAlpha();
        }

        //Si la opacidad alcanza su maximo
        if(currentAlpha >=1)
        {
            Debug.Log("MUERTEEEEEE");
            spriteRenderer.enabled = false; // Desactivar el sprite renderer
            BossKilledYou = true; // Cambiar el estado de la variable

            StartCoroutine(Screamer()); // Llama a la corrutina para activar el sprite

            StartCoroutine(NextScene()); // Llama a la corrutina para cambiar de escena
        }
    }

    void UpdateSpriteAlpha()
    {
        Color newColor = spriteRenderer.color;
        newColor.a = currentAlpha;
        spriteRenderer.color = newColor;
    }

    IEnumerator NextScene()
    {
        // Espera 2 segundos antes de activar el sprite
        yield return new WaitForSeconds(3f);

        // Cargar la escena de GameOver
        SceneManager.LoadScene("Creditos");
    }

    IEnumerator Screamer()
    {
        // Espera 2 segundos antes de activar el sprite
        yield return new WaitForSeconds(0.5f);

        bossAnimator.SetTrigger("BossMuerte"); // Activar la animación de muerte del boss
    }
}
