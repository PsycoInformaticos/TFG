using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackState : MonoBehaviour
{
    public Image attackArrow;
    public Canvas canvas;

    public GameObject sword;

    Vector3 accel;

    Queue arrowQueue;

    bool flechaDestruida;

    private void Start()
    {
        arrowQueue = new Queue();

        flechaDestruida = true;
    }

    //enum direction { up, down, left, right}

    //Principal run of the attack state in the combats
    public void attack()
    {
        //Se generará nueva flecha por tiempo y no pulsando un botón

       Image arrow = (Image)arrowQueue.Peek();

        //Arriba
        if (arrow.transform.rotation.z == 0 && sword.GetComponent<JoyconMovement>().isAMovement(0))
        {
            //Revisar como destruir una imagen de una cola
            Destroy((Image)arrowQueue.Peek());
            arrowQueue.Dequeue();
            flechaDestruida = true;
        }

        //Abajo
        if (arrow.transform.rotation.z == 180 && sword.GetComponent<JoyconMovement>().isAMovement(1))
        {
            Destroy((Image)arrowQueue.Peek());
            arrowQueue.Dequeue();
            flechaDestruida = true;
        }

        //Derecha
        if (arrow.transform.rotation.z == 90 && sword.GetComponent<JoyconMovement>().isAMovement(2))
        {
            Destroy((Image)arrowQueue.Peek());
            arrowQueue.Dequeue();
            flechaDestruida = true;
        }

        //Izquierda
        if (arrow.transform.rotation.z == -90 && sword.GetComponent<JoyconMovement>().isAMovement(3))
        {
            Destroy((Image)arrowQueue.Peek());
            arrowQueue.Dequeue();
            flechaDestruida = true;
        }

    }

    public void nextArrow()
    {
        Vector3 posArrow = new Vector3(canvas.transform.position.x + 100, canvas.transform.position.y, 0);

        //Depends on the number, will apear an arrrow in diferent direction. Will be some direction more, but first only 4 for trying
        int randomDirection = /*Random.Range(1, 4);*/ 0;
        int rotationZ = 0;
        //Select the direction depends on the random number
        switch(randomDirection){

            //UP
            //case 0: the original rotation of the sprite
                
            //DOWN
            case 1:
                rotationZ = 180;
                break;
            //LEFT
            case 2:
                rotationZ = 90;
                break;
            //RIGHT
            case 3:
                rotationZ = -90;
                break;
        }
       
        Instantiate(attackArrow, posArrow, transform.rotation, canvas.transform);
        
        transform.Rotate(0, 0, rotationZ);

        arrowQueue.Enqueue(attackArrow);

        flechaDestruida = false;

    }

    public bool GetFlechaDestruida ()
    {
        return flechaDestruida;
    }
}
