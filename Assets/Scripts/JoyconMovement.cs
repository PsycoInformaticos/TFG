/*
 * Based on JoyconLib from https://github.com/Looking-Glass/JoyconLib
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconMovement : MonoBehaviour
{
    private List<Joycon> joycons;
    Joycon j;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jcIndex = 0;
    public Quaternion orientation;

    public GameObject sword;
    //public GameObject shield;

    int contUp, contDown, contRight, contLeft;

    

    void Start()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon object attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jcIndex + 1)
        {
            Destroy(gameObject);
        }

        contUp = contDown = contRight = contLeft = 0;

        //Asignacion de cada mando a un objeto
        j = joycons[jcIndex];

        if (gameObject == sword)
        {
            if (!joycons[0].isLeft)
            {
                j = joycons[0];

            }

            else
                j = joycons[1];

        }

        //else if (gameObject == shield)
        //{
        //    if (joycons[0].isLeft)
        //    {
        //        j = joycons[0];

        //    }

        //    else
        //        j = joycons[1];

        //}

    }

    // Update is called once per frame
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Input();
            Movement();

        }
    }

    void Input()
    {
        Joycon j = joycons[jcIndex];
        // GetButtonDown checks if a button has been pressed (not held)
        if (j.GetButtonDown(Joycon.Button.PLUS))
        {
            Debug.Log("Plus button pressed");

            // Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
            j.Recenter();
        }
        // GetButtonDown checks if a button has been released
        if (j.GetButtonUp(Joycon.Button.PLUS))
        {
            Debug.Log("Plus released");
            Debug.Log(accel);
        }

        if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            Debug.Log("Rumble");

            // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
            // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

            j.SetRumble(160, 320, 0.6f, 200);

            // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
            // (Useful for dynamically changing rumble values.)
            // Then call SetRumble(0,0,0) when you want to turn it off.
        }
    }

    void Movement()
    {

        stick = j.GetStick();

        // Gyro values: x, y, z axis values (in radians per second)
        gyro = j.GetGyro();

        // Accel values:  x, y, z axis values (in Gs)
        accel = j.GetAccel();

        orientation.x = j.GetVector().x;
        orientation.y = j.GetVector().y;
        orientation.w = j.GetVector().w;
        //Al sumarle la y a la z el gameObject se coloca en la posición que tiene el mando. Actúan sobre el mismo eje de forma contraria
        orientation.z = j.GetVector().z + j.GetVector().y;

        gameObject.transform.rotation = orientation;

    }

    //Cutre código por probar con el contador
    public bool isAMovement(int m)
    {
        
        //Ariba = 0
        if (m == 0)
        {
            if (contUp >= 30)
            {
                contUp = 0;
                return true;
            }
            else
            {
                if(accel.x > 0 && accel.y > 0 && accel.z > 0)
                {
                    contUp++;
                }
            }
        }

        //Abajo = 1 
        else if (m == 1)
        {
            if (contDown >= 5)
            {
                contDown = 0;
                return true;
            }
            else
            {
                if (accel.x < 0 && accel.y < 0 && accel.z < 0)
                {
                    contDown++;
                }
            }
        }

        //Derecha = 2
        else if (m == 2)
        {
            if (contRight >= 5)
            {
                contRight = 0;
                return true;
            }
            else
            {
                if (accel.x > 0 && accel.y > 0 && accel.z > 0)
                {
                    contRight++;
                }
            }
        }

        //Izquierda = 3
        else if (m == 3)
        {
            if (contLeft >= 5)
            {
                contLeft = 0;
                return true;
            }
            else
            {
                if (accel.x > 0 && accel.y > 0 && accel.z > 0)
                {
                    contLeft++;
                }
            }
        }

        return false;
    }

    //Devuelve la aceleración que tiene cuando se le pide
    public Vector3 getAccel()
    {
        return accel;
    }
}
