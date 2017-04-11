using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationHelper  {

	public static string localDataPath() {
		string dataPath = Application.dataPath;
		for(int i = 0; i<2; i++)
			dataPath = dataPath.Substring (0, dataPath.LastIndexOf ('/'));
		return Application.isEditor ? dataPath + "/RobotVR-Build" : dataPath;
	}
}
