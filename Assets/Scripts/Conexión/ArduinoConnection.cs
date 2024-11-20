using System.Collections.Concurrent; // Necesario para ConcurrentQueue
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Threading;

public class ArduinoConnection : MonoBehaviour
{
    public string ipAddress = "192.168.1.18"; // IP del servidor
    public int port = 80;

    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    private StreamReader reader;
    private volatile bool keepReading = true;

    // Referencia al controlador de juego
    public TrashController trashController;

    // Cola para pasar señales al hilo principal
    private ConcurrentQueue<string> signalQueue = new ConcurrentQueue<string>();

    void Start()
    {
        ConnectToTcpServer();
    }

    private void ConnectToTcpServer()
    {
        try
        {
            socketConnection = new TcpClient(ipAddress, port);
            clientReceiveThread = new Thread(ListenForData);
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
                    //Debug.Log($"Mensaje recibido del servidor: {serverMessage}");

                    if (!string.IsNullOrEmpty(serverMessage))
                    {
                        // Encola la señal para procesarla en el hilo principal
                        signalQueue.Enqueue(serverMessage);
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

    private void Update()
    {
        // Procesar señales en el hilo principal
        while (signalQueue.TryDequeue(out string signal))
        {
            if (trashController != null)
            {
                trashController.ProcesarSenal(signal);
            }
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
