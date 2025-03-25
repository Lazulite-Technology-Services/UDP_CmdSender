using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManagerScript : MonoBehaviour
{
    public InputField UdpIp;
    public InputField UdpPort;
    public InputField OscIp;
    public List<InputField> UdpCmdInput;
    public List<InputField> OscCmdInput;
    public UDPClient UdpClient;
    public List<Button> Btns;
    int Btnindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < UdpCmdInput.Count; i++)
        {
            UdpCmdInput[i].text = PlayerPrefs.GetString("UdpCmdInput" + i);
        }
        for (int i = 0; i < OscCmdInput.Count; i++)
        {
            OscCmdInput[i].text = PlayerPrefs.GetString("OscCmdInput" + i);
        }
        UdpIp.text = PlayerPrefs.GetString("UdpIp");
        UdpPort.text = PlayerPrefs.GetString("UdpPort");
        OscIp.text = PlayerPrefs.GetString("OscIp");
        selectBtn(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectBtn(int index)
    {
        for (int i = 0; i < Btns.Count; i++)
        {
            Btns[i].interactable = (i != index);
        }
        Btnindex = index;
    }
    public void saveCmds()
    {
        for (int i = 0; i < UdpCmdInput.Count; i++)
        {
            PlayerPrefs.SetString("UdpCmdInput"+i, UdpCmdInput[i].text);
        }
        for (int i = 0; i < OscCmdInput.Count; i++)
        {
            PlayerPrefs.SetString("OscCmdInput" + i, OscCmdInput[i].text);
        }
        PlayerPrefs.SetString("UdpIp", UdpIp.text);
        PlayerPrefs.SetString("UdpPort", UdpPort.text);
        PlayerPrefs.SetString("OscIp", OscIp.text);
    }
    public void ardiunoCmdRecive()
    {
        btnCmd(Btnindex);
    }
    public void btnCmd(int Index)
    {
        UdpClient.SendValue(UdpCmdInput[Index].text);
        print("UDP CMD Send = " +  UdpCmdInput[Index].text);

        /*string oscMsg = OscCmdInput[Index].text;
        print("OSC CMD Send = " + oscMsg);
        OscMessage message = new OscMessage();
        message.address = oscMsg;
        message.values.Add(1); 
        OSC.INSTANCE.Send(message);*/
    }
}
