using UnityEngine;
using System.Collections.Generic;

public class GestorEstado : MonoBehaviour
{
    public static GestorEstado instancia;

    public int nivelActual = 1; // Nivel en el que estabas antes de cambiar
    public bool muerte = false; // Estado de muerte del jugador
    public List<string> inventario = new List<string>(); // Inventario persistente
    public Dictionary<string, bool> objetosRecogidos = new Dictionary<string, bool>(); // Estado de objetos recogidos
    public bool regresoDeBarco = false; // Indica si el jugador vuelve de Barco
    public bool aletaActiva = false; // Guarda si la aleta ha sido activada
    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Permitir que el estado persista
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados
        }
    }

    public void AñadirItem(string nombreItem)
    {
        if (!inventario.Contains(nombreItem))
        {
            inventario.Add(nombreItem);
            objetosRecogidos[nombreItem] = true; // Marca el objeto como recogido
        }
    }

    public bool ObjetoFueRecogido(string nombreItem)
    {
        return objetosRecogidos.ContainsKey(nombreItem) && objetosRecogidos[nombreItem];
    }
    public void ActivarAleta()
    {
        aletaActiva = true;
    }

    public bool ObtenerEstadoAleta()
    {
        return aletaActiva;
    }
}