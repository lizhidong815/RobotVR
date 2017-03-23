using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System;

public class ServerManager {

	public SimManager simManager;
    TcpListener listener = null;
    int port = 8888;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");
    List<TcpClient> conns = new List<TcpClient>();

    byte[] recvBuf = new byte[1024];

    // Use this for initialization
    public ServerManager () {
        listener = new TcpListener(localAddr, port);
        listener.Start();
        Debug.Log("Server Started");
	}
	
	// Update is called once per frame
	public void Update () {
        if (listener.Pending()) {
            TcpClient client = listener.AcceptTcpClient();
            Debug.Log("Accepted a connection");
            conns.Add(client);
        }
        foreach (TcpClient conn in conns) {
            NetworkStream stream = conn.GetStream();
            if (stream.DataAvailable) {
                int bytesRead = stream.Read(recvBuf, 0, recvBuf.Length);
                int i = BitConverter.ToInt32(recvBuf, 0);
                Debug.Log("Bytes: " + bytesRead);
                Debug.Log(i);
                if (i >= 0) {
                    //robotDriver.getRemoteControl(i, 0);
                } else {
                    //robotDriver.getRemoteControl(-i, -i);
                }
            }
        }

    }
    public void SendToAll(byte[] msg) {
        foreach (TcpClient conn in conns) {
            NetworkStream stream = conn.GetStream();
            stream.Write(msg, 0, msg.Length);
        }
    }
}
