using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorResiduo : MonoBehaviour
{
    public List<GameObject> residuos; // Lista de residuos disponibles
    private List<GameObject> residuosDisponibles; // Lista de residuos que aún no han aparecido
    private GameObject residuoActual; // Residuo actual mostrado
    private bool mostrandoResiduo = false; // Bandera para evitar duplicados
    private void Start()
    {
        ResetResiduos(); // Inicializar la lista de disponibles
    }

    public void MostrarResiduoAleatorio()
    {
        if (mostrandoResiduo) return; // Evita que se llame mientras ya se está mostrando un residuo

        mostrandoResiduo = true; // Activa la bandera

        // if (residuosDisponibles.Count == 0)
        // {
        //     Debug.LogWarning("Todos los residuos han sido mostrados.");
        //     return;
        // }

    //     // Selecciona un residuo aleatorio de la lista
    //     int indiceAleatorio = Random.Range(0, residuosDisponibles.Count);
    //     residuoActual = residuosDisponibles[indiceAleatorio];

    //     // Activa el residuo seleccionado y desactiva los demás
    //     foreach (GameObject residuo in residuos)
    //     {
    //         residuo.SetActive(false);
    //     }

    //     residuoActual.SetActive(true);

    //     // Remueve el residuo seleccionado de los disponibles
    //     residuosDisponibles.RemoveAt(indiceAleatorio);

    //     // Inicia la animación del residuo
    //     residuoActual.GetComponent<AnimarResiduo>().IniciarMovimiento();
        if (residuosDisponibles.Count > 0)
        {
            int indiceAleatorio = Random.Range(0, residuosDisponibles.Count);
            residuoActual = residuosDisponibles[indiceAleatorio];

            foreach (GameObject residuo in residuos)
            {
                residuo.SetActive(false);
            }

            residuoActual.SetActive(true);
            residuoActual.GetComponent<AnimarResiduo>().IniciarMovimiento();
            residuosDisponibles.Remove(residuoActual); // Elimina de la lista de disponibles
        }
        else
        {
            Debug.LogWarning("No hay residuos disponibles para mostrar.");
        }

        mostrandoResiduo = false; // Libera la bandera
    }
    public GameObject ObtenerResiduoActual()
    {
        return residuoActual;
    }
    public void ResetResiduos()
    {
        // Reinicia la lista de disponibles con todos los residuos
        residuosDisponibles = new List<GameObject>(residuos);

        foreach (var residuo in residuos)
        {
            residuo.SetActive(false); // Asegúrate de desactivar todos los residuos
        }

        // // Mostrar el primer residuo
        // MostrarResiduoAleatorio();
        // Debug.Log("Residuos reiniciados.");
        Debug.Log("Residuos reiniciados.");
    }
    public bool EstaListaVacia()
    {
        return residuosDisponibles.Count == 0;
    }



}