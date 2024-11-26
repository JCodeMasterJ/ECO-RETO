using System.Collections.Generic;
using UnityEngine;

public class ArduinoManager : MonoBehaviour
{
    [System.Serializable]
    public class ESPDevice
    {
        public string ipAddress; // IP del ESP32
        public TrashController trashController; // Referencia a un controlador de basura
    }

    public List<ESPDevice> espDevices; // Lista de dispositivos ESP32

    private List<ArduinoConnection> connections = new List<ArduinoConnection>();

    private void Start()
    {
        foreach (var device in espDevices)
        {
            // Crear dinámicamente una instancia de ArduinoConnection
            var connectionObject = new GameObject($"ArduinoConnection_{device.ipAddress}");
            var connection = connectionObject.AddComponent<ArduinoConnection>();

            connectionObject.transform.parent = this.transform; // Agrupar bajo ArduinoManager
            connection.trashController = device.trashController; // Asignar el controlador
            connectionObject.GetComponent<ArduinoConnection>().ipAddress = device.ipAddress;

            connections.Add(connection);
        }
    }
}
