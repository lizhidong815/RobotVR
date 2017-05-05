using System;
using System.Collections;
using System.Collections.Generic;

public static class RobotFunction
{
    // Function to reverse byte order of a unsigned 32 bit integer
    // Because NetworkToHostOrder doenst work on unsigned ints
    public static UInt32 ReverseBytes(UInt32 value)
    {
        return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
            (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
    }

    // Function to reverse byte order of a unsigned 32 bit integer
    // Because NetworkToHostOrder doenst work on unsigned ints
    public static UInt16 ReverseBytes(UInt16 value)
    {
        return 0;
    //    return (value & 0x00FFU) << 8 | (value & 0xFF00) >> 8;
    }
}
