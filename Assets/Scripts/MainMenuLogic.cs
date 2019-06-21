using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MainMenuLogic : MonoBehaviour
{
    public static bool wasCalibration = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Application.LoadLevel(0);
            wasCalibration = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Application.LoadLevel(1);
        }

    }
}
