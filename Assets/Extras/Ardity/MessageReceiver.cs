using UnityEngine;

public class MessageReceiver : MonoBehaviour
{
    // Este método será llamado por el SerialController cuando llegue un mensaje
    void OnMessageArrived(string message)
    {
        // Mostrar el mensaje en la consola
        Debug.Log("Mensaje recibido: " + message);

        // Verificar si el mensaje es "verde"
        if (message == "verde")
        {
            Debug.Log("El mensaje recibido es 'verde'");
        }
    }

    // Este método se llama cuando hay un evento de conexión
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Conectado al dispositivo serial");
        else
            Debug.Log("Desconectado del dispositivo serial");
    }
}
