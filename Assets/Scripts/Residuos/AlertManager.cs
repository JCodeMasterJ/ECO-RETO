using System.Collections;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public GameObject avisoCorrecto; // Aviso para "Correcto"
    public GameObject avisoVerde;   // Aviso para "Fallaste, debía ser verde"
    public GameObject avisoBlanco;  // Aviso para "Fallaste, debía ser blanco"
    public GameObject avisoNegro;   // Aviso para "Fallaste, debía ser negro"
    public ControladorResiduo controladorResiduo; // Controlador para acceder al residuo actual
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
        // if (tipo.ToLower() == "correcto"){
        //     avisoSeleccionado = avisoCorrecto;
        // }
        // else if (tipo.ToLower() == "verde"){
        //     avisoSeleccionado = avisoVerde;
        // }
        // else if (tipo.ToLower() == "blanco"){
        //     avisoSeleccionado = avisoBlanco;
        // }
        // else if (tipo.ToLower() == "negro"){
        //     avisoSeleccionado = avisoCorrecto;
        // }

        if (avisoSeleccionado != null)
        {
            // StartCoroutine(MostrarAvisoTemporal(avisoSeleccionado));
            StartCoroutine(MostrarAvisoConOcultarCentro(avisoSeleccionado));
        }
    }

    // private IEnumerator MostrarAvisoTemporal(GameObject aviso)
    // {
    //     aviso.SetActive(true);
    //     yield return new WaitForSeconds(duracionAviso);
    //     aviso.SetActive(false);
    // }
    private IEnumerator MostrarAvisoConOcultarCentro(GameObject aviso)
    {
        // Accede al residuo actual desde el controlador
        GameObject residuoActual = controladorResiduo.ObtenerResiduoActual();
        if (residuoActual != null)
        {
            residuoActual.SetActive(false); // Oculta el residuo
            var animarResiduo = residuoActual.GetComponent<AnimarResiduo>();
            if (animarResiduo != null && animarResiduo.textoNombreResiduo != null)
            {
                animarResiduo.textoNombreResiduo.gameObject.SetActive(false); // Oculta el texto permanentemente
            }
        }

        // Mostrar el aviso
        aviso.SetActive(true);

        // Esperar la duración del aviso
        yield return new WaitForSeconds(duracionAviso);

        // Ocultar el aviso
        aviso.SetActive(false);

        // No hacemos nada adicional con el residuo ni el texto, ya están desactivados.
    }

}
