using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCinta : MonoBehaviour
{
    public MovimientoCinta movimientoCinta; // Referencia al script de la cinta transportadora
    private MovimientoCaja[] cajas; // Referencia a todas las cajas
    public AgitarCaja cajaDerecha; // Referencia a la caja que debe abrirse
       private void Start()
    {
        // Encuentra todas las cajas en la escena y define el total
        cajas = FindObjectsOfType<MovimientoCaja>();
    }

    private void Update()
    {
        // Verifica si todas las cajas están en su posición
        bool todasEnPosicion = true;
        foreach (MovimientoCaja caja in cajas)
        {
            if (!caja.enPosicion)
            {
                todasEnPosicion = false;
                break;
            }
        }

        // Si todas las cajas están en posición, detener el movimiento de la cinta y de las cajas
        if (todasEnPosicion)
        {
            movimientoCinta.DetenerMovimiento();

            // Detiene todas las cajas deshabilitando sus scripts
            foreach (MovimientoCaja caja in cajas)
            {
                caja.enabled = false;
            }

            // Inicia el shake de la caja de la derecha
            cajaDerecha.IniciarShake();
        }
    }
}