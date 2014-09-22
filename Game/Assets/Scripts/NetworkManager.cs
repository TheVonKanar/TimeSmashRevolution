using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

[RequireComponent(typeof(NetworkView))]
public class NetworkManager : MonoBehaviour {

    private readonly int port = 1234;
    private readonly int helloCode = 0xb23f;
    private readonly int gamePort = 1111;

    private UdpClient udp;

    private void Awake()
    {
        Network.InitializeServer(10, gamePort, false);

        if (udp == null)
            udp = new UdpClient(new IPEndPoint(IPAddress.Any, port));

        udp.BeginReceive(ReceiveUDP, null);

        Application.runInBackground = true;
    }

    private void ReceiveUDP(IAsyncResult ar)
    {
        var e = new IPEndPoint(IPAddress.Any, port);
        byte[] receiveBytes = udp.EndReceive(ar, ref e);
        int code = BitConverter.ToInt32(receiveBytes, 0);
        Debug.Log("RECEIVED SOMETHING : " + (helloCode == code) + " from " + e.Address);
        byte[] bytes = BitConverter.GetBytes(helloCode);
        udp.Send(bytes, bytes.Length, e);
        udp.BeginReceive(ReceiveUDP, null);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Player disconnected from " + player.ipAddress + ":" + player.port);
    }

    void OnDisable()
    {
        Network.Disconnect();
    }
}
