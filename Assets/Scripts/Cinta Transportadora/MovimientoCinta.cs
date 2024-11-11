using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCinta : MonoBehaviour
{
    public float velocidad = 2f;
    public Transform cinta1;
    public Transform cinta2;
    private float anchoCinta;
    private bool enMovimiento = true; // Control para activar o desactivar el movimiento

    private void Start()
    {
        // Calcula el ancho de una cinta en base a su tamaño local
        anchoCinta = cinta1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        if (enMovimiento)
        {
            // Mueve ambas cintas hacia la derecha
            cinta1.Translate(Vector3.right * velocidad * Time.deltaTime);
            cinta2.Translate(Vector3.right * velocidad * Time.deltaTime);

            // Reposiciona la cinta que salió de la vista hacia la izquierda de la otra cinta
            if (cinta1.position.x >= anchoCinta)
            {
                cinta1.position = new Vector3(cinta2.position.x - anchoCinta, cinta1.position.y, cinta1.position.z);
            }
            else if (cinta2.position.x >= anchoCinta)
            {
                cinta2.position = new Vector3(cinta1.position.x - anchoCinta, cinta2.position.y, cinta2.position.z);
            }
        }
    }

    // Método para detener el movimiento de la cinta transportadora
    public void DetenerMovimiento()
    {
        enMovimiento = false;
    }

    // Método para reanudar el movimiento de la cinta transportadora (si lo necesitas después)
    public void ReanudarMovimiento()
    {
        enMovimiento = true;
    }
}
