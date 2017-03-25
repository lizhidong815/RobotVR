using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationHelper  {

	public static string dataPath = Application.dataPath;
	public static string appPath = Application.dataPath.Substring (0, Application.dataPath.LastIndexOf ('/'));
	public static string localDataPath = "/Users/JoelFrewin/Documents/RobotVR/RobotVR-Mac";//appPath.Substring (0, appPath.LastIndexOf ('/'));
}
