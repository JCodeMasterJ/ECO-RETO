using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TrashController : MonoBehaviour
 {
    public ScoreManager scoreManager; // Referencia al ScoreManager
    public LifeManager lifeManager; // Para la gestión de vidas
    public AlertManager alertManager; // Para los avisos de retroalimenatción
    private AnimarResiduo residuoActual;
    public GameObject textoGamOver;
    public GameObject botonRestart; // Botón de Restart
    public AudioSource gameOverSound;
    
    // private void Start()
    // {
        
    // }

    // private void Update()
    // {

    // }

    public void SetResiduoActual(AnimarResiduo residuo)
    {
        residuoActual = residuo;
        Debug.Log($"Residuo Actual Configurado: {residuo.nombreResiduo}, Basura Correcta: {residuo.basuraCorrecta}");
        // Iniciar el temporizador para el primer residuo
        lifeManager.IniciarTemporizador();
    }

    public void ProcesarSenal(string senal)
    {
        
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

        // tiempoInactivo = 0f; // Reinicia el temporizador al recibir señal válida

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
            lifeManager.ReiniciarTemporizador(); // Reinicia el temporizador de inactividad
            
        }
        else
        {
            Debug.Log($"¡Incorrecto! Basura: {senal}");
            scoreManager.RemovePoint();
            alertManager.MostrarAviso(residuoActual.basuraCorrecta.ToLower());
            lifeManager.ReiniciarTemporizador(); // Reinicia el temporizador de inactividad
            
        }
        
    }

    public void CambiarResiduo()
    {
        // Obtén el controlador de residuos
        var controladorResiduo = FindObjectOfType<ControladorResiduo>();
        
        if (controladorResiduo != null)
        {
            // Accede al residuo actual
            GameObject residuoActual = controladorResiduo.ObtenerResiduoActual();
            
            if (residuoActual != null)
            {
                // Oculta el residuo actual
                residuoActual.SetActive(false);

                // Accede al componente AnimarResiduo
                var animarResiduo = residuoActual.GetComponent<AnimarResiduo>();
                if (animarResiduo != null && animarResiduo.textoNombreResiduo != null)
                {
                    // Oculta el texto del residuo
                    animarResiduo.textoNombreResiduo.gameObject.SetActive(false);
                }
            }

            // // Llama al método para mostrar un residuo aleatorio (nuevo residuo)
            // controladorResiduo.MostrarResiduoAleatorio();
            // Obtiene un nuevo residuo si hay disponibles
            if (!controladorResiduo.EstaListaVacia())
            {
                controladorResiduo.MostrarResiduoAleatorio();
                // Ahora que el nuevo residuo está activo, asegúrate de mostrar el texto
                GameObject nuevoResiduo = controladorResiduo.ObtenerResiduoActual();
                if (nuevoResiduo != null)
                {
                    var nuevoAnimarResiduo = nuevoResiduo.GetComponent<AnimarResiduo>();
                    if (nuevoAnimarResiduo != null && nuevoAnimarResiduo.textoNombreResiduo != null)
                    {
                        nuevoAnimarResiduo.textoNombreResiduo.gameObject.SetActive(true); // Activa el texto
                    }
                }
                // Inicia el temporizador para medir inactividad
                lifeManager.IniciarTemporizador();
            }
            else
            {
                Debug.LogWarning("No hay más residuos disponibles para mostrar.");
            }
            
            
        }
        else
        {
            Debug.LogWarning("ControladorResiduo no encontrado.");
        }
    }

    // public void GameOver()
    // {
    //     Debug.Log("¡Game Over!");
    //     textoGamOver.SetActive(true);
    //     gameOverSound.Play();
    //     // Aquí puedes añadir lógica adicional, como mostrar un mensaje final o reiniciar el juego.
    // }
    public void GameOver()
    {
        Debug.Log("¡Game Over!");

        // Desactiva todos los residuos
        var controladorResiduo = FindObjectOfType<ControladorResiduo>();
        if (controladorResiduo != null)
        {
            GameObject residuoActual = controladorResiduo.ObtenerResiduoActual();
            if (residuoActual != null)
            {
                residuoActual.SetActive(false); // Desactiva el residuo actual
                var animarResiduo = residuoActual.GetComponent<AnimarResiduo>();
                if (animarResiduo != null && animarResiduo.textoNombreResiduo != null)
                {
                    animarResiduo.textoNombreResiduo.gameObject.SetActive(false); // Oculta el texto del residuo
                }
            }
        }

        // Muestra el texto de Game Over
        textoGamOver.SetActive(true);
        botonRestart.SetActive(true); // Activar el botón de reinicio

        // Reproduce el sonido de Game Over si está disponible
        if (gameOverSound != null)
        {
            gameOverSound.Play();
        }

        // Opcional: Pausa el juego
        Time.timeScale = 0f; // Detiene el tiempo para pausar el juego
    }

    public void RestartGame()
    {
        Debug.Log("Reiniciando el juego...");

        // Reiniciar el score
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        // Reiniciar las vidas
        if (lifeManager != null)
        {
            lifeManager.RestartLives();
        }

        // Reiniciar residuos
        var controladorResiduo = FindObjectOfType<ControladorResiduo>();
        if (controladorResiduo != null)
        {
            controladorResiduo.ResetResiduos(); // Este método lo implementaremos en el controlador de residuos
        }

        // Ocultar el texto de Game Over
        if (textoGamOver != null)
        {
            textoGamOver.SetActive(false);
        }

        // Restablecer el tiempo si estaba pausado
        Time.timeScale = 1f;

        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Juego reiniciado correctamente.");
    }



}
