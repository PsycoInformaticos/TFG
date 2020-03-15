using System;
using System.Collections.Generic;
using UnityEngine;

public class Data
{

    public System.IO.StreamWriter fileWriter;
    string filePath;
    string fileName = "Movements.csv";

    public Data()
    {
        //Sets the path of the data file
        filePath = Application.dataPath + @"\Data\" + fileName;
        fileWriter = new System.IO.StreamWriter(filePath, false);

        fileWriter.AutoFlush = true;

        Init();

    }

    //Write the first line
    public void Init()
    {
        fileWriter.WriteLine("accelX-accelY-accelZ");
    }

    public void Write(float accelX, float accelY, float accelZ, bool last)
    {
        if (!last)
            fileWriter.WriteLine(accelX + "-" + accelY + "-" + accelZ + ",");
        else
            fileWriter.WriteLine(accelX + "-" + accelY + "-" + accelZ);
    }

}

