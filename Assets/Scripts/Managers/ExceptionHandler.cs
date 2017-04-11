using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceptionHandler {

    static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
    {
        Debug.Log("Caught an unhandled exception from " + e.ExceptionObject.ToString());
    }
}
