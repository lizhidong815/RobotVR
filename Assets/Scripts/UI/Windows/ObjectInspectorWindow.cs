using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Displays a window of object information
public class ObjectInspectorWindow : Window {

    [SerializeField]
    private Text objectName, objectID, objectMass, objectXPos, objectYPos;

    public void NewObjectSelected(PlaceableObject newObj)
    {
        objectName.text = newObj.name;
        objectID.text = newObj.objectID.ToString();
        objectMass.text = newObj.rigidBody.mass.ToString();
        objectXPos.text = newObj.transform.position.x.ToString();
        objectYPos.text = newObj.transform.position.z.ToString();
    }
}
