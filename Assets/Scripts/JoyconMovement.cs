/*
 * Based on JoyconLib from https://github.com/Looking-Glass/JoyconLib
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JoyconMovement : MonoBehaviour
{
    private List<Joycon> joycons;
    Joycon j;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public Vector3 linearAccel;
    public Vector3 gravity;
    public int jcIndex = 0;
    public Quaternion orientation;

    public GameObject rightController;
    public GameObject leftController;

    //Variables para recoger movimientos
    float cont;

    //Latencia
    public float timeWait;
    float contWait;

    //Cada movimiento tiene 150 valores de aceleracion (50 de 3 ejes cada uno)
    float[] move = new float[150];
    int it;                          //iterador de move

   Queue<int> moves = new Queue<int>();

    public GameObject red;

    //Variables para la recogida de datos
    bool pressed;

    int type;
    

    void Start()
    {
        pressed = false;
        it = 0;

        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        linearAccel = new Vector3(0, 0, 0);
        gravity = new Vector3(0, 0, 0);

        // get the public Joycon object attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jcIndex + 1)
        {
            Destroy(gameObject);
        }

        cont = 0.0f;
        contWait = timeWait;

        //Asignacion de cada mando a un objeto
        j = joycons[jcIndex];

        if (gameObject == rightController)
        {
            if (!joycons[0].isLeft)
            {
                j = joycons[0];

            }

            else
                j = joycons[1];

        }

        else if (gameObject == leftController)
        {
            if (joycons[0].isLeft)
            {
                j = joycons[0];

            }

            else
                j = joycons[1];

        }

        type = -1;

    }

    private void FixedUpdate()
    {
        JoyconUpdate();
        
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

        //Al pulsar el boton PLUS o MINUS (depende del mando que se este usando) 
        if (j.GetButtonDown(Joycon.Button.PLUS) || j.GetButtonDown(Joycon.Button.MINUS))
        {
            //Activa el booleano para poder recoger la aceleracion en ColletData
            pressed = true;

            // Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
            j.Recenter();

        }

    }

    void JoyconUpdate()
    {
        stick = j.GetStick();

        // Gyro values: x, y, z axis values (in radians per second)
        gyro = j.GetGyro();

        // Accel values:  x, y, z axis values (in Gs)
        accel = j.GetAccel();

        gravity.x = 0.9f * gravity.x + 0.1f * accel.x;
        gravity.y = 0.9f * gravity.y + 0.1f * accel.y;
        gravity.z = 0.9f * gravity.z + 0.1f * accel.z;

        linearAccel.x = accel.x - gravity.x;
        linearAccel.y = accel.y - gravity.y;
        linearAccel.z = accel.z - gravity.z;


        orientation.x = j.GetVector().x;
        orientation.y = j.GetVector().y;
        orientation.w = j.GetVector().w;

        //Al sumarle la y a la z el gameObject se coloca en la posición que tiene el mando. Actúan sobre el mismo eje de forma contraria
        orientation.z = j.GetVector().z + j.GetVector().y;

        gameObject.transform.rotation = orientation;
    }

    void Movement()
    {

        //Latencia de medio segundo entre la recogida de un movimiento y otro
        if (contWait >= timeWait)
        {
            
            //Suponemos que hay 50 fixedupdate por segundo
            //Si el contador llega a 50 (uno segundo), indicará
            //que se ponga a false el pulsado y que no entre aquí
            //Y el contador vuelve a su estado inicial
            if (cont >= 50)
            {
                cont = 0;
                it = 0;

                float[] X = new float[150];
                for (int i = 0; i < 150; i++)
                {
                    X[i] = move[i];
                }

                moves.Enqueue(red.GetComponent<NeuralNetwork>().Movement(X));

                contWait = 0f;

            }

            //Si no ha pasado un segundo, guarda los valores de la acceleracion en el array
            else
            {
                cont++;

                //Se guarda cada valor de la acceleracion
                move[it++] = linearAccel.x;
                move[it++] = linearAccel.y;
                move[it++] = linearAccel.z;

                //move[it++] = gyro.x;
                //move[it++] = gyro.y;
                //move[it++] = gyro.z;
            }


        }
        else contWait++;
       

    }

    //Prueba con el contador
    public int moveType()
    {
        //int[] dirRepetidas = new int[] { 0, 0, 0, 0, 0 };

        //int vueltas = moves.Count;
        //if (vueltas > 0)
        //{
        //    for (int i = 0; i < vueltas; i++)
        //    {
        //        int peek = moves.Peek(); moves.Dequeue();
        //        if (peek == 0) dirRepetidas[0]++;
        //        else if (peek == 1) dirRepetidas[1]++;
        //        else if (peek == 2) dirRepetidas[2]++;
        //        else if (peek == 3) dirRepetidas[3]++;
        //        else if (peek == 4) dirRepetidas[4]++;
        //    }

        //    int t = 0;
        //    for (int j = 0; j < dirRepetidas.Length; j++)
        //    {

        //        if (dirRepetidas[j] > t)
        //        {
        //            t = dirRepetidas[j];
        //            type = j;
        //        }
        //    }
        //}
        if (moves.Count != 0)
        {
            type = moves.Peek(); moves.Dequeue();
        }
        return type;
        

    }

    //Devuelve la aceleración que tiene cuando se le pide
    public Vector3 getAccel()
    {
        return linearAccel;
    }

    //Devuelve el gyro que tiene cuando se le pide
    public Vector3 getGyro()
    {
        return gyro;
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
