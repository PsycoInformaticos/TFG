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
        filePath = Application.dataPath + @"\Data\" + System.DateTime.Now.ToString("dd.MM.yyyy.HH.mm") + "_" + fileName;
        fileWriter = new System.IO.StreamWriter(filePath, false);

        fileWriter.AutoFlush = true;

        //Init();

    }

    //Write the first line
    public void Init()
    {
        for (int i = 1; i <= 50; i++)
        {
            fileWriter.Write("accel" + i + ", , , ");
        }

        fileWriter.Write('\n');

        for (int j = 1; j <= 50; j++)
        {
            fileWriter.Write("X, Y, Z, ");
        }

        fileWriter.Write('\n');

    }

    public void Write(float accelX, float accelY, float accelZ, bool last)
    {
        //Si no es la última escribe en la misma linea
        if (!last)
        {
            //Pasamos los floats al formato universal para que en el csv se escriban con . ya que el formato de separacion es la coma
            fileWriter.Write(accelX.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                + ", " + accelY.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                + ", " + accelZ.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + ",");
        }
        else
        {
            fileWriter.Write(accelX.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                 + ", " + accelY.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                 + ", " + accelZ.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + '\n');

        }
    }

}

