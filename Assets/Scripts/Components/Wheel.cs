using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotComponents
{
    public class Wheel: MonoBehaviour
    {
        public Rigidbody rigidBody;
		public HingeJoint wheelHingeJoint;
		public CapsuleCollider wheelCollider;

        public float speed;
		public float ticks = 0;
		public float tickrate;
		public float currentrotation;
		public int encoderRate = 540;
		public float diameter;

        public bool pidEnabled = false;
        public int P;
        public int I;
        public int D;

		public void SetPIDParams(int p, int i, int d){
			pidEnabled = true;
			P = p;
			I = i;
			D = d;
		}

		public void SetMotorSpeed(float speed)
		{
			this.speed = speed;
			JointMotor newmotor = GetComponent<HingeJoint>().motor;
			newmotor.targetVelocity = speed;
			GetComponent<HingeJoint>().motor = newmotor;
		}
			
		public void FixedUpdate()
		{
				updateRotation ();
		}

		public void updateRotation(){
			float deltaAngle = GetComponent<HingeJoint>().angle - currentrotation;
			if (Mathf.Abs (deltaAngle) < 20)
				tickrate = deltaAngle * encoderRate / 360;

			ticks += tickrate;
			currentrotation = GetComponent<HingeJoint>().angle;
		}
    }
}
