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
		public Camera cameraComponent;
		public Texture2D tex;

        // Calculate sensor values at each frame
		public byte[] GetBytes(){
			return tex.GetRawTextureData ();
		}

		void OnPostRender() {
			RenderTexture target = cameraComponent.targetTexture;
			if (target == null) {
				print("no target texture");
				return;
			}
			tex = new Texture2D( target.width, target.height, TextureFormat.RGB24, false );
			Rect rect = new Rect( 0, 0, target.width, target.height );
			tex.ReadPixels( rect, 0, 0, false );
			tex.Apply( false );
		}
    }
}
