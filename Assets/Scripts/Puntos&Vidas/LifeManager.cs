using UnityEngine;
using System.Collections;


public class LifeManager : MonoBehaviour
{
    public int maxLives = 3; // Número máximo de vidas
    public GameObject heartsGameObject; // Referencia al objeto que contiene la imagen de corazones
    public float heartWidth = 16f; // Ancho de un corazón individual (en píxeles)
    public float limiteInactividad = 10f; // Límite de tiempo de inactividad para perder una vida
    public TrashController trashController; // Controlador para cambiar el residuo después de perder una vida
    public AlertManager alertManager;
    public AudioSource lifeLostSound;


    private int lives;
    private float tiempoInactivo = 0f; // Tiempo de inactividad
    private RectTransform heartsRectTransform; // Para actualizar el HUD
    private bool temporizadorActivo = false; // Temporizador para controlar la inactividad
    // private bool pausaPorInactividad = false;
    private bool juegoTerminado = false;
    // private bool enCambioDeResiduo = false; // Evita múltiples cambios simultáneos
    private bool pausaPorInactividad = false; // Indica si está en pausa por inactividad



    private void Start()
    {
        lives = maxLives;

        // Obtener el RectTransform del HUD de corazones
        if (heartsGameObject != null)
        {
            heartsRectTransform = heartsGameObject.GetComponent<RectTransform>();
        }

        UpdateHUD();
    }

    private void Update()
    {
        if (!temporizadorActivo || pausaPorInactividad || juegoTerminado) return;

        tiempoInactivo += Time.deltaTime;

        if (tiempoInactivo >= limiteInactividad)
        {
            PerderVida();
            tiempoInactivo = 0f; // Reinicia el temporizador
        }
    }

    public void IniciarTemporizador()
    {
        if(!juegoTerminado){
            temporizadorActivo = true;
            tiempoInactivo = 0f;
            Debug.Log("Temporizador de inactividad iniciado.");
        }
        
    }

    public void DetenerTemporizador()
    {
        temporizadorActivo = false;
        tiempoInactivo = 0f;
        Debug.Log("Temporizador de inactividad detenido.");
    }

    public void ReiniciarTemporizador()
    {
        tiempoInactivo = 0f;
    }

    // private void PerderVida()
    // {
    //     if (lives <= 0){
    //         trashController.GameOver();  // Llamar al GameOver desde TrashController
    //         return; // Evita perder más vidas si ya es Game Over
    //     }

    //     lives--;
    //     if (lifeLostSound != null)
    //     {
    //         lifeLostSound.Play();
    //     }
    //     Debug.Log($"Perdiste una vida. Vidas restantes: {lives}");
    //     UpdateHUD();

    //     // if (lives <= 0)
    //     // {
    //     //     GameOver();
    //     // }
    //     // else
    //     // {
    //     //     // Cambiar residuo si hay vidas restantes
    //     //     if (trashController != null)
    //     //     {
    //     //         trashController.CambiarResiduo();
    //     //     }
    //     // }

    //     // Mostrar aviso de pérdida de vida por inactividad y pausar
    //     StartCoroutine(PausaPorInactividad());
    // }
    

    private void PerderVida()
    {
        if (juegoTerminado) return; // Si el juego ha terminado, no hacer nada

        // var controladorResiduo = FindObjectOfType<ControladorResiduo>();
        // if (controladorResiduo != null && controladorResiduo.EstaListaVacia())
        // {
        //     juegoTerminado = true;
        //     Debug.Log("No quedan más residuos. Deteniendo lógica de juego.");
        //     return;
        // }

        lives--;
        UpdateHUD();

        if (lives <= 0)
        {
            trashController.GameOver(); 
            juegoTerminado = true; // Detiene toda lógica futura
            return;
        }

        
        if (lifeLostSound != null)
        {
            lifeLostSound.Play();
        }

        Debug.Log($"Perdiste una vida. Vidas restantes: {lives}");
        

        StartCoroutine(PausaPorInactividad());
    }

    public  IEnumerator PausaPorInactividad()
    {   
        pausaPorInactividad = true; // Activar pausa
        Debug.Log("Pausa por inactividad iniciada.");

        // pausaPorInactividad = true; // Activa la pausa
        // if (enCambioDeResiduo) yield break; // Detén si ya hay un cambio en progreso

        // enCambioDeResiduo = true; // Bloquea más cambios
        // NUEVO
        // Oculta el residuo actual
        // var controladorResiduo = FindObjectOfType<ControladorResiduo>();
        // if (controladorResiduo != null)
        // {
        //     GameObject residuoActual = controladorResiduo.ObtenerResiduoActual();
        //     if (residuoActual != null)
        //     {
        //         residuoActual.SetActive(false);
        //         var animarResiduo = residuoActual.GetComponent<AnimarResiduo>();
        //         if (animarResiduo != null && animarResiduo.textoNombreResiduo != null)
        //         {
        //             animarResiduo.textoNombreResiduo.gameObject.SetActive(false); // Oculta el texto del residuo actual
        //         }
        //     }
        // }
        // TERMINA LO NUEVO
        // Mostrar el aviso a través del AlertManager
        if (alertManager != null)
        {
            alertManager.MostrarAvisoInactividad();
        }

        // Pausar por 3 segundos
        yield return new WaitForSeconds(4f);

        pausaPorInactividad = false;

        // // Continuar con el flujo
        // if (lives > 0)
        // {
        //     trashController.CambiarResiduo(); // Cambia al siguiente residuo si hay vidas restantes
        // }
        // else
        // {
        //     trashController.GameOver(); // Termina el juego si ya no hay vidas
        // }
        // // pausaPorInactividad = false; // Desactiva la pausa
        // enCambioDeResiduo = false; // Desbloquea cambios
        if (!juegoTerminado)
        {
            trashController.CambiarResiduo();
        }else{
            trashController.GameOver();
        }
    }
    // private void GameOver()
    // {
    //     Debug.Log("¡Game Over!");
    //     // Aquí puedes agregar lógica para detener el juego o mostrar pantalla final
    // }
    public bool EstaEnPausa()
    {
        return pausaPorInactividad; // Devuelve el estado actual de la pausa
    }

    private void UpdateHUD()
    {
        if (heartsRectTransform != null)
        {
            float visibleWidth = heartWidth * lives;
            heartsRectTransform.sizeDelta = new Vector2(visibleWidth, heartsRectTransform.sizeDelta.y);
        }
    }

    public void RestartLives()
    {
        lives = maxLives;
        UpdateHUD();
        Debug.Log("Vidas reiniciadas.");
    }
    public void DetenerLogicaDeVidas()
    {
        juegoTerminado = true; // Detiene toda lógica de vidas
        temporizadorActivo = false;
        pausaPorInactividad = false;
        Debug.Log("Lógica de vidas detenida. No más pérdidas de vidas.");
    }


}
