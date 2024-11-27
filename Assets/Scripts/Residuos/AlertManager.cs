using System.Collections;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public GameObject avisoCorrecto; // Aviso para "Correcto"
    public GameObject avisoVerde;   // Aviso para "Fallaste, debía ser verde"
    public GameObject avisoBlanco;  // Aviso para "Fallaste, debía ser blanco"
    public GameObject avisoNegro;   // Aviso para "Fallaste, debía ser negro"
    public GameObject avisoInactividad;

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

        if (avisoSeleccionado != null)
        {
            // StartCoroutine(MostrarAvisoTemporal(avisoSeleccionado));
            StartCoroutine(MostrarAvisoConOcultarCentro(avisoSeleccionado));
        }
    }
    public void MostrarAvisoInactividad()
    {
        StartCoroutine(MostrarAvisoInactividadCoroutine());
    }

    private IEnumerator MostrarAvisoInactividadCoroutine()
    {
        // Muestra el aviso en pantalla
        // GameObject avisoInactividad = // referencia al aviso "Perdiste una vida por inactividad"
        avisoInactividad.SetActive(true);

        // Espera 3 segundos
        yield return new WaitForSeconds(3f);

        // Oculta el aviso
        avisoInactividad.SetActive(false);
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
        // Llamar a CambiarResiduo después de que pase el tiempo del aviso
        var trashController = FindObjectOfType<TrashController>();
        if (trashController != null)
        {
            trashController.CambiarResiduo();
        }
    }

}
