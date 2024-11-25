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

    //CON MUCHA FE
    //private float tiempoEsperando = 0f; // Contador para el mensaje "Esperando..."
    //private bool recibiendoEsperando = false; // Bandera para saber si se está recibiendo "Esperando..."


    public AlertManager alertManager; // Para los avisos de retroalimenatción

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
        // if (residuoActual == null) return;

        // // Ignorar señales no válidas
        // if (string.IsNullOrEmpty(senal) || senal == "Esperando...")
        // {
        //     Debug.LogWarning("Señal ignorada: " + senal);
        //     return;
        // }

        // tiempoInactivo = 0f; // Reinicia el temporizador al recibir señal

        // if (string.IsNullOrEmpty(residuoActual.basuraCorrecta))
        // {   
        //     Debug.LogWarning("La propiedad 'basuraCorrecta' del residuo actual está vacía o no está configurada.");
        //     return;
        // }

        // if (senal.Trim().ToLower() == residuoActual.basuraCorrecta.Trim().ToLower())
        // {
        //     Debug.Log($"¡Correcto! Basura: {senal}");
        //     scoreManager.AddPoint();
        //     alertManager.MostrarAviso("correcto"); // Muestra el aviso "Correcto"
        // }
        // else
        // {
        //     Debug.Log($"¡Incorrecto! Basura: {senal}");
        //     scoreManager.RemovePoint();

        //     // Mostrar aviso según la basura correcta
        //     alertManager.MostrarAviso(residuoActual.basuraCorrecta.ToLower());
        // }

        // SEGUNDA OPCION NO SIRVIO

        // Ignorar señales no válidas
        // if (string.IsNullOrEmpty(senal))
        // {
        //     Debug.LogWarning("Señal vacía o nula ignorada.");
        //     return;
        // }

        // // Si se recibe "Esperando..."
        // if (senal.Trim().ToLower() == "Esperando...")
        // {
        //     if (!recibiendoEsperando) // Empieza a contar si es el primer "Esperando..."
        //     {
        //         Debug.Log("Inicia la espera");
        //         recibiendoEsperando = true;
        //         tiempoEsperando = 0f;
        //     }

        //     tiempoEsperando += Time.deltaTime;

        //     // Si alcanza los 10 segundos seguidos, se quita una vida
        //     if (tiempoEsperando >= 10f)
        //     {
        //         PerderVidaPorInactividad();
        //         tiempoEsperando = 0f;
        //         recibiendoEsperando = false; // Reinicia la bandera
        //     }

        //     return; // No procesamos más si seguimos recibiendo "Esperando..."
        // }

        // // Si llega una señal válida, reinicia el contador de "Esperando..."
        // recibiendoEsperando = false;
        // tiempoEsperando = 0f;

        // // Procesar las señales válidas
        // if (residuoActual == null)
        // {
        //     Debug.LogWarning("No hay residuo actual para evaluar.");
        //     return;
        // }

        // if (senal.Trim().ToLower() == residuoActual.basuraCorrecta.Trim().ToLower())
        // {
        //     Debug.Log($"¡Correcto! Basura: {senal}");
        //     scoreManager.AddPoint();
        //     alertManager.MostrarAviso("correcto");
        //     return;
        // }
        
        // Debug.Log($"¡Incorrecto! Basura: {senal}");
        // scoreManager.RemovePoint();
        // alertManager.MostrarAviso(residuoActual.basuraCorrecta.ToLower());
        if (residuoActual == null) return;

        // Acceso a la última señal procesada desde ArduinoConnection
        var arduinoConnection = FindObjectOfType<ArduinoConnection>();
        if (arduinoConnection != null && arduinoConnection.LastSignalProcessed == senal.Trim().ToLower())
        {
            Debug.Log("Señal ya procesada, ignorando: " + senal);
            return;
        }

        // Ignorar señales no válidas
        if (string.IsNullOrEmpty(senal) || senal.Trim().ToLower() == "esperando...")
        {
            Debug.LogWarning("Señal ignorada: " + senal);
            return;
        }

        tiempoInactivo = 0f; // Reinicia el temporizador al recibir señal válida

        if (string.IsNullOrEmpty(residuoActual.basuraCorrecta))
        {
            Debug.LogWarning("La propiedad 'basuraCorrecta' del residuo actual está vacía o no está configurada.");
            return;
        }

        if (senal.Trim().ToLower() == residuoActual.basuraCorrecta.Trim().ToLower())
        {
            Debug.Log($"¡Correcto! Basura: {senal}");
            scoreManager.AddPoint();
            alertManager.MostrarAviso("correcto");
        }
        else
        {
            Debug.Log($"¡Incorrecto! Basura: {senal}");
            scoreManager.RemovePoint();
            alertManager.MostrarAviso(residuoActual.basuraCorrecta.ToLower());
        }
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

        if (!temporizadorActivo) return; // Evitar múltiples pérdidas de vida

        lives--;
        //vidaPerdidaPorResiduo = true;
        Debug.Log("Perdiste una vida por inactividad.");
        UpdateHUD();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            CambiarResiduo(); // Cambia al siguiente residuo después de perder una vida
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
    private void CambiarResiduo()
    {
        var controladorResiduo = FindObjectOfType<ControladorResiduo>();
        if (controladorResiduo != null)
        {
            controladorResiduo.MostrarResiduoAleatorio();
        }
        else
        {
            Debug.LogWarning("ControladorResiduo no encontrado.");
        }
    }

    // Método para evitar procesar señales adicionales después de una
    // private void ResetSignalProcessing()
    // {
    //     lastSignalProcessed = ""; // Limpia para nuevas señales después de que se reinicie la lógica
    //     temporizadorActivo = false; // Detiene el temporizador mientras se prepara el siguiente residuo
    //     CambiarResiduo(); // Cambia al siguiente residuo automáticamente
    // }
    private void GameOver()
    {
        Debug.Log("¡Game Over!");
        temporizadorActivo = false; // Detener el temporizador
        // Aquí puedes añadir lógica adicional, como mostrar un mensaje final o reiniciar el juego.
    }

}
