using System;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using System.Text;

// using Debug = System.Diagnostics.Debug;


public class SocketIO : Singleton<SocketIO>
{
    public SocketIOUnity socket;
    public string Uri;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: check the Uri if Valid.
        var uri = new Uri(Uri);
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        ///// reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };
        socket.OnPing += (sender, e) =>
        {
            Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        // socket.OnReconnectAttempt += (sender, e) =>
        // {
        //     Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        // };


        Debug.Log("Connecting...");
        socket.Connect();



        /*listen for server events
        socket.On("connect", (fn) =>
         parse json response */

        socket.OnUnityThread(SocketEventDef.MOVE, (response) =>
        {
            Debug.Log("response: " + response);
            string data = response.GetValue().ToString(); ;
            RobotMovement resultJson = RobotMovement.FromJSON(data);
            RobotMovementSocketIO.instance.ControlRobotMovement(resultJson.direction, resultJson.step);
        });
    }


    void Update()
    {
        CommandLiveCamera();
    }

    void CommandLiveCamera()
    {

        // if (LiveCamera.instance.imageData != null)
        // {
        //     string encodedText = LiveCamera.instance.imageData;

        // string data = LiveCamera.instance.imageData;
        // if (LiveCamera.instance.bytesToSend != null)
        // {
        socket.Emit(SocketEventDef.LIVE_CAMERA, LiveCamera.instance.imageData);
        // LiveCamera.instance.imageDataReady = false;
        // }
        // }


    }

}