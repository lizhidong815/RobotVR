using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Intepreter takes RoBIOS commands and parses them, calling the appropriate function
/// on the robot. Input commands are of the form xABCD, where x is an ASCII character,
///  A,B,C,D are 1 or 2 byte integers, depending on the command.
/// </summary>

public class Interpreter {

    public ServerManager serverManager;

    // Motor Drive Uncontrolled
    private void Command_m(byte[] recv, RobotConnection conn)
    {
        if (conn.robot is IMotors)
        {
            int[] inputs = new int[2] { recv[1] - 1, recv[2] };
            (conn.robot as IMotors).DriveMotor(inputs);
        }
        else
        {
            Debug.Log("Drive Command received for a non drivable robot");
        }
    }
    // Motor Drive Controlled
    private void Command_M(byte[] recv, RobotConnection conn)
    {
        if (conn.robot is IPIDUsable)
        {
            int[] inputs = new int[2] { recv[1] - 1, recv[2]};
            (conn.robot as IPIDUsable).DriveMotorControlled(inputs);
        }
        else
        {
            Debug.Log("Drive Command received for a non drivable robot");
        }
    }
    // Set Motor PID
    private void Command_d(byte[] recv, RobotConnection conn)
    {
        if (conn.robot is IPIDUsable)
        {
            int[] inputs = new int[4] { recv[1] - 1, recv[2], recv[3], recv[4] };
            (conn.robot as IPIDUsable).SetPID(inputs);
        }
        else
        {
            Debug.Log("Set PID Command received for a non drivable robot");
        }
    }
    // Set Servo position
    private void Command_s(byte[] recv, RobotConnection conn)
    {
        if(conn.robot is IServoSettable)
        {
            int[] inputs = new int[2] { recv[1] - 1, recv[2] };
            (conn.robot as IServoSettable).SetServo(inputs);
        }
        else
        {
            Debug.Log("Set Servo Command received for a non servo settable robot");
        }

    }
    // Get PSD Value
    private void Command_p(byte[] recv, RobotConnection conn)
    {
        if (conn.robot is IPSDSensors)
        {
            int inputs = recv[1] - 1;
            byte[] value = BitConverter.GetBytes((conn.robot as IPSDSensors).GetPSD(inputs));
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(value);
            }
            Packet packet = new Packet();
            packet.packetType = PacketType.SERVER_MESSAGE;
            packet.dataSize = 2;
            packet.data = new byte[2];
            value.CopyTo(packet.data, 0);
            Debug.Log("PSD Sensor Value: " + value);
            serverManager.WritePacket(conn, packet);
        }
        else
        {
            Debug.Log("Get PSD Command from a robot without PSDs");
        }
    }
    // Get Vehicle Pose
    private void Command_q(byte[] recv, RobotConnection conn)
    {
        if(conn.robot is IPosable)
        {
            Pose pose = (conn.robot as IPosable).GetPose();
            Debug.Log("x: " + pose.x + " y: " + pose.y + " phi: " + pose.phi);
        }
        else
        {
            Debug.Log("Requested pose from a non posable robot");
        }
    }
    // Get Pose
    private void Command_Q(byte[] recv, RobotConnection conn)
    {
        if (conn.robot is IPosable)
        {
            int[] inputs = new int[3]{IPAddress.NetworkToHostOrder(BitConverter.ToInt16(recv, 1)),
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(recv, 3)),
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(recv, 5)) };
            (conn.robot as IPosable).SetPose(inputs);
        }
        else
        {
            Debug.Log("Requested pose from a non posable robot");
        }
    }
    // Drive Done
    private void Command_Z(byte[] recv, RobotConnection conn)
    {
        if(conn.robot is IVWDrivable)
        {
            bool done =(conn.robot as IVWDrivable).VWDriveDone();
            Packet p = new Packet();
            p.packetType = PacketType.SERVER_MESSAGE;
            p.dataSize = 1;
            p.data = BitConverter.GetBytes(done);
            serverManager.WritePacket(conn, p);
        }
        else
        { 
            Debug.Log("Requested drive done from a non VW drivable robot");
        }
    }
    // Get Camera
    private void Command_f(byte[] recv, RobotConnection conn)
    {
        if (conn.robot is HasCameras)
        {
            byte[] img = (conn.robot as HasCameras).GetCameraOutput(0);
            Packet packet = new Packet();
            packet.packetType = PacketType.SERVER_CAMIMG;
            packet.dataSize = (UInt32)img.Length;
            packet.data = img;
            serverManager.WritePacket(conn, packet);
        }
    }
    // Set Camera
    private void Command_F(byte[] recv, RobotConnection conn)
    {
        if(conn.robot is HasCameras)
        {
            int[] inputs = new int[3];
            inputs[0] = recv[1] - 1;
            inputs[1] = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(recv, 2));
            inputs[2] = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(recv, 4));
            (conn.robot as HasCameras).SetCameraResolution(inputs);
        }
    }

    public void ReceiveCommand(byte[] recv, RobotConnection conn)
    {
        Debug.Log("Received Command: " + (char)recv[0]);
        switch ((char)recv[0])
        {
            case 'm':
                Command_m(recv, conn);
                break;
            case 'M':
                Command_M(recv, conn);
                break;
            case 'd':
                Command_d(recv, conn);
                break;
            case 's':
                Command_s(recv, conn);
                break;
            case 'p':
                Command_p(recv, conn);
                break;
            case 'q':
                Command_q(recv, conn);
                break;
            case 'Q':
                Command_Q(recv, conn);
                break;
            case 'Z':
                Command_Z(recv, conn);
                break;
            case 'f':
                Command_f(recv, conn);
                break;
            case 'F':
                Command_F(recv, conn);
                break;
            default:
                Debug.Log("Received an unknown command.");
                break;
        }
    }
}
