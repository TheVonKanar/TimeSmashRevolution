using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

[RequireComponent(typeof(NetworkView))]
public class NetworkManager : MonoBehaviour {

    private readonly int port = 80;
    private readonly int hellocode = 0xb23f;
    private readonly int portgame = 443;

    private UdpClient udp;
    private bool find = false;
    private IPEndPoint e;

    private void Awake()
    {
        udp = new UdpClient();
        ReinitConnection();
        Application.runInBackground = true;
    }

    private void ReinitConnection()
    {
        Debug.Log("Reinit the udp connection");
        udp.Client.ReceiveTimeout = -1;
        udp.BeginReceive(ReceiveUDP, null);
        StartCoroutine("_coReinitConnection");
    }

    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

    private IEnumerator _coReinitConnection()
    {
        find = false;
        while (enabled)
        {
            var ip = LocalIPAddress().Split('.');
            byte[] bytes = BitConverter.GetBytes(hellocode);
            for (int i = 0; i < 254; i++)
            {
                udp.Send(bytes, bytes.Length, ip[0] + '.' + ip[1] + '.' + ip[2] + '.' + i, port);
            }
            yield return new WaitForSeconds(1.0f);
            if (find)
            {
                Network.Connect(e.Address.ToString(), portgame);
                yield break;
            }
            udp.BeginReceive(ReceiveUDP, null);
        }
    }

    private void ReceiveUDP(IAsyncResult ar)
    {
        e = new IPEndPoint(IPAddress.Any, port);
        byte[] receiveBytes = udp.EndReceive(ar, ref e);
        int code = BitConverter.ToInt32(receiveBytes, 0);
        Debug.Log("RECEIVED SOMETHING : " + code + " " + (hellocode == code) + " from " + e.Address);
        Debug.Log(e.Address.ToString());
        find = true;
    }

    private void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
        ReinitConnection();
    }

    private void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("Disconnected from server: " + info);
        ReinitConnection();
        GetComponent<Controller>().CancelInfoUpdate();
    }

    private void OnConnectedToServer()
    {
        Debug.Log("Connected to server");
        StopCoroutine("_coReinitConnection");
        GetComponent<Controller>().StartInfoUpdate();
    }

    private void OnDisable()
    {
        udp = null;
    }
}
