using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorResiduo : MonoBehaviour
{
    public List<GameObject> residuos; // Lista de residuos disponibles
    private GameObject residuoActual; // Residuo actual mostrado

    public void MostrarResiduoAleatorio()
    {
        // Selecciona un residuo aleatorio de la lista
        int indiceAleatorio = Random.Range(0, residuos.Count);
        residuoActual = residuos[indiceAleatorio];

        // Activa el residuo seleccionado y desactiva los demás
        foreach (GameObject residuo in residuos)
        {
            residuo.SetActive(false);
        }

        residuoActual.SetActive(true);

        // Inicia la animación del residuo
        residuoActual.GetComponent<AnimarResiduo>().IniciarMovimiento();
    }
    public GameObject ObtenerResiduoActual()
    {
        return residuoActual;
    }
    public void ResetResiduos()
    {
        foreach (var residuo in residuos)
        {
            residuo.SetActive(false); // Asegúrate de desactivar todos los residuos
        }

        // Mostrar el primer residuo
        MostrarResiduoAleatorio();
        Debug.Log("Residuos reiniciados.");
    }


}