﻿/*
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
    public Vector3 lastAccel;
    public int jcIndex = 0;
    public Quaternion orientation;

    public GameObject sword;
    //public GameObject shield;

    //Variables para recoger movimientos
    float cont;

    //Cada movimiento tiene 150 valores de aceleracion (50 de 3 ejes cada uno)
    float[] move = new float[150];
    int it;                          //iterador de move
    bool isMove;

    public GameObject red;

    //Variables para la recogida de datos
    bool pressed;
    

    void Start()
    {
        pressed = false;
        isMove = false;
        it = 0;

        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        lastAccel = new Vector3(0, 0, 0);
        // get the public Joycon object attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jcIndex + 1)
        {
            Destroy(gameObject);
        }

        cont = 0.0f;

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

    private void FixedUpdate()
    {
        Movement();

    }

    // Update is called once per frame
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Input();

        }

    }

    void Input()
    {
        Joycon j = joycons[jcIndex];
        // GetButtonDown checks if a button has been pressed (not held)
        if (j.GetButtonDown(Joycon.Button.PLUS))
        {
            // Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
            j.Recenter();
        }
        

        //Al pulsar el boton B (o abajo en el izquierdo) se activa el booleano para poder recoger la aceleracion
        if (j.GetButtonDown(0))
        {
            //pressed = true;
        }
    }

    void Movement()
    {

        stick = j.GetStick();

        // Gyro values: x, y, z axis values (in radians per second)
        gyro = j.GetGyro();

        // Accel values:  x, y, z axis values (in Gs)
        lastAccel = accel;
        accel = j.GetAccel();

        orientation.x = j.GetVector().x;
        orientation.y = j.GetVector().y;
        orientation.w = j.GetVector().w;

        //Al sumarle la y a la z el gameObject se coloca en la posición que tiene el mando. Actúan sobre el mismo eje de forma contraria
        orientation.z = j.GetVector().z + j.GetVector().y;

        gameObject.transform.rotation = orientation;

        Debug.Log(accel);

        //Si JoyconMovement devuelve que se ha pulsado el botón b cuenta 1 segundo
        if (accel != lastAccel)
        {
            //Suponemos que hay 50 fixedupdate por segundo
            //Si el contador llega a 50 (uno segundo), indicará
            //que se ponga a false el pulsado y que no entre aquí
            //Y el contador vuelve a su estado inicial
            if (cont >= 50)
            {
                setNotPressed();
                cont = 0;
                it = 0;
                isMove = true;

                float[] X = new float[150];
                for (int i = 0; i < 150; i++)
                {
                    X[i] = move[i];
                }

                int n = red.GetComponent<NeuralNetwork>().Movement(X);
            }

            //Si no ha pasado un segundo, guarda los valores de la acceleracion en el array
            else
            {
                cont++;

                //Se guarda cada valor de la acceleracion
                move[it++] = accel.x;
                move[it++] = accel.y;
                move[it++] = accel.z;
            }
        }

    }

    //Prueba con el contador
    public int moveType()
    {
        if (isMove)
        {
            float[] X = new float[150];
            for (int i = 0; i < 150; i++)
            {
                X[i] = move[i];
            }

            return red.GetComponent<NeuralNetwork>().Movement(X);
        }

        return -1;

    }

    //Devuelve la aceleración que tiene cuando se le pide
    public Vector3 getAccel()
    {
        return accel;
    }

    //Devuelve si ha sido pulsada la A del mando o no para recoger datos
    public bool getPressed()
    {
        return pressed;
    }

    public void setNotPressed()
    {
        pressed = false;
    }
}
