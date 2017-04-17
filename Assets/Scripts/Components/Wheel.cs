using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotComponents
{
    [System.Serializable]
    public class Wheel
    {
        public WheelCollider wheel;
        public float speed;
		public float ticks = 0;
		public float tickrate;
		public Quaternion currentrotation;
		public Vector3 currentrotation2;
		public int encoderRate = 540;

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
		}

		public void FixedUpdate()
		{
			updateRotation ();
			updateTorque ();
			ApplyLocalPositionToVisuals();
		}

		public void updateTorque(){
			wheel.motorTorque = 1 * (speed - tickrate);
		}

		public void updateRotation(){
			Quaternion newrotation;
			Vector3 position;
			wheel.GetWorldPose(out position, out newrotation);

			//normalise euler angles *maybe Quaternions are a better option here*
			float currentxrot;
			if(currentrotation.eulerAngles.y > 270){
				currentxrot = currentrotation.eulerAngles.x;
			} else if(currentrotation.eulerAngles.x < 180){
				currentxrot = 180 - currentrotation.eulerAngles.x;
			} else {
				currentxrot = 540 - currentrotation.eulerAngles.x;
			}
			float newxrot;
			if(newrotation.eulerAngles.y > 270){
				newxrot = newrotation.eulerAngles.x;
			} else if(newrotation.eulerAngles.x < 180){
				newxrot = 180 - newrotation.eulerAngles.x;
			} else {
				newxrot = 540 - newrotation.eulerAngles.x;
			}	

			float rotationRate = newxrot - currentxrot;
			if (rotationRate < -180) rotationRate += 360;
			tickrate = rotationRate/360*540;
			ticks += Mathf.Round(tickrate);
			currentrotation = newrotation;
			currentrotation2 = currentrotation.eulerAngles;
		}

		// Function to update visuals of wheels
		public void ApplyLocalPositionToVisuals()
		{
			if (wheel.transform.childCount == 0)
			{
				return;
			}

			Transform visualWheel = wheel.transform.GetChild(0);

			Vector3 position;
			Quaternion rotation;
			wheel.GetWorldPose(out position, out rotation);

			visualWheel.transform.position = position;
			visualWheel.transform.rotation = rotation;
		}
    }
}
