using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotComponents
{
    public class Servo : MonoBehaviour
    {

        public float minAngle = -90f;
        public float maxAngle = 90f;
        public float desiredPosition = 0f;

        private HingeJoint hinge;
        private JointMotor motor;

        private float NegAngles(float val)
        {
            return val <= 180 ? val : val - 360;
        }

        public void SetPosition(int pos)
        {
            desiredPosition = Mathf.Clamp((float)pos, minAngle, maxAngle);
        }

        private void Start()
        {
            hinge = GetComponent<HingeJoint>();
            motor = hinge.motor;
            motor.force = 100000;
            hinge.useMotor = true;
            motor.targetVelocity = 0;
            motor.freeSpin = false;
        }

        public void FixedUpdate()
        {
            if (NegAngles(transform.localRotation.eulerAngles.y) < desiredPosition - 0.5f)
            {
                motor.targetVelocity = 30;
                hinge.motor = motor;
            }
            else if (NegAngles(transform.localRotation.eulerAngles.y) > desiredPosition + 0.5f)
            {
                motor.targetVelocity = -30;
                hinge.motor = motor;
            }
            else
            {
                Debug.Log("Rotating stopped");
                motor.targetVelocity = 0;
                hinge.motor = motor;
            }
        }
    }
}