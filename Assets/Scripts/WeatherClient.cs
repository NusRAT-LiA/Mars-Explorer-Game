using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using System.Threading.Tasks;
using UnityEngine.UI;
using AsyncIO;

public class WeatherClient : MonoBehaviour
{
    private RequestSocket clientSocket;
    private bool requestSent = false; // Flag to track whether request has been sent
    public TMPro.TextMeshProUGUI predictionText; // Reference to your UI Text element

    async void Start()
    {
        ForceDotNet.Force();

        // Initialize client socket
        clientSocket = new RequestSocket();
        clientSocket.Connect("tcp://localhost:5555");

        // Start receiving predictions from the server
        await StartReceivingPredictions();
    }

    void OnDestroy()
    {
        // Clean up resources
        clientSocket.Close();
        NetMQConfig.Cleanup();
    }

    async Task StartReceivingPredictions()
    {
        while (true)
        {
            // Send a request to the server only if the request has not been sent yet
            if (!requestSent)
            {
                clientSocket.SendFrame("predict");
                requestSent = true; // Set flag to true indicating request has been sent
            }

            // Receive the prediction data from the server asynchronously
            string message = await Task.Run(() => clientSocket.ReceiveFrameString());
            Data data = JsonUtility.FromJson<Data>(message);

            // Update UI with the received prediction data
            UpdatePredictionUI(data);
        }
    }

    void UpdatePredictionUI(Data data)
    {
        // Update UI with the received prediction data
        predictionText.text = $"Min Temp: {data.min_temp}\nMax Temp: {data.max_temp}\nPressure: {data.pressure}";
    }
}

[System.Serializable]
public class Data
{
    public float min_temp;
    public float max_temp;
    public float pressure;
}
