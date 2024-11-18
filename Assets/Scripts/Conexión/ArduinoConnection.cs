using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Threading;

public class ArduinoConnection : MonoBehaviour
{
    //public string ipAddress = "10.11.221.31"; // IP de la Uni
    public string ipAddress = "192.168.1.18"; // IP de la casa. Ctrl C + Ctrl V Si no te acuerdas que es pruebalo
    public int port = 80;

    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    private StreamReader reader;
    private volatile bool keepReading = true;

    // Referencia al controlador de juego
    public TrashController gameController;

    void Start()
    {
        ConnectToTcpServer();
    }

    private void ConnectToTcpServer()
    {
        try
        {
            socketConnection = new TcpClient(ipAddress, port);
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
            Debug.Log("Conectado al servidor.");
        }
        catch (System.Exception e)
        {
            Debug.Log("Error en la conexión: " + e);
        }
    }

    private void ListenForData()
    {
        try
        {
            NetworkStream networkStream = socketConnection.GetStream();
            reader = new StreamReader(networkStream);
            Debug.Log("Escuchando datos...");

            while (keepReading)
            {
                if (networkStream.DataAvailable)
                {
                    string serverMessage = reader.ReadLine();

                    if (serverMessage != null)
                    {
                        Debug.Log("Mensaje recibido: " + serverMessage);

                        // Delegar acciones al controlador del juego
                        if (serverMessage == "verde")
                        {
                            gameController.ActivateGreenTrash();
                        }
                        else if (serverMessage == "fuera" && gameController.GreenTrashActivated)
                        {
                            gameController.DeactivateGreenTrash();
                        }
                        // Añadir más condicionales para otros tipos de basura
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error: " + ex);
        }
    }

    private void OnApplicationQuit()
    {
        keepReading = false;
        if (clientReceiveThread != null)
        {
            clientReceiveThread.Join();
        }
        if (socketConnection != null)
        {
            socketConnection.Close();
        }
    }
}
