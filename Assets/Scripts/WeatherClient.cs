using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using System.Threading.Tasks;
using UnityEngine.UI;
using AsyncIO;
using System;

public class WeatherClient : MonoBehaviour
{
    private RequestSocket clientSocket;
    public TMPro.TextMeshProUGUI predictionText;
    async void Start()
    {
        ForceDotNet.Force();

        // Initialize client socket
        clientSocket = new RequestSocket();
        clientSocket.Connect("tcp://localhost:5555");

        await StartReceivingPredictions();
    }

    void OnDestroy()
    {
        clientSocket.Close();
        NetMQConfig.Cleanup();
    }

    async Task StartReceivingPredictions()
    {
        while (true)
        {
            // Send a request to the server
            clientSocket.SendFrame("predict");

            // Receive the prediction data from the server asynchronously
            string message = await Task.Run(() => clientSocket.ReceiveFrameString());
            Data data = JsonUtility.FromJson<Data>(message);

            // Update UI with the received prediction data
            UpdatePredictionUI(data);

            await Task.Delay(5000);
        }
    }

    void UpdatePredictionUI(Data data)
    {
        int truncatedUVIndex = Mathf.FloorToInt(data.uv_index_encoded);
        string uvIndex;

        switch (truncatedUVIndex)
        {
            case 0:
                uvIndex = "Low";
                break;
            case 1:
                uvIndex = "Moderate";
                break;
            case 2:
                uvIndex = "High";
                break;
            case 3:
                uvIndex = "Very High";
                break;
            default:
                uvIndex = "Unknown";
                break;
        }

        string TempUnit = "°C";
        string pressureUnit = "Pa";
        DateTime today = DateTime.Today;
        predictionText.text = $"Date:" + today.ToString("yyyy-MM-dd") + "\n\n" +
                              $"Min Temp: {data.min_temp:F2}{TempUnit}\n" +
                              $"Max Temp: {data.max_temp:F2}{TempUnit}\n" +
                              $"Pressure: {data.pressure:F2}{pressureUnit}\n" +
                              $"UV Index: {uvIndex}\n" +
                              $"Min GTS Temp: {data.min_gts_temp:F2}{TempUnit}\n" +
                              $"Max GTS Temp: {data.max_gts_temp:F2}{TempUnit}";
    }
}

[System.Serializable]
public class Data
{
    public float min_temp;
    public float max_temp;
    public float pressure;
    public float uv_index_encoded;
    public float min_gts_temp;
    public float max_gts_temp;
}
