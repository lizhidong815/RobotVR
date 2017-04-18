using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System;

///<summary>
/// Server manager handles network traffic and socket communication
/// between the simulator and the client program.
/// </summary>

[assembly: InternalsVisibleTo("Interpreter")]
internal class Packet
{
    // 5 bytes header
    internal byte packetType;
    internal UInt32 dataSize;
    // payload
    internal byte[] data;
}

// Robot connection class contains reference to the socket connection,
// and the corresponding simulated robot
public class RobotConnection
{
    public TcpClient tcpClient;
    public Robot robot;

    public RobotConnection(TcpClient newClient)
    {
        tcpClient = newClient;
    }

    public void AddRobotToScene(Robot newRobot)
    {
        robot = newRobot;
    }
}

public class ServerManager : MonoBehaviour
{
    public static ServerManager instance = null;
    List<RobotConnection> conns = new List<RobotConnection>();

    // TESTING
    public Robot testBot;
    //

    TcpListener listener = null;
    int port = 8888;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");
    

    public Interpreter interpreter;

    byte[] recvBuf = new byte[1024];
    string data = null;

    // Terminate a connection, and remove form the connection list
    private void CloseConnection(RobotConnection conn)
    {
        conn.tcpClient.Close();
        if (!conns.Remove(conn))
            Debug.Log("Failed to remove connection");
    }

    // Check each open connection every 5 seconds
    private IEnumerator CheckConnections()
    {
        while (true)
        {
            foreach (RobotConnection conn in conns)
            {
                if (conn.tcpClient.Client.Poll(0, SelectMode.SelectRead))
                {
                    byte[] buf = new byte[1];
                    if (conn.tcpClient.Client.Receive(buf, SocketFlags.Peek) == 0)
                    {
                        Debug.Log("Connection lost");
                        CloseConnection(conn);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(5);
        }
    }

    // Accept a pending connection
    private void AcceptConnection()
    {
        TcpClient client = listener.AcceptTcpClient();
        RobotConnection newClient = new RobotConnection(client);
        newClient.AddRobotToScene(testBot);
        Debug.Log("Accepted a connection");
        conns.Add(newClient);
    }

    // Read a packet from a connection
    // DataAvailable flag must be true before calling
    private void ReadPacket(RobotConnection conn)
    {
        NetworkStream stream = conn.tcpClient.GetStream();

        // Read Header
        int bytesRead = stream.Read(recvBuf, 0, 5);
        uint dataSize = BitConverter.ToUInt32(recvBuf,1);
        if (BitConverter.IsLittleEndian)
            dataSize = RobotFunction.ReverseBytes(dataSize);
        int packetType = recvBuf[0];
        Debug.Log("Packet Type: " + recvBuf[0] + " Packet Size: " + (uint)dataSize);

        // Read Body
        if (dataSize > 0)
        {
            bytesRead = stream.Read(recvBuf, 0, (int)dataSize);
            if (packetType == 10)
            {
                interpreter.ReceiveCommand(recvBuf, conn);
            }
        }
    }

    internal void WritePacket(RobotConnection conn, Packet packet)
    {
        byte[] sendBuf = new byte[1024];
        UInt32 size = packet.dataSize;

        if (BitConverter.IsLittleEndian)
            size = RobotFunction.ReverseBytes(size);

        sendBuf[0] = Convert.ToByte(packet.packetType);
        BitConverter.GetBytes(size).CopyTo(sendBuf, 1);
        packet.data.CopyTo(sendBuf, 5);

        NetworkStream stream = conn.tcpClient.GetStream();
        stream.Write(sendBuf, 0, ((int)packet.dataSize) + 5);
    }

    internal void WriteRaw(RobotConnection conn, byte[] msg, int size)
    {
        NetworkStream stream = conn.tcpClient.GetStream();
        stream.Write(msg, 0, size);
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        interpreter = new Interpreter();
        listener = new TcpListener(localAddr, port);
        listener.Start();
        StartCoroutine(CheckConnections());
        Debug.Log("Server Started");
    }   


    // Use this for initialization	
    void Start ()
    {
        Debug.Log("START CALLED");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (listener.Pending())
        {
            AcceptConnection();
        }
        // Check each connection for a message
        foreach (RobotConnection conn in conns)
        {
            NetworkStream stream = conn.tcpClient.GetStream();
            if (stream.DataAvailable)
                ReadPacket(conn);
        }
    }
}
