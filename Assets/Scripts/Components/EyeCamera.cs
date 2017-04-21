using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotCommands;
using RobotComponents;

namespace RobotComponents
{
	public class EyeCamera : MonoBehaviour
    {

        public float value = 0;
		new public Camera camera;

        // Calculate sensor values at each frame
		public byte[] GetBytes(){
			return GetScreenshot ().GetRawTextureData ();
		}

		Texture2D GetScreenshot()
		{
			Rect viewRect = camera.pixelRect;
			Texture2D tex = new Texture2D( (int)viewRect.width, (int)viewRect.height, TextureFormat.RGB24, false );
			tex.ReadPixels( viewRect, 0, 0, false );
			tex.Apply( false );
			return tex;
		}
    }
}
