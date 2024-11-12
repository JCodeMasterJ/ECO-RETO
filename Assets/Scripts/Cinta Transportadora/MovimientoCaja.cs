using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCaja : MonoBehaviour
{
    public Transform posicionObjetivo; // Posición final en la cinta
    public float velocidad = 2f; // Velocidad de movimiento
    private bool moviendo = false; // Control para iniciar el movimiento
    public bool enPosicion = false; // Indica si la caja llegó a su posición

    private void Start()
    {
        StartCoroutine(IniciarMovimiento());
    }

    private void Update()
    {
        if (moviendo)
        {
            // Mueve la caja hacia su posición objetivo de forma suave
            transform.position = Vector3.MoveTowards(transform.position, posicionObjetivo.position, velocidad * Time.deltaTime);

            // Verifica si está lo suficientemente cerca de la posición final
            if (Vector3.Distance(transform.position, posicionObjetivo.position) < 0.1f)
            {
                enPosicion = true;
            }
        }
    }

    private IEnumerator IniciarMovimiento()
    {
        // Espera un pequeño tiempo antes de empezar el movimiento para dar un efecto más realista
        yield return new WaitForSeconds(0.5f);
        moviendo = true;
    }
}