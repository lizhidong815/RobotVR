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
	Dictionary<TcpClient, string> conns = new Dictionary<TcpClient, string>();
	Queue<string> pendingconns = new Queue<string>();

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
			if (pendingconns.Count > 0) {
				conns.Add (client, pendingconns.Dequeue ());
			} else {
				string newID = simManager.newID ();
				conns.Add (client, newID);
				simManager.robots.Add (newID, null);
			}
        }
        foreach (KeyValuePair<TcpClient,string> conn in conns) {
            NetworkStream stream = conn.Key.GetStream();
            if (stream.DataAvailable) {
                int bytesRead = stream.Read(recvBuf, 0, recvBuf.Length);
                int i = BitConverter.ToInt32(recvBuf, 0);
                Debug.Log("Bytes: " + bytesRead);
                Debug.Log(i);
				Interpreter.Process (simManager.GetRobot (conn.Value), "some message");
            }
        }

    }
    public void SendToAll(byte[] msg) {
		foreach (KeyValuePair<TcpClient,string> conn in conns) {
            NetworkStream stream = conn.Key.GetStream();
            stream.Write(msg, 0, msg.Length);
        }
    }
}
