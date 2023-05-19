using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

#if WINDOWS_UWP
    using Windows.Foundation;
    using Windows.Networking.Sockets;
    using Windows.Security.Cryptography.Certificates;
    using Windows.Storage.Streams;
    using System.Threading.Tasks;
#else
using System.Text;
#endif

public class WebSocketManager : MonoBehaviour
{

#if WINDOWS_UWP
    private MessageWebSocket messageWebSocket;
#endif

    string NodeREDURL = "ws://192.168.1.34:1880/ws/test";

    void Start()
    {
#if WINDOWS_UWP
        // HoloLens2 WebSocket Connect
        OnConnect();
#endif

    }

    void Update()
    {

    }

    public void TouchStarted()
    {
        Debug.Log("TouchStarted");

#if WINDOWS_UWP
        try
        {
            Task.Run(async () => {
                await WebSock_SendMessage(messageWebSocket, "Touched!! 1");
            });
        }
        catch (Exception ex)
        {
            Debug.Log("error : " + ex.ToString());
        }
#endif


    }

    public void TouchCompleted(HandTrackingInputEventData eventData)
    {
        Debug.Log("TouchCompleted");
    }

#if WINDOWS_UWP
  
  
    private void OnConnect()
    {
  
        Debug.Log("OnConnect");
          
        messageWebSocket = new MessageWebSocket();
 
        messageWebSocket.Control.MessageType = SocketMessageType.Utf8;
        messageWebSocket.MessageReceived += WebSock_MessageReceived;
        messageWebSocket.Closed += WebSock_Closed;
  
        Uri serverUri = new Uri(NodeREDURL); // Connecting
  
        try
        {
            Task.Run(async () =>
            {
                await messageWebSocket.ConnectAsync(serverUri);
 
                Debug.Log("Connect to the server...." + serverUri.ToString());
                Debug.Log("ConnectAsync OK");
  
                await WebSock_SendMessage(messageWebSocket, "Connect Start");
            });
        }
        catch (Exception ex)
        {
            Debug.Log("error : " + ex.ToString());
        }
          
    }
      
    private async Task WebSock_SendMessage(MessageWebSocket webSock, string message)
    {
        DataWriter messageWriter = new DataWriter(webSock.OutputStream);
        messageWriter.WriteString(message);
        await messageWriter.StoreAsync();
        messageWriter.DetachStream();
 
    }
  
    private void WebSock_MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
    {
        DataReader messageReader = args.GetDataReader();
        messageReader.UnicodeEncoding = UnicodeEncoding.Utf8;
        string messageString = messageReader.ReadString(messageReader.UnconsumedBufferLength);
 
        Task.Run(async () =>
        {
  
            UnityEngine.WSA.Application.InvokeOnAppThread(() => {
            }, true);
  
            await Task.Delay(100);
        });
    }
  
    private void WebSock_Closed(IWebSocket sender, WebSocketClosedEventArgs args)
    {
        // WebSock_Closed
    }
  
#endif

}