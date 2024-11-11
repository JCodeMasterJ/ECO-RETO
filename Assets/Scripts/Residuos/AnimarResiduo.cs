using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI; // Si usarás un texto para el nombre del residuo
using TMPro;
public class AnimarResiduo : MonoBehaviour
{
    public Transform posicionCentro; // Referencia a la posición final en el centro de la pantalla
    public float duracionMovimiento = 1.5f; // Duración del movimiento y escalado
    public string nombreResiduo; // Nombre del residuo para mostrar
    public TextMeshProUGUI textoNombreResiduo; // Referencia al objeto Text para mostrar el nombre del residuo

    private Vector3 escalaInicial;
    private Vector3 escalaFinal = new Vector3(0.1f, 0.1f, 0.1f); // Escala deseada en el centro
    private Vector3 posicionInicial;

    private void Start()
    {
        escalaInicial = transform.localScale;
        posicionInicial = transform.localPosition;
        textoNombreResiduo.text = ""; // Inicia el texto vacío
    }

    public void IniciarMovimiento()
    {
        // Empieza el movimiento del residuo hacia el centro con un escalado
        StartCoroutine(MoverYEscalarResiduo());
    }

    private IEnumerator MoverYEscalarResiduo()
    {
        float tiempoTranscurrido = 0f;
        Vector3 posicionFinal = posicionCentro.localPosition;

        // Configura el nombre del residuo antes de iniciar el movimiento
        textoNombreResiduo.text = nombreResiduo;
        textoNombreResiduo.color = new Color(textoNombreResiduo.color.r, textoNombreResiduo.color.g, textoNombreResiduo.color.b, 0); // Hacerlo transparente al inicio

        while (tiempoTranscurrido < duracionMovimiento)
        {
            tiempoTranscurrido += Time.deltaTime;

            // Interpola la posición y la escala hacia el centro
            transform.localPosition = Vector3.Lerp(posicionInicial, posicionFinal, tiempoTranscurrido / duracionMovimiento);
            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, tiempoTranscurrido / duracionMovimiento);

            // Interpola la opacidad del texto para que aparezca suavemente
            textoNombreResiduo.color = new Color(textoNombreResiduo.color.r, textoNombreResiduo.color.g, textoNombreResiduo.color.b, tiempoTranscurrido / duracionMovimiento);

            yield return null;
        }

        // Asegura que la posición y escala sean las finales
        transform.localPosition = posicionFinal;
        transform.localScale = escalaFinal;
        textoNombreResiduo.color = new Color(textoNombreResiduo.color.r, textoNombreResiduo.color.g, textoNombreResiduo.color.b, 1); // Totalmente visible al final
    }
}
