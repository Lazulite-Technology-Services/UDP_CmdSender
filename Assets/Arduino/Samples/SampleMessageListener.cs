/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */
using System;
using System.Collections;
using System.IO;
using System.Net.Configuration;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/**
* When creating your message listeners you need to implement these two methods:
*  - OnMessageArrived
*  - OnConnectionEvent
*/
public class SampleMessageListener : MonoBehaviour
{
    public static SampleMessageListener Instance;
    public AppManagerScript mngrScript;
    // Invoked when a line of data is received from the serial device.

    private void Start()
    {
        Instance = this;
    }

    void OnMessageArrived(string cmd)
    {
        Debug.Log(cmd);
        if(cmd.Contains("z")|| cmd.Contains("Z"))
        {
            print("OSC CMD Send = " + PlayerPrefs.GetString("OscCmdInput0"));
            OscMessage message = new OscMessage();
            message.address = PlayerPrefs.GetString("OscCmdInput0");
            message.values.Add(1);
            OSC.INSTANCE.Send(message);
        }
        if (cmd.Contains("s") || cmd.Contains("S"))
        {
            print("OSC CMD Send = " + PlayerPrefs.GetString("OscCmdInput1"));
            OscMessage message = new OscMessage();
            message.address = PlayerPrefs.GetString("OscCmdInput1");
            message.values.Add(1);
            OSC.INSTANCE.Send(message);
            mngrScript.ardiunoCmdRecive();
        }
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) )
        {
            OnMessageArrived("s");
            mngrScript.ardiunoCmdRecive();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnMessageArrived("Z");
        }
    }

}
