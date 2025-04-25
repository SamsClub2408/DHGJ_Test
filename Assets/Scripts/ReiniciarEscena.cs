using UnityEngine;
using UnityEngine.SceneManagement; // Libreria para cargar escenas

public class ReiniciarEscena : MonoBehaviour
{
    public string nombreEscena; // Nombre de la escena a reiniciar
    public void Reiniciar()
    {
        // Reiniciar la escena actual
        SceneManager.LoadScene(nombreEscena);
    }
}
