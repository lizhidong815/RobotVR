using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotComponents
{
    public class PSDSensor : MonoBehaviour
    {

        public float value = 0;

        // Calculate sensor values at each frame
        void FixedUpdate()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, forward, out hit, 200))
            {
                value = hit.distance;
            }
            else
            {
                value = -1;
            }
        }
    }
}
