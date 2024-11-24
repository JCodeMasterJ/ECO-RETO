using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrashController : MonoBehaviour
{
    public GameObject heartsGameObject; // Referencia al objeto que contiene la imagen de corazones
    public int maxLives = 3; // Número máximo de vidas
    public float heartWidth = 16f; // Ancho de un corazón individual (en píxeles)
    public ScoreManager scoreManager; // Referencia al ScoreManager

    private int lives;
    private AnimarResiduo residuoActual;
    private float tiempoInactivo = 0f;
    private float limiteInactividad = 10f;
    private RectTransform heartsRectTransform; // Se obtiene dinámicamente
    private bool temporizadorActivo = false; // Temporizador del perder vida por inactividad. Controla si el temporizador está activo
    //private bool vidaPerdidaPorResiduo = false; // Indica si ya se perdió una vida por el residuo actual
    //private bool cooldownActivo = false; // Control del cooldown



    private void Start()
    {
        // Obtener el RectTransform del GameObject
        if (heartsGameObject != null)
        {
            heartsRectTransform = heartsGameObject.GetComponent<RectTransform>();
        }

        lives = maxLives;
        UpdateHUD();
    }
    public void IniciarTemporizador()
    {
        temporizadorActivo = true;
        tiempoInactivo = 0f; // Reinicia el temporizador
        Debug.Log("Temporizador iniciado.");
    }

    private void Update()
    {
        //if (!temporizadorActivo || cooldownActivo) return;
        if (!temporizadorActivo) return;

        tiempoInactivo += Time.deltaTime;

        if (tiempoInactivo >= limiteInactividad)
        {
            PerderVidaPorInactividad();
            tiempoInactivo = 0f;
        }
        // if (!temporizadorActivo) return; // Solo cuenta si el temporizador está activo

        // tiempoInactivo += Time.deltaTime;

        // if (tiempoInactivo >= limiteInactividad)
        // {
        //     PerderVidaPorInactividad();
        //     tiempoInactivo = 0f; // Reinicia el contador

        //     // Si el jugador llega a 0 vidas, detenemos el temporizador
        //     if (lives <= 0)
        //     {
        //         temporizadorActivo = false;
        //     }
        // }
        
    }

    public void SetResiduoActual(AnimarResiduo residuo)
    {
        residuoActual = residuo;
        tiempoInactivo = 0f; // Reinicia el temporizador
        //vidaPerdidaPorResiduo = false; // Resetea el control de vida
        //cooldownActivo = false; // Reinicia cooldown al cambiar de residuo
        Debug.Log($"Residuo Actual Configurado: {residuo.nombreResiduo}, Basura Correcta: {residuo.basuraCorrecta}");
    }

    public void ProcesarSenal(string senal)
    {
        //Debug.Log($"Procesando señal: {senal}");
        // if (residuoActual == null)
        // {   
        //     //Debug.LogWarning("No hay residuo actual configurado.");
        //     return;
        // }
        //if (residuoActual == null || cooldownActivo) return;
        if (residuoActual == null) return;

        // Ignorar señales no válidas
        if (string.IsNullOrEmpty(senal) || senal == "Esperando...")
        {
            Debug.LogWarning("Señal ignorada: " + senal);
            return;
        }

        tiempoInactivo = 0f; // Reinicia el temporizador al recibir señal

        if (string.IsNullOrEmpty(residuoActual.basuraCorrecta))
        {   
            Debug.LogWarning("La propiedad 'basuraCorrecta' del residuo actual está vacía o no está configurada.");
            return;
        }

        if (senal.Trim().ToLower() == residuoActual.basuraCorrecta.Trim().ToLower())
        {
            Debug.Log($"¡Correcto! Basura: {senal}");
            scoreManager.AddPoint();
        }
        if (senal.Trim().ToLower() == "verde" && 
            (residuoActual.basuraCorrecta.Trim().ToLower() == "negra" || 
            residuoActual.basuraCorrecta.Trim().ToLower() == "blanca"))
        {       
            Debug.Log($"¡Incorrecto! Basura: {senal}");
            scoreManager.RemovePoint();
        }
        else {
            Debug.Log("Así no era");
        }

        
        
        //StartCooldown(); // Activa cooldown después de procesar cualquier señal
        UpdateHUD();
    }


    public void PerderVidaPorInactividad()
    {
        // if (vidaPerdidaPorResiduo)
        // {
        //     Debug.Log("Ya se perdió una vida por este residuo.");
        //     return;
        // }
        //if (vidaPerdidaPorResiduo) return;

        lives--;
        //vidaPerdidaPorResiduo = true;
        Debug.Log("Perdiste una vida por inactividad.");
        UpdateHUD();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    //  private void StartCooldown()
    // {
    //     cooldownActivo = true;
    //     Debug.Log("Cooldown iniciado.");
    //     Invoke(nameof(ResetCooldown), 8f); // Cooldown de 8 segundos
    // }

    // private void ResetCooldown()
    // {
    //     cooldownActivo = false;
    //     Debug.Log("Cooldown terminado.");
    // }
    
    private void UpdateHUD()
    {
        // Ajusta el tamaño visible de los corazones
        if (heartsRectTransform != null)
        {
            float visibleWidth = heartWidth * lives;
            heartsRectTransform.sizeDelta = new Vector2(visibleWidth, heartsRectTransform.sizeDelta.y);
        }
    }

    private void GameOver()
{
    Debug.Log("¡Game Over!");
    temporizadorActivo = false; // Detener el temporizador
    // Aquí puedes añadir lógica adicional, como mostrar un mensaje final o reiniciar el juego.
}

}
