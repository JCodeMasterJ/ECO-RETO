using System.Collections;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public GameObject avisoCorrecto; // Aviso para "Correcto"
    public GameObject avisoVerde;   // Aviso para "Fallaste, debía ser verde"
    public GameObject avisoBlanco;  // Aviso para "Fallaste, debía ser blanco"
    public GameObject avisoNegro;   // Aviso para "Fallaste, debía ser negro"
    private float duracionAviso = 5f; // Duración del aviso en segundos

    // Método público para mostrar un aviso
    public void MostrarAviso(string tipo)
    {
        GameObject avisoSeleccionado = null;

        switch (tipo.ToLower())
        {
            case "correcto":
                avisoSeleccionado = avisoCorrecto;
                break;
            case "verde":
                avisoSeleccionado = avisoVerde;
                break;
            case "blanco":
                avisoSeleccionado = avisoBlanco;
                break;
            case "negro":
                avisoSeleccionado = avisoNegro;
                break;
        }

        if (avisoSeleccionado != null)
        {
            StartCoroutine(MostrarAvisoTemporal(avisoSeleccionado));
        }
    }

    private IEnumerator MostrarAvisoTemporal(GameObject aviso)
    {
        aviso.SetActive(true);
        yield return new WaitForSeconds(duracionAviso);
        aviso.SetActive(false);
    }
}
