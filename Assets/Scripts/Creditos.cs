using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    public AudioSource audioSource; // ✅ Referencia al AudioSource en la escena de créditos
    private static string escenaAnterior; // ✅ Variable para almacenar la escena previa

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Creditos" && escenaAnterior == "Layout")
        {
            audioSource.Play(); // ✅ Reproducir audio solo si la escena anterior fue "Layout"
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        escenaAnterior = scene.name; // ✅ Almacenar el nombre de la escena cuando cambie
    }
}