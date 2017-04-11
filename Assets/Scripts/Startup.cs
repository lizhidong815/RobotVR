using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{

    // Ensure startup is only ever ran once
    private static bool doStartup = true;

    // Managers are instantiated in Awake
    // This ensures valid references can 
    // be obtained in Start
    void Awake()
    {
        if (doStartup)
            doStartup = false;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameObject.AddComponent<SimManager>();
        gameObject.AddComponent<ServerManager>();
        gameObject.AddComponent<RobotBuilder>();
        gameObject.AddComponent<WorldBuilder>();
        gameObject.AddComponent<UIManager>();
    }

    void BuildManager()
    {
       //
    }

    void BuildServer()
    {
        //
    }

    void LoadRobots()
    {
        //
    }

    public void ReadCommands()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            Debug.Log("ARG " + i + ": " + args[i]);
            if (args[i] == "-input")
            {
                //simReader = new SimReader();
                //simReader.simManager = simManager;
                //simReader.read(ApplicationHelper.localDataPath() + args[i + 1]);
            }
        }
    }
}
