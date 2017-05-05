using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationHelper  {

	public static string localDataPath() {
		string dataPath = Application.dataPath;
		for(int i = 0; i<2; i++)
			dataPath = dataPath.Substring (0, dataPath.LastIndexOf (slash()));
		return Application.isEditor ? dataPath + slash() + "RobotVR-Build" : dataPath;
	}

	public static string slash() {
		switch(Application.platform){
		case RuntimePlatform.WindowsEditor:
			return "\\";
		case RuntimePlatform.WindowsPlayer:
			return "\\";
		default:
			return "/";
		}
	}
}
