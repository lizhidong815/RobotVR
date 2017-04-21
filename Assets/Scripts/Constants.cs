using System.Collections;
using System.Collections.Generic;

public static class PacketType  {

    public const byte CLIENT_HANDSHAKE = 1;
    public const byte SERVER_HANDSHAKE = 2;
    public const byte CLIENT_DISCONNECT = 3;
    public const byte SERVER_DISCONNECT = 4;
    public const byte SERVER_READY = 5;
    public const byte SERVER_STOP = 6;
    public const byte CLIENT_START = 7;
    public const byte CLIENT_STOP = 8;
    public const byte SERVER_MESSAGE = 9;
    public const byte CLIENT_MESSAGE = 10;
    public const byte SERVER_CAMIMG = 11;
    public const byte CLIENT_CAMGET = 12;
}
