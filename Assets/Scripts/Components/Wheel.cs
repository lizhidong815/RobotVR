using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotComponents
{
    [System.Serializable]
    public class WheelInfo
    {
        public WheelCollider wheel;
        public float speed;
        public int encRevs;

        public bool pidEnabled = false;
        public int P;
        public int I;
        public int D;
    }
}
