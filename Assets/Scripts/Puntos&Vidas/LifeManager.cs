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

    private int lives;
    private float tiempoInactivo = 0f; // Tiempo de inactividad
    private RectTransform heartsRectTransform; // Para actualizar el HUD
    private bool temporizadorActivo = false; // Temporizador para controlar la inactividad

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
        if (!temporizadorActivo) return;

        tiempoInactivo += Time.deltaTime;

        if (tiempoInactivo >= limiteInactividad)
        {
            PerderVida();
            tiempoInactivo = 0f; // Reinicia el temporizador
        }
    }

    public void IniciarTemporizador()
    {
        temporizadorActivo = true;
        tiempoInactivo = 0f;
        Debug.Log("Temporizador de inactividad iniciado.");
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

    private void PerderVida()
    {
        if (lives <= 0) return; // Evita perder más vidas si ya es Game Over

        lives--;
        Debug.Log($"Perdiste una vida. Vidas restantes: {lives}");
        UpdateHUD();

        // if (lives <= 0)
        // {
        //     GameOver();
        // }
        // else
        // {
        //     // Cambiar residuo si hay vidas restantes
        //     if (trashController != null)
        //     {
        //         trashController.CambiarResiduo();
        //     }
        // }

        // Mostrar aviso de pérdida de vida por inactividad y pausar
        StartCoroutine(PausaPorInactividad());
    }
    private IEnumerator PausaPorInactividad()
    {
        // Mostrar el aviso a través del AlertManager
        if (alertManager != null)
        {
            alertManager.MostrarAvisoInactividad();
        }

        // Pausar por 3 segundos
        yield return new WaitForSeconds(3f);

        // Continuar con el flujo
        if (lives > 0)
        {
            trashController.CambiarResiduo(); // Cambia al siguiente residuo si hay vidas restantes
        }
        else
        {
            GameOver(); // Termina el juego si ya no hay vidas
        }
    }
    private void GameOver()
    {
        Debug.Log("¡Game Over!");
        // Aquí puedes agregar lógica para detener el juego o mostrar pantalla final
    }

    private void UpdateHUD()
    {
        if (heartsRectTransform != null)
        {
            float visibleWidth = heartWidth * lives;
            heartsRectTransform.sizeDelta = new Vector2(visibleWidth, heartsRectTransform.sizeDelta.y);
        }
    }
}
