using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections;


public class UDPReceive : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;
    public int port = 5058;
    public bool startReceiving = true;
    public bool printToConsole = false;
    public string data;

    // Flag to indicate if the gesture is detected
    private bool gestureDetected = false;

    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void Update()
    {
        // Check if the gesture is detected
        if (gestureDetected)
        {
            // Perform scene loading operation on the main thread
            LoadSceneAsync();
            // Stop receiving data
            startReceiving = false;
        }
    }

    // receive thread
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (startReceiving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);

                IsGestureDetected(data);

                if (printToConsole)
                {
                    print(data);
                }
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void IsGestureDetected(string receivedData)
    {
        // Split the received data into individual arrays
        string[] arrays = receivedData.Split(new string[] { "][" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string array in arrays)
        {
            // Split each array into individual values
            string[] values = array.Trim('[', ']').Split(',');

            // Convert the string values to integers
            int[] points = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                points[i] = int.Parse(values[i]);
            }

            // Extract the coordinates of the ears and index fingers from the points array
            int rightEarX = points[8 * 3];
            int rightEarY = points[8 * 3 + 1];
            int leftEarX = points[7 * 3];
            int leftEarY = points[7 * 3 + 1];
            int rightIndexX = points[20 * 3];
            int rightIndexY = points[20 * 3 + 1];
            int leftIndexX = points[19 * 3];
            int leftIndexY = points[19 * 3 + 1];

            // Calculate the distances between index fingers and ears
            float distanceToRightEar = Vector2.Distance(new Vector2(rightEarX, rightEarY), new Vector2(rightIndexX, rightIndexY));
            float distanceToLeftEar = Vector2.Distance(new Vector2(leftEarX, leftEarY), new Vector2(leftIndexX, leftIndexY));

            Debug.Log("distanceToRightEar: "+distanceToRightEar);
            Debug.Log("distanceToLeftEar: "+distanceToLeftEar);

            // Define a threshold for considering the gesture detected
            float gestureThreshold = 100.0f;

            // Check if either index finger is close to its respective ear
            if (distanceToRightEar < gestureThreshold && distanceToLeftEar < gestureThreshold)
            {
                // Set the flag to indicate the gesture is detected
                gestureDetected = true;
                return; // Exit the loop
            }
        }
    }

    private void LoadSceneAsync()
    {
        // Load the new scene asynchronously
        SceneManager.LoadSceneAsync(4);
    }
}
