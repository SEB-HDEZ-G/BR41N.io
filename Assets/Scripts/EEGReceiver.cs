using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class EEGReceiver : MonoBehaviour
{
    UdpClient client;
    Thread receiveThread;
    int port = 5005;

    // Referencia al MonsterManager
    public MonsterManager monsterManager;

    void Start()
    {
        client = new UdpClient(port);
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        Debug.Log("🧠 EEGReceiver started on port " + port);
    }

    void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        while (true)
        {
            try
            {
                byte[] data = client.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data);
                Debug.Log($"📩 EEG Message: {message}");
                HandleEEGAction(message);
            }
            catch (SocketException ex)
            {
                Debug.Log("UDP socket closed: " + ex.Message);
                break;
            }
        }
    }

    void HandleEEGAction(string action)
    {
        switch (action)
        {
            case "EyesClosed":
                Debug.Log("💤 Ojos cerrados detectados");
                break;

            case "BlinkFast":
                Debug.Log("👁️ Parpadeo rápido detectado");
                break;

            case "ShakeHands":
                Debug.Log("👐 Movimiento de manos detectado");
                break;

            case "TurnHead":
                Debug.Log("🔁 Movimiento de cabeza detectado");
                break;

            case "Normal":
                Debug.Log("🧘 Estado normal detectado");
                break;

            default:
                Debug.LogWarning($"⚠️ Acción no reconocida: {action}");
                break;
        }

        // Comunicar al MonsterManager
        if (monsterManager != null)
        {
            monsterManager.HandleEEGAction(action);
        }
    }

    void OnApplicationQuit()
    {
        if (receiveThread != null) receiveThread.Abort();
        client.Close();
        Debug.Log("⛔ EEGReceiver cerrado");
    }
}
