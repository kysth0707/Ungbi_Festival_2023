using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoCommunication : MonoBehaviour
{
    public SerialPort MyArduino;
    [SerializeField] string portName = "COM8";
    public bool turnAndGoUp = false;
    public bool goUp = false;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            MyArduino = new SerialPort(portName, 115200);
            MyArduino.ReadTimeout = 100;
            MyArduino.Open();
        }
        catch
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            string receivedData = MyArduino.ReadExisting();
            string[] SplitData = receivedData.Split(' ');
            if (int.Parse(SplitData[0]) == 1)
            {
                turnAndGoUp = true;
            }
            else
            {
                turnAndGoUp = false;
            }

            if (int.Parse(SplitData[1]) == 1 || int.Parse(SplitData[2]) == 1)
            {
                goUp = true;
            }
            else
            {
                goUp = false;
            }
        }
        catch
        {

        }
    }
}
