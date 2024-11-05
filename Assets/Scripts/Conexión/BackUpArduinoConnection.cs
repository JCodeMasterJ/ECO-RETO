// using UnityEngine;
// using System.Net.Sockets;
// using System.IO;
// using System.Threading;

// public class ArduinoConnection : MonoBehaviour
// {
//     public string ipAddress = "10.11.221.31"; // Reemplaza con la IP del ESP32
//     public int port = 80; // Puerto del ESP32

//     private TcpClient socketConnection; // Conexión TCP al servidor (ESP32)
//     private Thread clientReceiveThread; // Hilo para recibir los datos
//     private StreamReader reader; // Lector para el stream de datos
//     private volatile bool keepReading = true; // Flag para controlar la lectura

//     // Estados para basuras
//     private bool verdeActivada = false; // Si la basura verde está activada
//     private bool blancoActivada = false; // Si la basura blanca está activada
//     private bool negroActivada = false; // Si la basura negra está activada

//     void Start()
//     {
//         ConnectToTcpServer(); // Inicia la conexión TCP
//     }

//     private void ConnectToTcpServer()
//     {
//         try
//         {
//             socketConnection = new TcpClient(ipAddress, port); // Establece la conexión TCP con el ESP32
//             clientReceiveThread = new Thread(new ThreadStart(ListenForData)); // Inicia un hilo para recibir datos
//             clientReceiveThread.IsBackground = true; // El hilo se ejecuta en segundo plano
//             clientReceiveThread.Start();
//             Debug.Log("Conectado al servidor.");
//         }
//         catch (System.Exception e)
//         {
//             Debug.Log("Error en la conexión: " + e); // Imprime cualquier error de conexión
//         }
//     }

//     private void ListenForData()
//     {
//         try
//         {
//             NetworkStream networkStream = socketConnection.GetStream(); // Obtiene el flujo de datos desde la conexión TCP
//             reader = new StreamReader(networkStream); // Inicializa el lector de datos
//             Debug.Log("Escuchando datos...");

//             while (keepReading) // Mientras el flag esté en true
//             {
//                 if (networkStream.DataAvailable) // Verifica si hay datos disponibles para leer
//                 {
//                     string serverMessage = reader.ReadLine(); // Lee la línea enviada por el servidor (ESP32)

//                     if (serverMessage != null)
//                     {
//                         Debug.Log("Mensaje recibido: " + serverMessage);

//                         // Condicional para activar la basura verde
//                         if (serverMessage == "verde")
//                         {
//                             if (!verdeActivada) // Solo si la basura verde no estaba activada
//                             {
//                                 verdeActivada = true; // Cambiamos el estado a activada
//                                 Debug.Log("Basura verde activada.");
//                             }
//                         }
//                         // Condicional para desactivar la basura verde
//                         else if (serverMessage == "fuera" && verdeActivada)
//                         {
//                             verdeActivada = false; // Cambiamos el estado a desactivada
//                             Debug.Log("Basura verde desactivada.");
//                         }

//                         // Condicional para activar la basura blanca
//                         else if (serverMessage == "blanco")
//                         {
//                             if (!blancoActivada) // Solo si la basura blanca no estaba activada
//                             {
//                                 blancoActivada = true; // Cambiamos el estado a activada
//                                 Debug.Log("Basura blanca activada.");
//                             }
//                         }
//                         // Condicional para desactivar la basura blanca
//                         else if (serverMessage == "fuera" && blancoActivada)
//                         {
//                             blancoActivada = false; // Cambiamos el estado a desactivada
//                             Debug.Log("Basura blanca desactivada.");
//                         }

//                         // Condicional para activar la basura negra
//                         else if (serverMessage == "negro")
//                         {
//                             if (!negroActivada) // Solo si la basura negra no estaba activada
//                             {
//                                 negroActivada = true; // Cambiamos el estado a activada
//                                 Debug.Log("Basura negra activada.");
//                             }
//                         }
//                         // Condicional para desactivar la basura negra
//                         else if (serverMessage == "fuera" && negroActivada)
//                         {
//                             negroActivada = false; // Cambiamos el estado a desactivada
//                             Debug.Log("Basura negra desactivada.");
//                         }
//                     }
//                     else
//                     {
//                         Debug.Log("No se recibió ningún mensaje.");
//                     }
//                 }
//                 else
//                 {
//                     Thread.Sleep(100); // Esperar un poco antes de volver a comprobar
//                 }
//             }
//         }
//         catch (SocketException socketException)
//         {
//             Debug.Log("Socket exception: " + socketException);
//         }
//         catch (IOException ioException)
//         {
//             Debug.Log("IO exception: " + ioException);
//         }
//         catch (System.Exception ex)
//         {
//             Debug.Log("Error: " + ex);
//         }
//     }

//     private void OnApplicationQuit()
//     {
//         keepReading = false; // Detener la lectura
//         if (clientReceiveThread != null)
//         {
//             clientReceiveThread.Join(); // Esperar a que el hilo termine
//         }
//         if (socketConnection != null)
//         {
//             socketConnection.Close(); // Cerrar la conexión
//         }
//     }
// }
