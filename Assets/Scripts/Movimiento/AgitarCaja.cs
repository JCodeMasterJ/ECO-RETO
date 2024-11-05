using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgitarCaja : MonoBehaviour
{
    public float duracionShake = 1f; // Duración del efecto de agitar en segundos
    public float intensidadShake = 0.05f; // Intensidad del efecto de agitar
    public float esperaAntesShake = 0.5f; // Espera antes de empezar el shake
    public Sprite cajaAbierta; // Sprite de la caja abierta

    private SpriteRenderer spriteRenderer;
    private Vector3 posicionOriginal;
    private bool yaHaSacudido = false; // Variable para controlar si ya se ha agitado

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        posicionOriginal = transform.localPosition; // Guarda la posición original al inicio
    }

    // Método para iniciar el efecto de agitar y abrir la caja
    public void IniciarShake()
    {
        if (!yaHaSacudido) // Verifica si no ha sacudido aún
        {
            // Fija la posición original antes de empezar el shake
            posicionOriginal = transform.localPosition;
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        // Establece el estado a true para evitar futuras sacudidas
        yaHaSacudido = true;

        // Espera un pequeño tiempo antes de iniciar el shake
        yield return new WaitForSeconds(esperaAntesShake);

        float tiempoTranscurrido = 0f;

        // Inicia el bucle de shake por la duración especificada
        while (tiempoTranscurrido < duracionShake)
        {
            tiempoTranscurrido += Time.deltaTime;

            // Depuración: Imprime el valor de tiempoTranscurrido
            Debug.Log($"Shake: Tiempo transcurrido = {tiempoTranscurrido}");

            // Crea un desplazamiento aleatorio pero anclado a la posición original
            float desplazamientoX = Random.Range(-intensidadShake, intensidadShake);
            transform.localPosition = new Vector3(posicionOriginal.x + desplazamientoX, posicionOriginal.y, posicionOriginal.z);

            // Espera al siguiente frame
            yield return null;
        }

        // Restaurar la posición original al terminar el shake
        transform.localPosition = posicionOriginal;

        // Llama al método para abrir la caja después de finalizar el shake
        AbrirCaja();
    }

    // Cambia el sprite para abrir la caja y ajusta el tamaño
    private void AbrirCaja()
    {
        if (cajaAbierta != null)
        {
            spriteRenderer.sprite = cajaAbierta;

            // Asegura que la caja esté en su posición correcta
            transform.localPosition = posicionOriginal;

            Debug.Log("Caja Abierta");
        }
    }
}
